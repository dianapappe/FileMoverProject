using System;

// Diana Pappe
// Class will read the lines of a text file.
namespace FileMoverProject
{
    public class ReadFiles
    {
        string PathFile;    // Field will contain the full path of a text file.
        int LinesToRead;    // Indicate the number of lines that will read.


        public ReadFiles(string aPathFile, int aLinesToRead)
        {
            PathFile = aPathFile;
            LinesToRead = aLinesToRead;
        }

        // method will return the lines requested
        public string[] LinesToReturn(string aPathFile, int aLinesToRead)
        {
            string getLine = "";

            if (aPathFile.Trim().Length > 0)
            {
                int countLines = 0;

                // Read the file and quit after get the first line.  
                foreach (string line in System.IO.File.ReadLines(aPathFile))
                {
                    getLine = line;
                    countLines++;

                    if (countLines == aLinesToRead)
                        break;
                }

                getLine = getLine.Replace('\n', ',');
            }

            string[] firstLine = { getLine };
            return firstLine;
        }
    }
}
