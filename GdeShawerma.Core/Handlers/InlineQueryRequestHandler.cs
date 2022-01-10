namespace GdeShawerma.Core.Handlers;

internal class InlineQueryRequestHandler : IRequestHandler<InlineQueryRequest>
{
    public InlineQueryRequestHandler()
    {
        
    }
    
    public Task<Unit> Handle(InlineQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}