using System;
using System.IO;
using System.Threading;
class FolderSync 
{
    static void LogMessage(string logPath, string message)
    {
        string time = $"[{DateTime.Now}] {message}";
        using (StreamWriter writer = new StreamWriter(logPath, append: true))
        {
            writer.WriteLine($"[{DateTime.Now}] {message}");
        }
        Console.WriteLine(time);
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

        // Write to log that the program has started
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
        //Loop according to interval
        while (true)
        {
            FileInfo[] sourceFiles = sourceDir.GetFiles("*", SearchOption.AllDirectories);
            //Log soure files - Delete if unnecessary since it parses every file in source directory
            foreach (FileInfo file in sourceFiles)
            {
                LogMessage(logPath, $"Source file: {file.FullName} | Last modified: {file.LastWriteTime}");
            }
            FileInfo[] targetFiles = targetDir.GetFiles("*", SearchOption.AllDirectories);
            //Log target files - Delete if unnecessary, same reason
            foreach (FileInfo file in targetFiles)
            {
                LogMessage(logPath, $"Target file: {file.FullName} | Last modified: {file.LastWriteTime}");
            }
            // Synch source into target
            foreach (FileInfo sourceFile in sourceFiles)
            {
                string relativePath = Path.GetRelativePath(sourceDir.FullName, sourceFile.FullName);
                string targetFilePath = Path.Combine(targetDir.FullName, relativePath);
                FileInfo targetFile = new FileInfo(targetFilePath);
                string targetFileDir = Path.GetDirectoryName(targetFilePath);
                if (!Directory.Exists(targetFileDir))
                {
                    Directory.CreateDirectory(targetFileDir);
                    LogMessage(logPath, $"Created directory: {targetFileDir}");
                }
                if (targetFile.Exists)
                {
                    //Check if source file newer than target
                    if (sourceFile.LastWriteTime > targetFile.LastWriteTime)
                    {
                        sourceFile.CopyTo(targetFile.FullName, true);
                        LogMessage(logPath, $"Updated: {targetFile.FullName} (newer version copied)");
                    }
                    else
                    {
                        LogMessage(logPath, $"Skipped: {targetFile.FullName} (target is up-to-date)");
                    }
                }
                else
                {
                    sourceFile.CopyTo(targetFile.FullName);
                    LogMessage(logPath, $"Copied: {targetFile.FullName} (new file)");
                }
            }
            LogMessage(logPath, $"Synchronization complete. Repeating in {intervalSec} seconds");
            Thread.Sleep(intervalSec * 1000); //Cause sleep expects milliseconds
        }
    }
}
