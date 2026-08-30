using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IPtor.Helpers
{
    public static class FileHelper
    {
        // Открывает файл в программе по умолчанию (Excel, Word и т.д.)
        public static void OpenFile(string filePath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
    }
}
