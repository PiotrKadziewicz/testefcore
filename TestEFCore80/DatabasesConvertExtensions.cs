namespace TestEFCore80
{
    public static class DatabasesConvertExtensions
    {
        public static string FromListToStringJoin<T>(this IEnumerable<T>? src)
        {
            return src != null && src.Any() == true ? string.Join(";", src.Select(x => x!.ToString())) : string.Empty;
        }

        public static T EnumParser<T>(this string? toEnum) where T : struct, Enum
        {
            return !string.IsNullOrEmpty(toEnum) ? Enum.Parse<T>(toEnum) : default;
        }

        public static string[]? SplitObj(this string dst)
        {
            string[] splitted = dst.Split(";", StringSplitOptions.RemoveEmptyEntries);

            if (splitted.Any())
                return splitted;
            else
                return null;
        }

        public static int[]? SplitObjToInt(this string dst)
        {
            var splitedObj = dst.SplitObj();
            return splitedObj != null && splitedObj.Any() ? splitedObj.Select(d => Convert.ToInt32(d)).ToArray() : null;
        }
    }
}
