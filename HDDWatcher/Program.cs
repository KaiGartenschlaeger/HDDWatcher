using System;
using System.IO;

namespace HDDWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "HDD Watcher";
            Console.CursorVisible = false;

            Console.SetBufferSize(800, 10000);

            Console.ForegroundColor = ConsoleColor.White;

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo info in drives)
            {   
                if (info.DriveType == DriveType.Fixed)
                {
                    FileSystemWatcher watcher = new FileSystemWatcher(info.RootDirectory.FullName, "*.*");
                    watcher.Created += watcher_Created;
                    watcher.Deleted += watcher_Deleted;
                    watcher.Renamed += watcher_Renamed;
                    watcher.Error += watcher_Error;
                    watcher.EnableRaisingEvents = true;
                    watcher.IncludeSubdirectories = true;

                    Console.WriteLine("Überwache Laufwerk {0} ({1})",
                        info.VolumeLabel,
                        info.Name);
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            do
            {

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        static void watcher_Error(object sender, ErrorEventArgs e)
        {
        }

        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[+] {0}", e.FullPath);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[-] {0}", e.FullPath);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[=] {0} => {1}", e.OldFullPath, e.FullPath);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}