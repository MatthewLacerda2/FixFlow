/// <summary>
/// Service for 'Contacting' Clients via email
/// </summary>

public class EmailContactsPeriodic : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Your email sending logic goes here
            Console.WriteLine("Sending unique email...");

            // Simulate sending an email by waiting for a short duration
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }

}