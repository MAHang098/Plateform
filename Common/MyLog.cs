using System;
using System.IO;


namespace Common
{
    public class MyLog
    {
        

        public static void WriteBytes(string section, byte[] data,string mFileName = "log.txt")
        {
            if(data!=null)
            {
                string msg = "";
                foreach (var b in data)
                {
                    msg += " " + b.ToString("X2");
                }
                WriteInfo(section, msg);
            }
            
        }
        public static void WriteInfo(string section, string message, string mFileName = "log.txt")
        {
            string[] lines = new string[2];
            lines[0] = "[ " + DateTime.Now.ToString() + " ][ Info ]-[" + section + "]:";
            lines[1] = message;
            File.AppendAllLines(mFileName, lines);
        }

        public static void WriteWarning(string section, string message, string mFileName = "log.txt")
        {
           
            string[] lines = new string[2];
            lines[0] = "[ " + DateTime.Now.ToString() + " ][ Warnning ]-[" + section + "]:";
            lines[1] = message;
            File.AppendAllLines(mFileName, lines);
        }

        public static void WriteError(string section, string message, string mFileName = "log.txt")
        {
           
            string[] lines = new string[2];
            lines[0] = "[ " + DateTime.Now.ToString() + " ][ Error ]-[" + section + "]:";
            lines[1] = message;
            File.AppendAllLines(mFileName, lines);
        }

        public static void ClearLogs(string mFileName = "log.txt")
        {
            if(File.Exists(mFileName))
            {
                File.Delete(mFileName);
            }
        }
    }
}
