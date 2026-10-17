using System.Text.RegularExpressions;

namespace Common.Util;
public static class String
{
    public static string Format(string template, params object[] args)
    {
        int index = 0;

        string format = Regex.Replace(
            template,
            @"(?<!\{)\{([^{}]+)\}(?!\})",
            match => "{" + index++ + "}"
        );

        return string.Format(format, args);
    }
}