using System.Runtime.CompilerServices;

namespace LibCore;

public static class Log
{
    public static void Info(string msg,
                            [CallerFilePath] string fullFilePath = "",
                            [CallerLineNumber] int lineNumber = 0)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"[Info]({Path.GetFileName(fullFilePath)}:{lineNumber}) {msg}");
    }

    public static void Warning(string msg,
                               [CallerFilePath] string fullFilePath = "",
                               [CallerLineNumber] int lineNumber = 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[Warning]({Path.GetFileName(fullFilePath)}:{lineNumber}) {msg}");
    }

    public static void Error(string msg,
                             [CallerFilePath] string fullFilePath = "",
                             [CallerLineNumber] int lineNumber = 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Error]({Path.GetFileName(fullFilePath)}:{lineNumber}) {msg}");
    }

    public static void Exception(Exception e,
                                 [CallerFilePath] string fullFilePath = "",
                                 [CallerLineNumber] int lineNumber = 0)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"[Exception]({Path.GetFileName(fullFilePath)}:{lineNumber}) {e.StackTrace}");
    }
}