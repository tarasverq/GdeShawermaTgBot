using GdeShawerma.Db;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GdeShawerma.Core.Db;

[UsedImplicitly]
internal class LogUserHandler : IRequestHandler<LogUserRequest>
{
    private readonly ILogger<LogUserHandler> _logger;
    private readonly TgBotContext _context;
    private readonly Config _config;

    public LogUserHandler(ILogger<LogUserHandler> logger, TgBotContext context, IOptions<Config> config)
    {
        _logger = logger;
        _context = context;
        _config = config.Value;
    }


    public async Task<Unit> Handle(LogUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            DbUser user = await _context.Users
                .WithPartitionKey(_config.BotId)
                .FirstOrDefaultAsync(x => x.Id == request.User.Id.ToString(), cancellationToken);
            if (user != null)
            {
                user.LastMessage = DateTimeOffset.UtcNow;
                user.Username = request.User.Username;
                if (request.Location is not null)
                {
                    user.LastLocation = request.Location.Adapt<DbLocation>();
                    user.LastLocation.Date = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                user = new()
                {
                    Id = request.User.Id.ToString(),
                    JoinedAt = DateTimeOffset.UtcNow,
                    LastMessage = DateTimeOffset.UtcNow,
                    Username = request.User.Username,
                    BotId = _config.BotId,
                    LastLocation = request.Location.Adapt<DbLocation>()
                };
                await _context.Users.AddAsync(user, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Error when saving user");
        }

        return Unit.Value;
    }
}