using Server3;

namespace login;

class Program
{
    static void Main(string[] args)
    {
        ServerApp.StartMain(new LoginApp());
    }
}