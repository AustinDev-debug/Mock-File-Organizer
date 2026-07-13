using System;
using System.IO;

namespace FileOrganizer
{
    class Program
    {
        List<FileEntry> Files = new List<FileEntry>();
        static void Main(string[] args)
        {
            Program program = new Program();
            bool runProgram = true;
            while (runProgram)
            {
                Console.WriteLine("1. Add File\n" +
                    "2. List Files\n" +
                    "3. Search Files\n" +
                    "4. Load File\n" +
                    "5. Exit");
                Console.Write("Input: ");
                int input = program.NumberValidation(Console.ReadLine());
                switch(input)
                {
                    case 1:
                        program.AddFile();
                        break;
                    case 2:
                        program.ListFiles();
                        break;
                    case 3:
                        program.SearchFiles();
                        break;
                    case 4:
                        program.LoadFile();
                        break;
                    case 5:
                        runProgram = false;
                        break;
                }
            }
        }

        string InputValidation(string? input)
        {
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input can not be empty");
                input = Console.ReadLine();
            }
            return input;
        }

        int NumberValidation(string? input)
        {
            while (true)
            {
                int number = 0;
                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input can not be empty");
                    input = InputValidation(Console.ReadLine());
                }
                if (int.TryParse(input, out number))
                {
                    return number;
                }
                Console.WriteLine("Enter a valid number");
            }
        }

        void AddFile()
        {
            Console.WriteLine("How many files do you want to add?");
            int number = NumberValidation(Console.ReadLine());
            string directory = DirectorySelection();
            string combinedPath = Path.Combine(directory, "FileOrganizer.txt");
            try
            {
                if (!File.Exists(combinedPath))
                {
                    File.Create(combinedPath).Close();
                }
                for (int i = 0; i < number; i++)
                {
                    Console.WriteLine("Enter file name:");
                    string fileName = InputValidation(Console.ReadLine());
                    Console.WriteLine("Enter file category");
                    string category = InputValidation(Console.ReadLine());
                    DateTime date = DateTime.Now;
                    // write to file
                    using (StreamWriter writer = new StreamWriter(combinedPath, true))
                    {
                        writer.WriteLine($"Date: {DateTime.Now}");
                        writer.WriteLine($"File Name: {fileName}");
                        writer.WriteLine($"File Category: {category}");
                    }
                    FileEntry fileEntry = new FileEntry(fileName, category, date);
                    Files.Add(fileEntry);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        string DirectorySelection()
        {
            while (true)
            {
                Console.WriteLine("Select a directory from the following options\n" +
               "1. Desktop Folder\n" +
               "2. Documents Folder\n");
                int input = NumberValidation(Console.ReadLine());
                if (input == 1)
                {
                    string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    if (Directory.Exists(directoryPath))
                    {
                        return directoryPath.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Directory does not exist");
                    }
                }
                if (input == 2)
                {
                    string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (Directory.Exists(directoryPath))
                    {
                        return directoryPath.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Directory does not exist");
                    }
                }
            }

        }

        void ListFiles()
        {
            
            try
            {
                string path = DirectorySelection();
                string txtFile = "FileOrganizer.txt";
                string combinedPath = Path.Combine(path, txtFile);
                string[] lines = File.ReadAllLines(combinedPath);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void SearchFiles()
        {
            string path = DirectorySelection();
            string txtFile = "FileOrganizer.txt";
            string combinedPath = Path.Combine (path, txtFile);
            List<string> file = new List<string>();
            Console.WriteLine("Enter the name of the file you are looking for");
            string name = InputValidation(Console.ReadLine());
            string line;
            using (StreamReader sr = new StreamReader(combinedPath))
            {
               while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(name))
                    {
                        file.Add(line);
                        break;
                    }
                    
                }
            }
            foreach (string f in file)
            {
                Console.WriteLine(f);
            }
        }

        void LoadFile()
        {
            string path = DirectorySelection();
            string txtFile = "FileOrganizer.txt";
            string combinedPath = Path.Combine(path, txtFile);
            using (StreamReader sr = new StreamReader(combinedPath))
            {
                string line = sr.ReadToEnd();
                Console.WriteLine(line);
            }
        }
    }
}
