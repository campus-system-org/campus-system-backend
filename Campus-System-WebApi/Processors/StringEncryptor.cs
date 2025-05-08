using System.Security.Cryptography;
using System.Text;

namespace Campus_System_WebApi.Processors
{
    /// <summary>
    /// 雙向加解密處理
    /// </summary>
    public class StringEncryptor
    {
        private readonly string _key;

        public StringEncryptor(IConfiguration configuration)
        {
            _key = configuration["encryptor:key"]!;
        }

        /// <summary>
        /// 加密提供的明文字串，並返回包含鹽值、密文和 HMAC 的 Base64 編碼數據。
        /// </summary>
        public string Encrypt(string plainText)
        {
            if (plainText == null) throw new ArgumentNullException(nameof(plainText));

            // 生成隨機鹽值
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // 使用 PBKDF2 生成加密密鑰和 HMAC 密鑰
            var (encryptionKey, hmacKey) = DeriveKeys(salt);

            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.GenerateIV(); // 隨機生成 IV
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var ms = new MemoryStream())
                {
                    // 寫入鹽值和 IV
                    ms.Write(salt, 0, salt.Length);
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    // 加密數據
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs, Encoding.UTF8))
                    {
                        sw.Write(plainText);
                    }

                    // 生成密文
                    byte[] cipherData = ms.ToArray();

                    // 計算 HMAC
                    byte[] hmac = ComputeHMAC(cipherData, hmacKey);

                    // 合併密文與 HMAC
                    using (var finalStream = new MemoryStream())
                    {
                        finalStream.Write(cipherData, 0, cipherData.Length);
                        finalStream.Write(hmac, 0, hmac.Length);
                        return Convert.ToBase64String(finalStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 解密提供的 Base64 編碼數據，並驗證完整性，返回原始明文字串。
        /// </summary>
        public string Decrypt(string cipherText)
        {
            if (cipherText == null) throw new ArgumentNullException(nameof(cipherText));

            byte[] fullData = Convert.FromBase64String(cipherText);

            using (var ms = new MemoryStream(fullData))
            {
                // 提取鹽值
                byte[] salt = new byte[16];
                ms.Read(salt, 0, salt.Length);

                // 提取 IV
                byte[] iv = new byte[16];
                ms.Read(iv, 0, iv.Length);

                // 剩餘部分是密文和 HMAC
                byte[] cipherData = new byte[fullData.Length - salt.Length - iv.Length - 32];
                ms.Read(cipherData, 0, cipherData.Length);

                // 提取 HMAC
                byte[] providedHmac = new byte[32];
                ms.Read(providedHmac, 0, providedHmac.Length);

                // 重新生成密鑰
                var (encryptionKey, hmacKey) = DeriveKeys(salt);

                // 驗證 HMAC
                byte[] expectedHmac = ComputeHMAC(fullData.AsSpan(0, fullData.Length - 32).ToArray(), hmacKey);
                if (!CryptographicOperations.FixedTimeEquals(providedHmac, expectedHmac))
                {
                    throw new CryptographicException("數據完整性驗證失敗！");
                }

                // 解密數據
                using (Aes aes = Aes.Create())
                {
                    aes.Key = encryptionKey;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var cipherStream = new MemoryStream(cipherData))
                    using (var cs = new CryptoStream(cipherStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs, Encoding.UTF8))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// 使用鹽值從主密鑰衍生加密密鑰和 HMAC 密鑰。
        /// </summary>
        private (byte[] encryptionKey, byte[] hmacKey) DeriveKeys(byte[] salt)
        {
            using (var keyDerivation = new Rfc2898DeriveBytes(_key, salt, 200, HashAlgorithmName.SHA256))
            {
                byte[] encryptionKey = keyDerivation.GetBytes(32); // 256 位元加密密鑰
                byte[] hmacKey = keyDerivation.GetBytes(32);       // 256 位元 HMAC 密鑰
                return (encryptionKey, hmacKey);
            }
        }

        /// <summary>
        /// 計算數據的 HMAC。
        /// </summary>
        private byte[] ComputeHMAC(byte[] data, byte[] hmacKey)
        {
            using (var hmac = new HMACSHA256(hmacKey))
            {
                return hmac.ComputeHash(data);
            }
        }
    }
}
