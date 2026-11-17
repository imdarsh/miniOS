namespace miniOS.Services;

public class Logger
{
    public void Log(object? sender, ProcessCompletedEventArgs e)
    {
        Console.WriteLine($"Process {e.ProcessId} ({e.ProcessName}) completed successfully at {e.CompletionTime}!");
    }
}