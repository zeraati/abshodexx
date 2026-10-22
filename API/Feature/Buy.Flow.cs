using Bale;

namespace API;

public class BuyFlowHandler : IFlowHandler
{
    public Flow Flow => Flow.Buy;

    public string Handle(Update update, ConversationState state)
    {
        string message = state.Step switch
        {
            Step.WaitingForEnterGeram => HandleGeram(update, state),
            Step.WaitingForConfirmOrder =>HandleConfirmation(update, state),
            _ => "وضعیت خرید نامعتبر است."
        };

        return message;
    }

    private string HandleGeram(Update update, ConversationState state)
    {
        var geram = update.Message!.Number;
        if (geram.GetValueOrDefault() <= 0) return "مقدار نامتعبر است، لطقا مقدار معتبری وارد نمایید";

        state.Update(state.Flow,Step.WaitingForConfirmOrder, geram.Value);
        return $"خرید {geram} گرم\nیک برای تایید عدد یک و برای لغو عدد دو را ارسال کنید";
    }
    
    private string HandleConfirmation(Update update,ConversationState state)
    {
        if (update.Message!.Text == "1" || update.Message!.Text=="۱")
        {
            // TODO: Create buy order using geram

            state.Clear();
            return $"{state.Geram} گرم خریداری شد.";
        }

        if (update.Message!.Text == "2" || update.Message!.Text == "۲")
        {
            state.Clear();
            return "درخواست خرید لغو شد.";
        }

        return $"خرید {state.Geram} گرم\nیک برای تایید عدد یک و برای لغو عدد دو را ارسال کنید";
    }
}