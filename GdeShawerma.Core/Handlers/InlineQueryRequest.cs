namespace GdeShawerma.Core.Handlers;

internal class InlineQueryRequest : IRequest
{
    public InlineQuery InlineQuery { get; }

    public InlineQueryRequest(InlineQuery query)
    {
        InlineQuery = query;
    }
}