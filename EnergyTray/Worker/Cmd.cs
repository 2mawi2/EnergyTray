using System.Diagnostics;

namespace EnergyTray.Worker
{
    public class Cmd
    {
        public static void ExecCommand(string command, DataReceivedEventHandler callback = null)
        {
            var cmdProcess = StartCmd();
            if (callback != null)
            {
                cmdProcess.OutputDataReceived += callback;
            }
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.StandardInput.WriteLine(command);
            cmdProcess.StandardInput.WriteLine("exit");
            cmdProcess.WaitForExit();
        }

        private static Process StartCmd()
        {
            var cmdStartInfo = new ProcessStartInfo
            {
                FileName = @"C:\Windows\System32\cmd.exe",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            return new Process {StartInfo = cmdStartInfo};
        }
    }
}