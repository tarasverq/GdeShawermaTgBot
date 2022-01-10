namespace GdeShawerma.Core.Db;

internal class LogUserRequest : IRequest
{
    public User User { get; init; }
    public Location Location { get; init; }

    public LogUserRequest(User user, Location location = null)
    {
        User = user;
        Location = location;
    }
}