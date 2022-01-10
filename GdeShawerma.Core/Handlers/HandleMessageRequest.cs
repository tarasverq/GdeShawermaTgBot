namespace GdeShawerma.Core.Handlers;

internal class HandleMessageRequest : IRequest
{
    public Message Message { get; }

    public HandleMessageRequest(Message message)
    {
        Message = message;
    }
}