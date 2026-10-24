using Common;
using Common.Extension;
using Common.HttpClient;

namespace Bale;

public partial class BaleService : IScoped
{
    public async Task<bool> SendMessage(Update update, string text, CancellationToken cancellation)
    {
        var body = new SendMessageRequest(update, text.ToPersianNumber());
        return await SendRequest(body, cancellation);
    }

    public async Task<bool> SendMessage<TButton>(Update update, string text, TButton[] buttons, CancellationToken cancellation) where TButton : struct, Enum
    {
        var body = new SendMessageRequest(update, text.ToPersianNumber());
        body.AddKeyboard(buttons);
        return await SendRequest(body, cancellation);
    }

    public async Task<bool> RequestContact(Update update, string text, CancellationToken cancellation)
    {
        var body = new SendMessageRequest(update, text.ToPersianNumber());
        body.ReplyMarkup=ReplyMarkup.RequestContact;
        return await SendRequest(body, cancellation);
    }

    public async Task<bool> RemoveKeyboard(Update update, CancellationToken cancellation)
    {
        var body = new SendMessageRequest(update,null);
        body.ReplyMarkup = ReplyMarkup.RemoveKey;
        return await SendRequest(body, cancellation);
    }

    private async Task<bool> SendRequest(SendMessageRequest body, CancellationToken cancellation)
    {
        var request = new HttpClientRequest(HttpMethod.Post, Url.Base, Url.SendMessage) { Body = body };
        var response = await client.Send<SendMessageResponse>(request, cancellation);
        return response?.Response?.Ok == true;
    }
}
