using System;
//using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PSO2emergencyGetter
{
    class logOutput
    {
        //static StreamWriter writer;
        //static FileStream stream;
        private static string filename;
        private static DateTime dt;
        private static string date;
        private static string time;

        public static void writeLog(string str)
        {
            if(filename == null)
            {
                filename = @"config/log.txt";
            }

            if(File.Exists(filename) == false)
            {
                string directory = Regex.Replace(filename, "/.+$", string.Empty);
                Directory.CreateDirectory(directory);
            }

            dt = DateTime.Now;
            date = dt.ToString("yyyy/MM/dd");
            time = dt.ToString("HH:mm:ss");
            string text = string.Format("[{0} {1}]{2}", date, time, str);

            try
            {
                using (FileStream file = new FileStream(filename, FileMode.Append))
                {
                    using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
                    {
                        writer.WriteLine(text);
                        System.Console.WriteLine(text);
                    }
                }
            }
            catch(FieldAccessException)
            {
                System.Console.WriteLine(text);
                System.Console.WriteLine("ログファイルへの書き込みに失敗しました。");
            }
            catch (System.Security.SecurityException)
            {
                System.Console.WriteLine(text);
                System.Console.WriteLine("ログファイルへのアクセス権がありません。");
            }
        }

        public static void writeLog(string log,params string[] args)
        {
            string str = string.Format(log, args);
            writeLog(str);
        }

        public static void outputPronpt()
        {
            System.Console.Write("PSO2 Discord > ");
        }

        public static void init(string name)
        {
            filename = name;
        }
    }
}
