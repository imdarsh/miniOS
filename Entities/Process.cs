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
    public async Task ExecuteProcessAsync(CancellationToken token)
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
            await Task.Delay(BurstTime * 1000, token);
        }
        catch(OperationCanceledException)
        {
            lock(_lock)
            {
                State = ProcessState.Cancelled;
            }
            NotifyProcessCanceled(new ProcessCanceledEventArgs(Id, Name, DateTime.Now));
            throw;
        } 
        catch
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
        bool shouldNotify = false;
        lock (_lock)
        {
            if(State == ProcessState.Running)
            {         
                State = ProcessState.Terminated;
                shouldNotify = true;
            }
        }
        if(shouldNotify)
        { 
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

    private void NotifyProcessCanceled(ProcessCanceledEventArgs eventArgs)
    {
        Delegate[] handlers = ProcessCanceled?.GetInvocationList() ?? Array.Empty<Delegate>();
        foreach(var handler in handlers)
        {
            try
            {
                var callBack = (EventHandler<ProcessCanceledEventArgs>)handler;
                callBack(this, eventArgs);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Subscriber failed: {e.Message}");
            }
        }
    }

    // Events
    public event EventHandler<ProcessCompletedEventArgs>? ProcessCompleted;
    public event EventHandler<ProcessCanceledEventArgs>? ProcessCanceled;
}