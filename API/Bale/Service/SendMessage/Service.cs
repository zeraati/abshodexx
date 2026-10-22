using Common.Extension;
using Common.HttpClient;

namespace Bale;
public partial class Service
{
    public async Task<bool> SendMessage<TButton>(Update update, string text, TButton[] buttons, CancellationToken cancellation) where TButton : struct, Enum
    {
        var body = new SendMessageRequest(update, text.ToPersianNumber()!);
        body.AddButton(buttons);

        var request = new HttpClientRequest(HttpMethod.Post, Url.Base, Url.SendMessage) { Body = body };
        var response = await client.Send<SendMessageResponse>(request, cancellation);

        return response?.Response?.Ok == true;
    }
}
