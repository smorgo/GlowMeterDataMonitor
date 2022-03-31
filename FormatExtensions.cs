public static class FormatExtensions
{
    public static int FromHexToInt(this string? value)
    {
        if (value == null)
        {
            return 0;
        }

        return int.Parse(value, System.Globalization.NumberStyles.HexNumber);
    }

    public static long FromHexToLong(this string? value)
    {
        if (value == null)
        {
            return 0;
        }

        return long.Parse(value, System.Globalization.NumberStyles.HexNumber);
    }

    public static DateTime ToUtcDateTime(this string? value)
    {
        if (value == null)
        {
            return DateTime.MinValue;
        }

        var milliseconds = long.Parse(value);
        var time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds);
        return time;
    }
}