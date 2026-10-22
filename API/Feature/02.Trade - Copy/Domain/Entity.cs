using Bale;
using Common.Entity;
using System.Collections.Concurrent;

namespace API.Feature.Domain;

public partial class ConversationState : BaseEntity
{
    private ConversationState(){ }
    public long UserId { get; set; }

    public Side Side { get; }
    public DateTime? SideAt { get;}

    public int? Lot { get; private set; }
    public DateTime? LotAt { get; private set; }

    public decimal? IndicativePrice { get; private set; } // قیمت اعلامی غیر قطعی
    public DateTime? IndicativePriceAt { get; private set; }

    public decimal? Price { get; private set; }
    public DateTime? PriceAt { get; private set; }
}

public enum Side { Buy, Sell }

public class TradeConfig : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
    }
}


public class 
{
    public ConversationState()
    {
        Flow = Flow.None;
        Step = Step.None;
    }

    public void Update(Flow flow, Step step, long? geram)
    {
        Flow = flow;
        Step = step;
        Geram = geram;
    }

    public void Clear()
    {
        Flow = Flow.None;
        Step = Step.None;
    }

    public Flow Flow { get; private set; }
    public Step Step { get; private set; }
    public long? Geram { get; private set; }
}


public class ConversationStateStore
{
    private readonly ConcurrentDictionary<long, ConversationState> _states = new();
    public ConversationState Get(Update update) =>
        _states.GetOrAdd(update.Message!.Chat.Id, static _ => new ConversationState());

    public bool Clear(Update update) => _states.TryRemove(update.Message!.Chat.Id, out _);
}