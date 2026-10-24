using Common;
using Common.Extension;
using Common.HttpClient;

namespace Bale;

public partial class BaleService : IScoped
{
    public async Task<bool> SendMessage(Update update, string text, CancellationToken cancellation)
    {
        var body = new SendMessageRequest(update, text.ToPersianNumber()!);
        return await SendRequest(body, cancellation);
    }

    public async Task<bool> SendMessage<TButton>(Update update, string text, TButton[] buttons, CancellationToken cancellation) where TButton : struct, Enum
    {
        var body = new SendMessageRequest(update, text.ToPersianNumber()!);
        body.AddKeyboard(buttons);
        return await SendRequest(body, cancellation);
    }

    public async Task SendMessage(Update update, string text)
    {
        var body = new SendMessageRequest(update, text.ToPersianNumber()!);

        var keyboard = new
        {
            keyboard = new[]
             {
        new[]
        {
            new { text = "📦 محصولات" },
            new { text = "🛒 سفارشات" }
        },
        new[]
        {
            new { text = "👤 پروفایل" },
            new { text = "☎️ پشتیبانی" }
        }
    }
        };

        var keyboard2 = new
        {
            inline_keyboard = new[]
    {
        new[]
        {
            new
            {
                text = "✅ تأیید",
                callback_data = "order_confirm"
            },
            new
            {
                text = "❌ لغو",
                callback_data = "order_cancel"
            }
        }
    }
        };


        var keyboard3 = new
        {
            keyboard = new[]
    {
        new[]
        {
            new
            {
                text = "📱 ارسال شماره تلفن",
                request_contact = true
            }
        }
    }
        };

        body.ReplyMarkup = keyboard3;
        await SendRequest(body, CancellationToken.None);
    }

    private async Task<bool> SendRequest(SendMessageRequest body, CancellationToken cancellation)
    {
        var request = new HttpClientRequest(HttpMethod.Post, Url.Base, Url.SendMessage) { Body = body };
        var response = await client.Send<SendMessageResponse>(request, cancellation);
        return response?.Response?.Ok == true;
    }
}
