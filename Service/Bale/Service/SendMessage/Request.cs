using Common;

namespace Bale;

public class SendMessageRequest
{
    public SendMessageRequest(Update update, string? text)
    {
        if (update.CallbackQuery != null) ChatId = update.CallbackQuery!.Message!.Chat.Id;
        else ChatId = update.Message!.Chat.Id;

        Text = text;
    }


    public void AddKeyboard<TButton>(TButton[] buttons) where TButton : struct, Enum
    {
        var texts = buttons.Select(x => MessageResource.ResourceManager.GetString(x.ToString())!).ToArray();

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i] == "خرید") texts[i] = "➕ خرید";
            if (texts[i] == "فروش") texts[i] = "➖ فروش";
            if (texts[i] == "قیمت") texts[i] = "💰 قیمت";
        }

        ReplyMarkup = new ReplyMarkup([texts]);
    }

    public long ChatId { get; }
    public string? Text { get; }
    public ReplyMarkup? ReplyMarkup { get; set; }
}



public class ReplyMarkup
{
    public ReplyMarkup(string[][] keyboard) => Keyboard = keyboard;
    public static ReplyMarkup RemoveKey => new ReplyMarkup { RemoveKeyboard= true};
    public static ReplyMarkup RequestContact => new ReplyMarkup { Keyboard = [[new { Text = "📱 ارسال شماره موبایل", RequestContact = true }]] };
    public ReplyMarkup(InlineKeyboard[][] inlineKeyboard) => InlineKeyboard = inlineKeyboard;

    private ReplyMarkup() { }
    public object[][]? Keyboard { get; private set; }
    public InlineKeyboard[][]? InlineKeyboard { get; private set; }
    public bool ResizeKeyboard { get; private set; } = false;
    public bool IsPersistent { get; private set; } = true;
    public bool? RemoveKeyboard { get; set; }
}

public class InlineKeyboard
{
    public InlineKeyboard(string text,string callbackData)
    {
        Text=text;
        CallbackData=callbackData;
    }

    public static InlineKeyboard Confirm()=>new("✅ تأیید","confirm");
    public static InlineKeyboard Reject()=>new("❌ رد", "reject");

    public string Text { get; set; }
    public string CallbackData { get; set; }
}
