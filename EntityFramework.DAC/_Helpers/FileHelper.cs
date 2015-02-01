using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DAC
{
    public static class FileHelper
    {
        public static string GetFilePathInCurrentDirectory(string fileName)
        {
            return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, fileName));
        }
    }
}
