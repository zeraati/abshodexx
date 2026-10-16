using System.Text.Json;

namespace Common.Util;

public static class Json
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    public static string? Serialize(object? obj)
    {
        try{return obj is null ? null : JsonSerializer.Serialize(obj, obj.GetType(), JsonOptions);}

        catch (Exception exception) { Console.WriteLine(exception.Message); }
        return "Json Serialize Exception";
    }

    public static T? Deserialize<T>(string? json)
    {
        try { return json is null ? default : JsonSerializer.Deserialize<T>(json, JsonOptions); }

        catch (Exception exception) { Console.WriteLine(exception.Message); }
        return default;
    }   
}