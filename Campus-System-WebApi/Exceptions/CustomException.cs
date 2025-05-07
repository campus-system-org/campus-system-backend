namespace Campus_System_WebApi
{
    /// <summary>
    /// 客製化錯訊
    /// </summary>
    public class CustomException : Exception
    {
        public int ErrorCode { get; private set; }

        public CustomException(string msg = "", int errorCode = 500) : base(msg)
        {
            this.ErrorCode = errorCode;
        }
    }
}
