using System.Net;
using System.Net.Mail;
using Server.Data;

namespace Server.Services;

/// <summary>
/// Service for 'Contacting' Clients via email
/// </summary>
public class EmailContactsPeriodic : BackgroundService
{
    private readonly SmtpClient _smtpClient;
    private readonly ServerContext _context;

    const string EmailAddress = "my_email_address@hotmail.com";
    const string EmailPassword = "mystrongpassword1234";

    public EmailContactsPeriodic(ServerContext context)
    {
        _context = context;

        _smtpClient = new SmtpClient("my_smtp_server")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(EmailAddress, EmailPassword),
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await SendEmailToContactsAsync();
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }

    private async Task SendEmailToContactsAsync()
    {
        var contacts = _context.Contacts
            .Where(c => c.dateTime.Date == DateTime.Today.Date)
            .ToArray();

        foreach (var contact in contacts)
        {
            if (contact.Client.Email == null)
            {
                continue;
            }

            var mailMessage = new MailMessage(EmailAddress, contact.Client.Email, "Reminder", "Your message here");
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}