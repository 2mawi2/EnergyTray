using System;
using System.Threading;

public class ExceptionHandler
{
    public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        HandleException((Exception) e.ExceptionObject);
    }

    public static void ErrorHandler(object sender, ThreadExceptionEventArgs e)
    {
        HandleException(e.Exception);
    }

    public static void HandleException(Exception ex)
    {
        Console.WriteLine($"Exception: {ex}, " +
                          $"message: {ex.Message} , " +
                          $"inner exception: {ex.InnerException}");
    }
}