namespace GdeShawerma.Core.Handlers;

internal class UsageMessageRequest : IRequest<Message>
{
    public Message Message { get; }

    public UsageMessageRequest(Message message)
    {
        Message = message;
    }
}