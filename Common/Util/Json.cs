using System.Text.Json;
using System.Text.Encodings.Web;

namespace Common;

public static class Json
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private static readonly JsonSerializerOptions BaleOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNameCaseInsensitive = true
    };

    public static string? Serialize(object? obj, string? provider = null)
    {
        try { return obj is null ? null : JsonSerializer.Serialize(obj, obj.GetType(), provider?.ToLower() == "bale" ? BaleOptions : JsonOptions); }

        catch (Exception exception) { Console.WriteLine(exception.Message); }
        return "Json Serialize Exception";
    }

    public static T? Deserialize<T>(string? json, string? provider = null)
    {
        try { return json is null ? default : JsonSerializer.Deserialize<T>(json, provider == "bale" ? BaleOptions : JsonOptions); }

        catch (Exception exception) { Console.WriteLine(exception.Message); }
        return default;
    }
}

//TODO دو تا مشکل پیاده سازی هارد کدی و نامناسب بله و همینطور ساخت آپشن اضافه که رم میگیره