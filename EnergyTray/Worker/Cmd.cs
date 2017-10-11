using System.Diagnostics;

namespace EnergyTray.Worker
{
    public class Cmd : ICmd
    {
        private readonly string _cmdPath;

        public Cmd(string cmdPath = @"C:\Windows\System32\cmd.exe")
        {
            _cmdPath = cmdPath;
        }

        public void ExecCommand(string command, DataReceivedEventHandler callback = null)
        {
            var cmdProcess = StartCmd();
            if (callback != null)
            {
                cmdProcess.OutputDataReceived += callback;
            }
            ConfigureCommandLine(command, cmdProcess);
        }

        private static void ConfigureCommandLine(string command, Process cmdProcess)
        {
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.StandardInput.WriteLine(command);
            cmdProcess.StandardInput.WriteLine("exit");
            cmdProcess.WaitForExit();
        }

        private Process StartCmd() => new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _cmdPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            }
        };
    }
}