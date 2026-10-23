using Common.HttpClient;

namespace Baha24;
public partial class Service(HttpClientService client){}

public static class Url
{
    public const string Base = "https://baha24.com/api/";
    public const string Gold18 = "v1/price/GOL18";
}