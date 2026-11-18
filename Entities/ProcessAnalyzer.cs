public class ProcessAnalyzer
{
    public int Analyze(List<Process> processes, Predicate<Process> filter, Action<Process> print)
    {
        int count = 0;
        ArgumentNullException.ThrowIfNull(processes);
        ArgumentNullException.ThrowIfNull(filter);
        ArgumentNullException.ThrowIfNull(print);
            foreach(var proc in processes)
            {
                if(filter(proc))
                {
                    print(proc);
                    count++;
                }
            }
            return count;
    }

    public Process HigherBurstTime(Process p1, Process p2, Func<Process, Process, Process> comparer)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);
        ArgumentNullException.ThrowIfNull(comparer);
        return comparer(p1, p2);
    }
}

        