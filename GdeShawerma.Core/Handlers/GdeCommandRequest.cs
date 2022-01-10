using GdeShawerma.Core.Resources;

namespace GdeShawerma.Core.Handlers;

internal class GdeCommandRequest : IRequest<Message>
{
    public long ChatId { get; }

    public GdeCommandRequest(long chatId)
    {
        ChatId = chatId;
    }
}