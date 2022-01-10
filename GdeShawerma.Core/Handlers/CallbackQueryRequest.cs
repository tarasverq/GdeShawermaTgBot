namespace GdeShawerma.Core.Handlers;

internal class CallbackQueryRequest : IRequest
{
    public CallbackQuery CallbackQuery { get; }

    public CallbackQueryRequest(CallbackQuery query)
    {
        CallbackQuery = query;
    }
}



