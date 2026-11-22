public class ProcessCanceledEventArgs : EventArgs
{
    public int ProcessId { get; }
    public string? ProcessName { get; }
    public DateTime CancellationTime { get; }

    public ProcessCanceledEventArgs(int processId, string processName, DateTime cancellationTime)
    {
        ProcessId = processId;
        ProcessName = processName;
        CancellationTime = cancellationTime;
    }
}