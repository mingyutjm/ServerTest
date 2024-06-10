using System.Runtime.CompilerServices;

namespace Server3;

public static class Log
{
    public static void Info(string msg,
                            [CallerFilePath] string fullFilePath = "",
                            [CallerMemberName] string memberName = "",
                            [CallerLineNumber] int lineNumber = 0)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(
            $"[Info]({Path.GetFileNameWithoutExtension(fullFilePath)}.{memberName}: {lineNumber}) {msg}");
    }

    public static void Warning(string msg,
                               [CallerFilePath] string fullFilePath = "",
                               [CallerMemberName] string memberName = "",
                               [CallerLineNumber] int lineNumber = 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(
            $"[Warning]({Path.GetFileNameWithoutExtension(fullFilePath)}.{memberName}: {lineNumber}) {msg}");
    }

    public static void Error(string msg,
                             [CallerFilePath] string fullFilePath = "",
                             [CallerMemberName] string memberName = "",
                             [CallerLineNumber] int lineNumber = 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(
            $"[Error]({Path.GetFileNameWithoutExtension(fullFilePath)}.{memberName}: {lineNumber}) {msg}");
    }

    public static void Exception(Exception e,
                                 [CallerFilePath] string fullFilePath = "",
                                 [CallerMemberName] string memberName = "",
                                 [CallerLineNumber] int lineNumber = 0)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(
            $"[Exception]({Path.GetFileNameWithoutExtension(fullFilePath)}.{memberName}: {lineNumber}) {e.StackTrace}");
    }
}