using Common;

namespace Bale;

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
