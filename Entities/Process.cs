public class Process
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BurstTime { get; private set; }
    public int Priority { get; set; }
    public ProcessState State {get; set; }

    // public Process(int burstTime = 0)
    // {
    //     if(burstTime > 0)
    //     {
    //         BurstTime = burstTime;
    //     } else
    //     {
    //         throw new InvalidProcessException("BurstTime must be greater than zero.");
    //     }
    // }
    
    public void PauseProcess(int Id)
    {
        Console.WriteLine($"Process {Id} Paused!");
    }
}

public delegate void ProcessAction(int Id);
