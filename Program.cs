using miniOS.Delegates;
using miniOS.Services;

class Program
{
    public static void Main(string[] args)
    {
        // List<Process> processes = new List<Process>();

        // processes.Where(process => process.State == ProcessState.Ready)
        //     .OrderBy(process => process.Priority).Select(process => process.Name);
        // bool hasBurstTime = processes.Any(process => process.BurstTime > 10);
        Process chrome = new Process(1, "Chrome", 15);
        Process vscode = new Process(2, "VS Code", 8);
        Process process = new Process(3, "Process 3", 3, 2, ProcessState.Created);
        Logger logger = new Logger();
        NotificationService notifications = new NotificationService();
        
        process.ProcessCompleted += logger.Log;
        process.ProcessCompleted += process.BrokenLogger;
        process.ProcessCompleted += notifications.Show;

        process.TerminateProcess();
        process.TerminateProcess();
    }
}