using server;
using Server3;

class Program
{
    static void Main()
    {
        Console.WriteLine("Start server");

        Server server = new Server();
        if (!server.Listen("127.0.0.1", 2233))
            return;

        bool isRun = true;
        while (isRun)
        {
            if (!server.Tick())
                break;
            server.DataHandler();
        }
        server.Dispose();
    }
}