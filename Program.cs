class Program
{
    public static void Main(string[] args)
    {
        // List<Process> processes = new List<Process>();

        // processes.Where(process => process.State == ProcessState.Ready)
        //     .OrderBy(process => process.Priority).Select(process => process.Name);
        // bool hasBurstTime = processes.Any(process => process.BurstTime > 10);

        Process process = new Process();

        ProcessAction action = process.PauseProcess;

        action(1);
    }
}