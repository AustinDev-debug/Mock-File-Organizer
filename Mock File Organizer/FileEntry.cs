using System;
using System.IO;

namespace FileOrganizer
{
    class FileEntry
    {
        public string FileName { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }


        public FileEntry(string fileName, string category,  DateTime date)
        {
            FileName = fileName;
            Category = category;
            Date = date;
        }
    }
}