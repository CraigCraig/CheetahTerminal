namespace Tests;

public class Program
{
    static void Main()
    {
        List<Task> tasks = [];

        tasks.Add(Task.Run(() =>
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                Console.WriteLine(key);
            }
        }));
        tasks.Add(Task.Run(() =>
        {
            while (true)
            {
                Console.WriteLine("Test");
                Thread.Sleep(1);
            }
        }));
        Task.WaitAll([.. tasks]);
    }
}