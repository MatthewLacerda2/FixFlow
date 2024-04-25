namespace Server.Services;

public class EmailSettings
{
    public int Port { get; set; } = 0;

    public string Host { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string FromAddress { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}
