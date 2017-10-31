using System;
using System.Diagnostics;
using System.Threading;

namespace EnergyTray.Application.Exceptions
{
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

        private static void HandleException(Exception ex)
        {
            var errorMessage = $"Exception: {ex}, " +
                               $"message: {ex.Message} , " +
                               $"inner exception: {ex.InnerException}";
            
            Debug.WriteLine(errorMessage);
            Trace.WriteLine(errorMessage);
            Console.WriteLine(errorMessage);
        }
    }
}