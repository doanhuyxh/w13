using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helper.Logging
{
    public static class LogServices
    {
        public static void LogService(string content, string fname = "LogServices")
        {
            string url =  ConfigurationManager.AppSettings["LogServiceUrl"];
            string fileName = @"\" + fname + "_" + DateTime.Now.ToString("dd_MM_yyyy") + "_Log.txt";
            FileStream fs = new FileStream(url + fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("------------- Logs on "+DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy")+ " -------------");
            sw.WriteLine(content);
            sw.WriteLine("----------------------------- End logs ----------------------------------");
            sw.Flush();
            sw.Close();
        }
    }
}
