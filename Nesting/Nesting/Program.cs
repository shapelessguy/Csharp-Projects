using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nesting
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string parent = System.IO.Directory.GetCurrentDirectory();
            if (parent.Contains(@"E:\DOCUMENTI\Workspace Visual Studio\Nesting\Nesting\bin\Debug")) return;
            foreach(string file in Directory.GetFiles(parent))
            {
                if (file.Contains("infopowervideos.txt")) continue;
                try
                {
                    string destinationFilePath = Path.Combine(parent, Path.GetFileNameWithoutExtension(file));
                    Directory.CreateDirectory(destinationFilePath);
                    File.Move(file, Path.Combine(destinationFilePath, Path.GetFileName(file)));
                }
                catch (Exception) { }
            }
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
