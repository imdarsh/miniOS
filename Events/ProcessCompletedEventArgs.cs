public class ProcessCompletedEventArgs : EventArgs
{
    public int ProcessId { get; }
    public string? ProcessName { get; }
    public DateTime CompletionTime { get; }

    public ProcessCompletedEventArgs(int processId, string processName, DateTime completionTime)
    {
        ProcessId = processId;
        ProcessName = processName;
        CompletionTime = completionTime;
    }
}