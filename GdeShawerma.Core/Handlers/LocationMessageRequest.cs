namespace GdeShawerma.Core.Handlers;

internal class LocationMessageRequest : IRequest<Message>
{
    public Message Message { get; }

    public LocationMessageRequest(Message message)
    {
        Message = message;
    }
}