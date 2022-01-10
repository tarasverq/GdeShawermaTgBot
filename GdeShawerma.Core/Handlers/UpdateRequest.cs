namespace GdeShawerma.Core.Handlers;

public class UpdateRequest : IRequest
{
    public Update Update { get; }

    public UpdateRequest(Update update)
    {
        Update = update;
    }
}