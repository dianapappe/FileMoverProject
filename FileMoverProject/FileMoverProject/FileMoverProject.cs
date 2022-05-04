using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


// Diana Pappe
// Programming task for NFR Programmer Position.
// Monitor a source directory
// Move any file from the source directory
// If moved file has a txt extension, then get first line and save it into a log file.

namespace FileMoverProject
{
    // Class will store the folder and log paths
    public class FilePaths
    {
        public string SourceDirectory;          // Full Source Directory's name - ex: c:\FolderFiles\source
        public string DestinationDirectory;     // Full Destination Directory's name - ex: c:\FolderFiles\Destination
        public string LogFile;                  // Full log's file path - ex: c:\Log\logFile.txt

        public FilePaths(string aSourceDirectory, string aDestinationDirectory, string aLogFile)
        {
            SourceDirectory = aSourceDirectory;
            DestinationDirectory = aDestinationDirectory;
            LogFile = aLogFile;
        }
    }
    class FileMoverProject
    {
        static FilePaths MovedFile = new FilePaths("", "", "");    // contains the folder that will be monitored and the path of the log file

        static void Main(string[] args)
        {
            // Set Paths 
            MovedFile.SourceDirectory = "c:\\users\\documents\\NFR";
            MovedFile.DestinationDirectory = "c:\\users\\documents\\NFR\\Destination";
            MovedFile.LogFile = "c:\\users\\documents\\NFR\\Log\\LogTest.txt";

             // validating paths
            DirectoryInfo SourcePath = new DirectoryInfo(Path.GetFullPath(MovedFile.SourceDirectory));
            DirectoryInfo DestinationPath = new DirectoryInfo(Path.GetFullPath(MovedFile.DestinationDirectory));
            FileInfo LogPath = new FileInfo(Path.GetFullPath(MovedFile.LogFile));

            // monitoring events
            if (SourcePath.Exists && DestinationPath.Exists && LogPath.Exists)
            {
                GetFiles();
            }
            else
            {
                Console.WriteLine("Path does not exist.\n");
            }

            Console.ReadLine();
        }

        // Method get all files from the source directory
        // send the full name of each file to the MoveFile method.
        static void GetFiles()
        {
            string[] DirectoryFiles = Directory.GetFiles(MovedFile.SourceDirectory);
            foreach (string FileName in DirectoryFiles)
            {
                MoveFile(FileName);
            }

            Console.WriteLine("Process complete.");
        }

        // Method Validates paths.
        // move files from source directory to destination directory.
        // if file name already exists in the destination folder, then it asks if file has to be overwritten.
        static void MoveFile(string aFileName)
        {
            string FullDestinationFileName = "";
            string OverWriteFile = "Y";

            FileInfo OrigenFullPath = new FileInfo(Path.GetFullPath(aFileName));

            // Adding file's name to destination directory and moving the file.
            FullDestinationFileName = MovedFile.DestinationDirectory + "\\" + OrigenFullPath.Name;

            FileInfo DestinationFullPath = new FileInfo(Path.GetFullPath(FullDestinationFileName));

            // File is moved only if it does exist.
            if (OrigenFullPath.Exists)
            {
                // Verify if file already exists in destination folder
                if (DestinationFullPath.Exists)
                {
                    Console.WriteLine(OrigenFullPath.Name + " already exist in destination folder. Do you want overwrite it? Y/N:");
                    OverWriteFile = Console.ReadLine().ToUpper();

                    if (OverWriteFile == "Y")
                        File.Delete(FullDestinationFileName); //File must be removed from destination folder before it can be moved
                    else
                        Console.WriteLine(OrigenFullPath.Name + " was not overwritten. File was not moved.");   // file will not be overwritten
                }

                // File id moved\added in destination folder
                if (OverWriteFile == "Y")
                {
                    // Moving file
                    File.Move(aFileName, FullDestinationFileName);
                    Console.WriteLine(OrigenFullPath.Name + " moved to Destination folder.");

                    if (DestinationFullPath.Extension.Equals(".txt"))
                    {
                        CreateLogFile(FullDestinationFileName);   // Add first line into log file only if file has a txt extension
                    }
                }
            }
        }

        // Read the first line and save it into the log file.
        // log file is created if it does not exist. Otherwise, line will be appended into the existing log file.
        static void CreateLogFile(string FileFullPath)
        {
            if (MovedFile.LogFile.Trim().Length > 0 && FileFullPath.Trim().Length > 0)
            {
                // Getting the first line
                ReadFiles rFile = new ReadFiles(FileFullPath, 1);
                string[] agetLine = rFile.LinesToReturn(FileFullPath, 1);

                //Adding line to log file
                WriteFile wLogFile = new WriteFile(MovedFile.LogFile, agetLine);
                bool isSuccess = wLogFile.FileSaved(MovedFile.LogFile, agetLine);

                if (isSuccess)
                {
                    Console.WriteLine("Line added to Log Successfully");
                }
                else
                {
                    Console.WriteLine("An error occurred when adding to log file");
                }
            }
            else
            { Console.WriteLine("An error occurred when adding to log file"); }
        }
    }
}
