using System.Net;
using System.Net.Mail;
using Server.Controllers;
using Server.Data;
using Server.Models.PasswordReset;

namespace Server.Services;

/// <summary>
/// Service used to send the Email with the link for resetting the Password
/// </summary>
public class MailResetPassword : BackgroundService
{
    private readonly SmtpClient _smtpClient;
    private readonly ServerContext _context;

    const string EmailAddress = "my_email_address@hotmail.com";
    const string EmailPassword = "mystrongpassword1234";

    public MailResetPassword(ServerContext context)
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
            Console.WriteLine("EmailResetPassword Alive and well...");
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }

    public async Task SendResetPasswordEmailAsync(PasswordResetRequest pr)
    {
        await SendResetEmailAsync(pr.Email, pr.token);

        await Task.Delay(TimeSpan.FromMinutes(AccountsController.ResetEmailTokenExpirationInMinutes));

        _context.Resets.Remove(pr);
    }

    private async Task SendResetEmailAsync(string to, string token)
    {
        var mailMessage = new MailMessage(EmailAddress, to, "Reminder", "Your token here: " + token);
        await _smtpClient.SendMailAsync(mailMessage);
    }
}