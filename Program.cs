using System;
using System.IO;
using System.Threading;
class FolderSync 
{
    static void LogMessage(string logPath, string message)
    {
        using (StreamWriter writer = new StreamWriter(logPath, append: true))
        {
            writer.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
    static void Main(string[] args) 
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Invalid command line arguments, please correct the formatting and try again");
            Console.WriteLine("FolderSync.exe <source> <target> <intervalSec> <logPath>");
            return;
        }
        // Log setup
        string logPath = args[3];
        string logDir = Path.GetDirectoryName(logPath);
        if (!Directory.Exists(logDir))
        {
            Directory.CreateDirectory(logDir);
        }

        // Write to log that the program has started and log the arguments
        LogMessage(logPath, "Program started.");

        string sourcePath = args[0];        
        if (!Directory.Exists(sourcePath))         
        {        
            Console.WriteLine($"Source directory '{sourcePath}' does not exist.");            
            return;            
        }        
        DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);

        //Write to log that the source directory exists and log the path
        LogMessage(logPath, $"Source directory exists: {sourcePath}");

        string targetPath = args[1];        
        if (!Directory.Exists(targetPath))         
        {        
            Console.WriteLine($"Target directory '{targetPath}' does not exist.");            
            return;            
        }        
        DirectoryInfo targetDir = new DirectoryInfo(targetPath);

        //Write to log that the target directory exists and log the path
        LogMessage(logPath, $"Target directory exists: {targetPath}");

        int intervalSec = int.Parse(args[2]);       
        FileInfo[] sourceFiles = sourceDir.GetFiles("*", SearchOption.AllDirectories);
        FileInfo[] targetFiles = targetDir.GetFiles("*", SearchOption.AllDirectories);
        return;
    }
}
