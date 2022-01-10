using GdeShawerma.Core.Api;

namespace GdeShawerma.Core.Handlers;

[UsedImplicitly]
internal class SendRestaurantExtendedInfoRequestHandler : IRequestHandler<SendRestaurantExtendedInfoRequest>
{
    private readonly IMediator _mediator;

    public SendRestaurantExtendedInfoRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(SendRestaurantExtendedInfoRequest request, CancellationToken cancellationToken)
    {
        var restaurant = await _mediator.Send(new GetRestaurantInfoRequest(request.RestaurantId), cancellationToken);

        Message message =
            await _mediator.Send(new SendRestaurantInfoRequest(request.ChatId, restaurant), cancellationToken);
        return Unit.Value;
    }
}