namespace GdeShawerma.Core;

public class Config
{
    public string ShawermaBaseUrl { get; set; }
    public string BotToken { get; set; }
    public string BotId => BotToken.Split(':')[0];
}