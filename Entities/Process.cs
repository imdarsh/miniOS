public class Process
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BurstTime {get; set; }
    public int Priority { get; set; }
    public ProcessState State {get; set; }
}