public class Process
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BurstTime { get; private set; }
    public int Priority { get; set; }
    public ProcessState State {get; private set; }
    private readonly object _lock = new object();

    public Process(int id, string name, int burstTime = 0, int priority = 1, ProcessState state = ProcessState.Created)
    {
        Id = id;
        Name = name;
        Priority = priority;
        State = state;
        if(burstTime > 0)
        {
            BurstTime = burstTime;
        } else
        {
            throw new InvalidProcessException("BurstTime must be greater than zero.");
        }
    }

    // Method
    public async Task ExecuteProcessAsync()
    {
 
        lock (_lock)
        {
            if(State == ProcessState.Created)
            {
                State = ProcessState.Running;
            } else
            {
                throw new InvalidOperationException("Process cannot be executed in its current state.");
            }
        } 
         try
        {
            await Task.Delay(BurstTime * 1000);
        } catch
        {
            lock (_lock)
            {                
                State = ProcessState.Failed;
            }
            throw;
        }
        TerminateProcess();
    }
    public void PauseProcess(int id)
    {
        Console.WriteLine($"Process {id} Paused!");
    }

    public void TerminateProcess()
    {
        if(State != ProcessState.Terminated)
        {
            State = ProcessState.Terminated;
            NotifyProcessCompleted(new ProcessCompletedEventArgs(Id, Name, DateTime.Now));
        }
    }

    public void BrokenLogger(object? sender, ProcessCompletedEventArgs e)
    {
        throw new Exception("Logger crashed!");
    }

    private void NotifyProcessCompleted(ProcessCompletedEventArgs eventArgs)
    {
        Delegate[] handlers = ProcessCompleted?.GetInvocationList() ?? Array.Empty<Delegate>();
        foreach(var handler in handlers)
        {
            try
            {
                var callBack = (EventHandler<ProcessCompletedEventArgs>)handler;
                callBack(this, eventArgs);
            } catch(Exception e)
            {
                Console.WriteLine($"Subscriber failed: {e.Message}");
            }
        }
    }

    // Events
    public event EventHandler<ProcessCompletedEventArgs>? ProcessCompleted;
}