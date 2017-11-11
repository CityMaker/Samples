using System.IO;
using System;
using System.Runtime.InteropServices;

namespace Gvitech
{
    public delegate void ShowInfoHandler(string message,LogLevel level);
    public enum LogLevel
    {
        Message = 0,
        Worning = 1,
        Error = 2
    }
    public class Logger
    {
        public static event ShowInfoHandler ShowInfoEvent;
        static String logPath = "";
        private Logger() { }
        public static bool Create(String path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                logPath = String.Format("{0}\\DebugLog.txt", path);
                if (!File.Exists(logPath))
                {
                    FileStream fs = File.Create(logPath);
                    fs.Close();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        private static void Write(string s,LogLevel level)
        {
            if (ShowInfoEvent != null)
                ShowInfoEvent(s, level);
        }
        public static void WriteDebug(string message)
        {
            try
            {
                Write(message, LogLevel.Message);
            }
            catch (System.Exception ex)
            {
            }
        }

        public static void WriteMsg(String str)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine(str);
                    sw.Close();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void WriteMsg(LogLevel level, String str, DateTime now)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    String message = String.Format("Level:{0};Time:{1};Message:{2}", level, now, str);
                    sw.WriteLine(message);
                    sw.Close();
                    Write(message, level);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void WriteException(Exception e)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    string strExp = String.Format("Level:{0};Time:{1};Position:{2}", LogLevel.Error, DateTime.Now, e.Message);
                    sw.WriteLine(strExp);
                    sw.WriteLine(e.Message);
                    sw.WriteLine(e.StackTrace);
                    sw.Close();
                    Write(strExp, LogLevel.Error);
                }     
            }
            catch (System.Exception ex)
            {
            }
        }

    }
}