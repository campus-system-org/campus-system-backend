namespace Campus_System_WebApi
{
    public static class EnumExtension
    {
        public static T ToEnum<T>(this string value) where T : struct, Enum
        {
            if (Enum.TryParse(value, out T result))
            {
                if (Enum.IsDefined(typeof(T), result))
                {
                    return result;
                }
            }

            throw new ArgumentException($"'{value}' is not a valid value for enum type {typeof(T).Name}");
        }

        public static bool TryParseEnum<T>(this string value, out T? @enum) where T : struct, Enum
        {
            if (Enum.TryParse(value, out T result))
            {
                if (Enum.IsDefined(typeof(T), result))
                {
                    @enum = result;
                    return true;
                }
            }

            @enum = default;
            return false;

            throw new ArgumentException($"'{value}' is not a valid value for enum type {typeof(T).Name}");
        }
    }
}
