using System.Diagnostics;

namespace EnergyTray.Application.PowerManagement
{
    public class Cmd : ICmd
    {
        private readonly string _cmdPath;

        public Cmd(string cmdPath = @"C:\Windows\System32\cmd.exe")
        {
            _cmdPath = cmdPath;
        }

        public string ExecCommand(string command)
        {
            var cmdProcess = CreateCmdProcess();

            WriteCommand(command, cmdProcess);
            var output = ReadOutput(cmdProcess);

            cmdProcess.WaitForExit();
            return output;
        }

        private Process CreateCmdProcess()
        {
            var cmdProcess = StartCmd();
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            return cmdProcess;
        }

        private static string ReadOutput(Process cmdProcess)
        {
            var output = "";
            string standardOutput;
            while ((standardOutput = cmdProcess.StandardOutput.ReadLine()) != null)
            {
                output += standardOutput + System.Environment.NewLine;
            }
            return output;
        }

        private static void WriteCommand(string command, Process cmdProcess)
        {
            cmdProcess.StandardInput.WriteLine(command);
            cmdProcess.StandardInput.WriteLine("exit");
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