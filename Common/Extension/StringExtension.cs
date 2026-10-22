namespace Common.Extension;

public static class StringExtension
{
    public static bool Empty(this string? value) => string.IsNullOrWhiteSpace(value);
    public static bool Equal(this string? value, string? other) => string.Equals(value, other, StringComparison.OrdinalIgnoreCase);


    public static string? ToPersianNumber(this string? input)
    {
        return input?.Replace('0', '۰').Replace('1', '۱').Replace('2', '۲').Replace('3', '۳').Replace('4', '۴').Replace('5', '۵')
            .Replace('6', '۶').Replace('7', '۷').Replace('8', '۸').Replace('9', '۹');
    }
}
