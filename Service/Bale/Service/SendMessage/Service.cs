using Common;
using Common.Extension;
using Common.HttpClient;

namespace Bale;

public partial class BaleService : IScoped
{
    public async Task<bool> SendMessage(Update update, string text, CancellationToken cancellation)
    {
        var body = new SendMessageRequest(update, text.ToPersianNumber());
        return await SendMessage(body, cancellation);
    }

    public async Task<bool> RemoveKeyboard(Update update, CancellationToken cancellation)
    {
        var body = new SendMessageRequest(update, null)
        {
            ReplyMarkup = ReplyMarkup.RemoveKey
        };
        return await SendMessage(body, cancellation);
    }

    public async Task<bool> SendMessage(SendMessageRequest body, CancellationToken cancellation)
    {
        var request = new HttpClientRequest(HttpMethod.Post, Url.Base, Url.SendMessage) { Body = body };
        var response = await client.Send<SendMessageResponse>(request, cancellation);
        return response?.Response?.Ok == true;
    }
}
