namespace login;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Start Login.");
        LoginApp app = new LoginApp();
        app.InitApp();
        app.StartAllThread();
        app.Run();
        app.Shutdown();
        Console.WriteLine("Login shutdown.");
        Console.ReadKey();
    }
}