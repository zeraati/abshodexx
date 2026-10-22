using Common.HttpClient;

namespace Bale;
public partial class Service(HttpClientService client){}

public static class Url
{
    public const string Base = "https://tapi.bale.ai/bot1943379383:kt82jf4bty1VFrEKd6tYv4juJt0irNgD8KE/";
    public static string GetUpdates(long offset) => $"getUpdates?offset={offset}&timeout=30";
    public const string SendMessage = "sendMessage";
}