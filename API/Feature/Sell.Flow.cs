using Bale;

namespace API;

public class SellFlowHandler : IFlowHandler
{
    public Flow Flow => Flow.Sell;

    public string Handle(Update update, ConversationState state)
    {
        string message = state.Step switch
        {
            Step.WaitingForEnterGeram => HandleGeram(update, state),
            Step.WaitingForConfirmOrder =>HandleConfirmation(update, state),
            _ => "وضعیت فروش نامعتبر است."
        };

        return message;
    }

    private string HandleGeram(Update update, ConversationState state)
    {
        var geram = update.Message!.Number;
        if (geram.GetValueOrDefault() <= 0) return "مقدار نامتعبر است، لطقا مقدار معتبری وارد نمایید";

        state.Update(state.Flow, Step.WaitingForConfirmOrder, geram.Value);
        return $"فروش {geram} گرم\nیک برای تایید عدد یک و برای لغو عدد دو را ارسال کنید";
    }
    
    private string HandleConfirmation(Update update,ConversationState state)
    {
        if (update.Message!.Text == "1" || update.Message!.Text=="۱")
        {
            // TODO: Create sell order using geram

            state.Clear();
            return $"{state.Geram} گرم فروخته شد.";
        }

        if (update.Message!.Text == "2" || update.Message!.Text == "۲")
        {
            state.Clear();
            return "درخواست فروش لغو شد.";
        }

        return $"فروش {state.Geram} گرم\nیک برای تایید عدد یک و برای لغو عدد دو را ارسال کنید";
    }
}