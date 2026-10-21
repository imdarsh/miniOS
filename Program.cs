class Program
{
    public static void Main(string[] args)
    {
        int NumberOfProcess;
        Console.WriteLine("How many processes do you want to create?");
        NumberOfProcess = Convert.ToInt32(Console.ReadLine());
        for(int i = 1; i <= NumberOfProcess; i++)
        {
            Console.WriteLine($"Enter details for process {i}: ");
            Console.Write("ID: ");
        }
        Console.WriteLine($"Process Created: \nID:{p1.Id}\nName:{p1.Name}\nBurstTime:{p1.BurstTime}\nPriority:{p1.Priority}\nState:{p1.State}");
    }
}