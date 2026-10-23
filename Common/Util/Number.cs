namespace Common.Util;
public static class Number
{
    public static string? ToToman(decimal? amount)
    {
        return amount != null ? amount.Value.ToString("#,##0") + " تومان":null;
    }
}