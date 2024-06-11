using robot;
using Server3;

class Program
{
    static void Main()
    {
        Console.WriteLine("Robot start!");

        // int threadNum = 1;
        // System.Random rand = new Random();
        //
        // List<ClientThread> clients = new List<ClientThread>(threadNum);
        // for (int i = 0; i < threadNum; i++)
        // {
        //     var client = new ClientThread(rand.Next(0, 10));
        //     client.Start();
        //     clients.Add(client);
        // }
        //
        // Log.Info($"online socket num: {clients.Count}, completed: {threadNum - clients.Count}");
        //
        // while (clients.Count > 0)
        // {
        //     for (int i = clients.Count - 1; i >= 0; i--)
        //     {
        //         ClientThread client = clients[i];
        //         if (!client.IsRun)
        //         {
        //             client.Dispose();
        //             clients.RemoveAt(i);
        //             Log.Info($"online socket num: {clients.Count}, completed: {threadNum - clients.Count}");
        //         }
        //     }
        //     System.Threading.Thread.Sleep(100);
        // }

        Client client = new Client(5);
        if (!client.Connect("127.0.0.1", 2233))
        {
            Log.Error("ClientThread Connect failed.");
        }
        while (true)
        {
            client.Tick();
            client.DataHandler();
            if (client.IsCompleted)
                break;
            System.Threading.Thread.Sleep(1000);
        }
        Log.Error("Robot end.");
        Console.ReadKey();
    }
}