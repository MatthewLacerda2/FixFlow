namespace Server.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
}