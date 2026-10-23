using Common;
using Common.Extension;
using Common.HttpClient;

namespace Bale;

public partial class BaleService : IScoped
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



public class SendMessageRequest
{
    public SendMessageRequest(Update update, string text)
    {
        ChatId = update.Message!.Chat.Id;
        Text = text;
    }

    public void AddButton<TButton>(TButton[] buttons) where TButton : struct, Enum
    {
        var labels = buttons.Select(x => MessageResource.ResourceManager.GetString(x.ToString())!).ToArray();
        ReplyMarkup = new ReplyMarkup([labels]);
    }


    public long ChatId { get; }
    public string Text { get; }
    public ReplyMarkup? ReplyMarkup { get; private set; }
}
public record ReplyMarkup(string[][] Keyboard, bool ResizeKeyboard = true, bool IsPersistent = true);



public class SendMessageResponse
{
    public bool Ok { get; set; } = false;

    public Message? Result { get; set; }
}