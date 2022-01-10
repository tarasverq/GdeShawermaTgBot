using GdeShawerma.Core.Api;
using GdeShawerma.Core.Api.Dtos;
using GdeShawerma.Core.Db;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.ReplyMarkups;

namespace GdeShawerma.Core.Handlers;

[UsedImplicitly]
internal class LocationMessageRequestHandler : IRequestHandler<LocationMessageRequest, Message>
{
    private readonly IMediator _mediator;
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<LocationMessageRequestHandler> _logger;

    public LocationMessageRequestHandler(IMediator mediator, ITelegramBotClient botClient,
        ILogger<LocationMessageRequestHandler> logger)
    {
        _mediator = mediator;
        _botClient = botClient;
        _logger = logger;
    }

    public async Task<Message> Handle(LocationMessageRequest request, CancellationToken cancellationToken)
    {
        var location = request.Message.Location
                       ?? throw new NullReferenceException("Location is not provided");
        await _botClient.SendChatActionAsync(request.Message.Chat.Id, ChatAction.FindLocation, 
            cancellationToken: cancellationToken);
        await _mediator.Send(new LogUserRequest(request.Message.From, location), cancellationToken);

        List<Restaurant> result;
        try
        {
            result = await _mediator.Send(new GetNearbyShawermaRequest(location.Longitude, location.Latitude),
                cancellationToken);
        }
        catch (Exception e)
        {
            return await _botClient.SendTextMessageAsync(chatId: request.Message.Chat.Id, text: Texts.ErrorNearby,
                replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);
        }
        
        if(!result.Any())
        {
            return await _botClient.SendTextMessageAsync(request.Message.Chat.Id,
                Texts.NothingFound, replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }

        var mean = result.Average(x => x.Rate);
        foreach (Restaurant restaurant in result.OrderByDescending(x => GetWeightedRating(x, mean)))
        {
            await _mediator.Send(new SendRestaurantInfoRequest(request.Message.Chat.Id, restaurant, location),
                cancellationToken);
        }

        var message = await _botClient.SendTextMessageAsync(request.Message.Chat.Id,
            Texts.Repeat, replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
        return message;
    }


    private double GetWeightedRating(Restaurant restaurant, double mean)
    {
        // https://stackoverflow.com/a/1411268/2789641
        /*
            weighted rating (WR) = (v ÷ (v+m)) × R + (m ÷ (v+m)) × C

            where:

            R = average for the movie (mean)
            v = number of votes for the movie
            m = minimum votes required to be listed in the Top 250 (currently 25000)
            C = the mean vote across the whole report (currently 7.0)

            For the Top 250, only votes from regular voters are considered.
        */
        var r = restaurant.Rate;
        var v = restaurant.RatesCount;
        const double m = 1.0;
        var c = mean;
        // ReSharper disable once PossibleLossOfFraction
        var wr = (v / (v + m)) * r + (m / (v + m)) * c;
        return wr;
    }
}