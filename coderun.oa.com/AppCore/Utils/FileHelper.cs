using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppCore.Utils
{
    public class FileHelper
    {
        public static string GetDir(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.DirectoryName;
        }

        public static bool IsExists(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Exists;
        }

        public static string GetName(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Name;
        }
    }
}
