using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppCore.Utils
{
    public class StaticVariable
    {
        public static string CODE_SAVE_PATH = "";

        public static string NowPath
        {
            get
            {
                string path = Path.Combine(CODE_SAVE_PATH, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        public static int RUN_MILL = 5000;
        
    }
}
