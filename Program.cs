using System;
using System.IO;
using System.Threading;
class FolderSync 
{ 
    static void Main(string[] args) 
    {
        if (args.Length != 4) 
        {
            Console.WriteLine("Invalid command line arguments, please correct the formatting and try again"); 
            Console.WriteLine("FolderSync.exe <source> <target> <intervalSec> <logPath>");
            string sourcePath = args[0];
            if (!Directory.Exists(sourcePath)) 
            {
                Console.WriteLine($"Source directory '{sourcePath}' does not exist.");
                return;
            }
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            string targetPath = args[1];
            if (!Directory.Exists(targetPath)) 
            {
                Console.WriteLine($"Target directory '{targetPath}' does not exist.");
                return;
            }
            DirectoryInfo targetDir = new DirectoryInfo(targetPath);
            int intervalSec = int.Parse(args[2]);
            string logPath = args[3];

            return;
        }
    }
}
