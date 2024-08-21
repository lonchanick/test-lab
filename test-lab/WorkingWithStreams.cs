using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_lab
{
    class WorkingWithStreams
    {
        public static void Exe()
        {
            string fileNameAndDirectory = @"C:\.NET FOLDER\Projects\test-lab\file.txt";
            //var temp = Directory.GetCurrentDirectory();
            //Console.WriteLine($"{temp}");
            using FileStream fileStream = new(fileNameAndDirectory,FileMode.Create,FileAccess.Write,FileShare.None);
            string text = "Himno\nNacional\nDel Ecuador";
            Byte[] writeArr = Encoding.UTF8.GetBytes(text);
            fileStream.Write(writeArr, 0, text.Length);
        }
    }
}
