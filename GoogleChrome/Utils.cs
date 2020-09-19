using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace GoogleChrome
{
    public class Utils
    {

        public static void ExecuteCommand(string command)
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

        

        public static string ExecuteCommandWithResult(string command)
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



        #region 文件类工具
        public static void FileHanler(string fileName)
        {
            if (!Directory.Exists(Setting.Dir))
            {
                Directory.CreateDirectory(Setting.Dir);
            }
            string path = Path.Combine(Setting.Dir, fileName);
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }


        public static List<string> LoadKeys()
        {
            FileHanler(Setting.KeyFileName);
            Setting.KeyCount = 0;
            FileStream keyFileStream = new FileStream(Setting.KeyPath, FileMode.Open, FileAccess.Read);
            StreamReader keyReader = new StreamReader(keyFileStream, System.Text.Encoding.Default);
            var keys = new List<string>();
            string line;
            while ((line = keyReader.ReadLine()) != null)
            {
                keys.Add(line.Trim());
                Setting.KeyCount += 1;
            }
            keyReader.Close();
            keyFileStream.Close();
            if (!Setting.Normal)
            {
                Random rnd = new Random();
                int rndIndex = rnd.Next(0, keys.Count);
                string rndKey = keys[rndIndex];
                keys.Clear();
                keys.Add(rndKey);
            }
            return keys;
        }

        public static bool CheckAuth()
        {
            try
            {
                HttpGet(Setting.Auth);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static List<string> LoadUA()
        {
            FileHanler(Setting.UAFileName);
            FileStream stream = new FileStream(Setting.UAPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
            var uas = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                uas.Add(line.Trim());
            }
            reader.Close();
            stream.Close();
            return uas;
        }

        public static string GetRandomUA()
        {
            if (Setting.UAs == null )
                Setting.UAs = LoadUA();
            if(Setting.UAs.Count < 1)
                return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36";

            Random rnd = new Random();
            int index = rnd.Next(0, Setting.UAs.Count);
            return Setting.UAs[index];
        }




        public static List<string> GetIPList(int days)
        {
            FileHanler(Setting.IPFileName);
            var ips = new List<string>();
            for (int i = 0; i < days; i++)
            {
                string filePath = Path.Combine(Setting.Dir, $"{DateTime.Now.AddDays(-i) :yyyy-MM-dd}IP.txt");
                if (!File.Exists(filePath))
                {
                    continue;
                }
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ips.Add(line.Trim());
                }
                reader.Close();
                stream.Close();
            }
            return ips;
        }

        public static void AddUsedIP(string ip)
        {
            FileStream stream = new FileStream(Setting.IPPath, FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(ip);
            writer.Close();
            stream.Close();
        }
        #endregion

        public static void LoadADSL()
        {
            FileHanler(Setting.ADSLFileName);
            FileStream stream = new FileStream(Setting.ADSLPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
            var results = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                results.Add(line.Trim());
            }
            reader.Close();
            stream.Close();
            if(results.Count > 2)
            {
                Setting.ADSL = results[0];
                Setting.ADSLUser = results[1];
                Setting.ADSLPassword = results[2];
            }
            
        }

        public static void SaveADSL()
        {
            FileHanler(Setting.ADSLFileName);
            FileStream stream = new FileStream(Setting.ADSLPath, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(Setting.ADSL);
            writer.WriteLine(Setting.ADSLUser);
            writer.WriteLine(Setting.ADSLPassword);
            writer.Close();
            stream.Close();
        }


        public static bool CheckSearchUrl(string url)
        {
            if(url.StartsWith("https://www.baidu.com") || url.StartsWith("http://www.baidu.com/"))
            {
                return true;
            }
            return false;
        }


        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Linux; Android 5.0; SM-G900P Build/LRX21T) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.86 Mobile Safari/537.36";
            request.Method = "GET";
            request.Accept = "application/json, text/plain, */*";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}
