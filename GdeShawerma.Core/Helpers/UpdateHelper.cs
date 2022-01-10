namespace GdeShawerma.Core.Helpers;

public static class UpdateHelper
{
    public static long GetChatId(this Update update)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                return update.Message.Chat.Id;
            case UpdateType.CallbackQuery:
                return update.CallbackQuery.From.Id;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}