namespace GdeShawerma.Core.Handlers;

internal class TextMessageRequest : IRequest<Message>
{
    public Message Message { get; }

    public TextMessageRequest(Message message)
    {
        Message = message;
    }
}