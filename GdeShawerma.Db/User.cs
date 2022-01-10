namespace GdeShawerma.Db;

public class DbLocation
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTimeOffset Date { get; set; }
}


public class DbUser
{
    public string Id { get; set; }
    public string BotId { get; set; }
   
    public string Username { get; set; }
    public DateTimeOffset JoinedAt { get; set; }
    public DateTimeOffset LastMessage { get; set; }
    
    public virtual DbLocation LastLocation { get; set; }
}