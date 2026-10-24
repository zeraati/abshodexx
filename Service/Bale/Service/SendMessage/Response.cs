namespace Bale;

public class SendMessageResponse
{
    public bool Ok { get; set; } = false;

    public Message? Result { get; set; }
}