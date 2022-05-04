using System;

// Diana Pappe
// Class will create a file, if it does not exist. otherwise, it will add a new lines to the existing text file.

namespace FileMoverProject
{
    public class WriteFile
    {
        string PathFile;            // Field will contain the full path of a text file
        string[] LinesToWrite;     // lines that will be added to the text file

        public WriteFile(string aPathFile, string[] aLinesToWrite)
        {
            PathFile = aPathFile;
            LinesToWrite = aLinesToWrite;
        }

        public bool FileSaved(string aPathFile, string[] aLinesToWrite)
        {
            bool Success = false;

            try
            {
                // Creating the file if it does not exist.
                if (!System.IO.File.Exists(aPathFile))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(aPathFile))
                    {
                        for (byte i = 0; i < 100; i++)
                        {
                            fs.WriteByte(i);
                        }
                    }

                    // Writting the line(s) for new File
                    System.IO.File.WriteAllLines(aPathFile, aLinesToWrite);
                    Success = true;
                }
                else
                {
                    // Add new line(s) to existing file
                    System.IO.File.AppendAllLines(aPathFile, aLinesToWrite);
                    Success = true;
                }
            }
            catch { Success = false; }

            return Success;
        }


    }
}
