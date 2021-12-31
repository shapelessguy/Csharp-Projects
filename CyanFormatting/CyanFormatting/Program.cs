using System;
using System.IO;

namespace CyanFormatting
{
    class Program
    {
        static bool invert = false;     // Se true verranno invertite le folder source e destination
        static bool verbose = false;    // Se true verrà mostrata in console ogni singola copia
        static string Username = "39339";   // Nome utente windows

        static void Main(string[] args)
        {
            Console.WriteLine("Premi 'import', oppure 'copy' se hai fatto queste cosine qui:");
            Console.WriteLine(" - Salvato le impostazioni di DisplayFusion e PDF Annotator");
            Console.WriteLine(" - Fatto 2 screen: configurazione mouse e menù");
            Console.WriteLine(" Il tutto dovresti tenerlo nella cartella di formattazione");
            Console.WriteLine();

            string inp = Console.ReadLine();
            if (inp == "copy" || inp == "import")
            {
                if (inp == "copy") invert = false; else invert = true;
                Console.WriteLine("Inserisci il nome utente:");
                Username = Console.ReadLine();
                CopyDocuments(@"C:\Users\"+Username+@"\Documents\", @"E:\Formattazione\Documents Structure\");
                CopyAll(@"C:\Users\"+Username+@"\Pictures\", @"E:\Formattazione\Other Structures\Immagini\");
                CopyAll(@"C:\Users\"+Username+@"\Videos\", @"E:\Formattazione\Other Structures\Video\");
                CopyAll(@"C:\Users\"+Username+@"\Downloads\", @"E:\Formattazione\Other Structures\Download\");
                CopyAll(@"C:\Users\"+Username+@"\Desktop\", @"E:\Formattazione\Other Structures\Desktop\");
                CopyAll(@"C:\ProgramData\Cyan\", @"E:\Formattazione\Cyan\");
                CopyAll(@"C:\Users\"+Username+@"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\", 
                  @"E:\Formattazione\Es.Automatica\");
            }



            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
        }







        static void CopyDocuments(string path_in, string path_out)
        {
            string pathIn = path_in;
            string pathOut = path_out;
            if (invert) { pathIn = path_out; pathOut = path_in; }
            if (!Directory.Exists(pathOut)) Directory.CreateDirectory(pathOut);

            foreach (var str in Directory.GetDirectories(pathIn))
            {
                if (str == @"C:\Users\39339\Documents\Immagini" || str == @"C:\Users\39339\Documents\Musica" ||
                    str == @"C:\Users\39339\Documents\Video") continue;
                try
                {
                    string str1 = Path.GetFileName(str);
                    if (!Directory.Exists(pathOut + str1)) Directory.CreateDirectory(pathOut + str1);
                    CopyEntireDirectory(pathIn + str1, pathOut + str1);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            CopyOnlyFiles(pathIn, pathOut);
            CopyOnlyLinks(pathIn, pathOut);
            Console.WriteLine("Successfully copied " + pathIn);
        }

        static void CopyAll(string path_in, string path_out)
        {
            string pathIn = path_in;
            string pathOut = path_out;
            if (invert) { pathIn = path_out; pathOut = path_in; }
            //foreach (var str in Directory.GetDirectories(path_in))
            {
                try
                {
                    if (!Directory.Exists(pathOut)) Directory.CreateDirectory(pathOut);
                    //string str1 = Path.GetFileName(str);
                    CopyEntireDirectory(pathIn, pathOut);
                    Console.WriteLine("Successfully copied " + pathIn);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            //CopyOnlyFiles(path_in, path_out);
            //CopyOnlyLinks(path_in, path_out);
        }



        static void CopyOnlyLinks(string path_in, string path_out)
        {
            foreach (var str in Directory.GetFiles(path_in, "*.*"))
            {
                try
                {
                    string str1 = Path.GetFileName(str);
                    if (!str1.Contains(".lnk")) continue;
                    if (verbose) Console.WriteLine("Copied: " + path_in + str1);
                    File.Copy(path_in + str1, path_out + str1);
                }
                catch (Exception e) { if(!e.Message.Contains("already exists")) Console.WriteLine(e.Message); }
            }
        }
        static void CopyOnlyFiles(string path_in, string path_out)
        {
            foreach (var str in Directory.GetFiles(path_in, "*.*"))
            {
                try
                {
                    string str1 = Path.GetFileName(str);
                    if (str1.Contains(".lnk")) continue;
                    if (verbose) Console.WriteLine("Copied: " + path_in + str1);
                    File.Copy(path_in + str1, path_out + str1);
                }
                catch (Exception e) { if (!e.Message.Contains("already exists")) Console.WriteLine(e.Message); }
            }
        }
        static void CopyEntireDirectory(string SourcePath, string DestinationPath)
        {
            //Now Create all of the directories
            Console.WriteLine(SourcePath);
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
                    if (verbose) Console.WriteLine("Copied: " + dirPath);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
                    if (verbose) Console.WriteLine("Copied: " + newPath);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }
    }
}
