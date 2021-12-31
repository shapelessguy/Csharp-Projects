using System;
using System.IO;
using System.Threading;

namespace SaveCurrentTime
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string directory = Path.Combine(path, "StartUp_Time");
            string file = Path.Combine(directory, "time.txt");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            if (File.Exists(file)) File.Delete(file);
            using(StreamWriter sw = File.CreateText(file))
            {
                sw.Write(DateTime.UtcNow.ToFileTimeUtc());
            }
            readFile();
        }

        static void readFile()
        {
            DateTime date = DateTime.UtcNow;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string directory = Path.Combine(path, "StartUp_Time");
            string file = Path.Combine(directory, "time.txt");
            Thread.Sleep(1000);
            try
            {
                string text = File.ReadAllText(file);
                try { date = DateTime.FromFileTimeUtc(Convert.ToInt64(text)); } catch (Exception) { }
                Console.WriteLine("Date: " + date);
                Console.WriteLine("Difference: " + DateTime.Now.ToUniversalTime().Subtract(date));
            }
            catch (Exception) { }
        }
    }
}
