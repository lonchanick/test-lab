using test_lab;


//working with APIs
//await WorkingWithAPIs.Exe();
//WorkingWithStreams.Exe();

DoSomeWhat.PrintSomeWhat();

class DoSomeWhat
{
    [Obsolete]
    public static void PrintSomeWhat()
    {
        Console.WriteLine("Console.WriteLine");
    }
}