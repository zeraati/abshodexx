using Common.Extension;

namespace Bale;

public class Update
{
    public long UpdateId { get; set; }

    public Message? Message { get; set; }

    public string? Username => Message?.From?.Username;
    public bool HasMessageText => Message?.Text.Empty() == false;
}

