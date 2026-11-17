namespace miniOS.Services;

public class NotificationService
{
    public void Show(object? sender, ProcessCompletedEventArgs e)
    {
        Console.WriteLine($"Notification: {e.ProcessName} has completed execution!");
    }
}