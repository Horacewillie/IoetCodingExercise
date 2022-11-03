using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoetPaymentServiceBase
{
    public class FileSystem : IFileSystem
    {
        public async Task<string[]> ReadTextFile(string textFile)
        {
            string fileExtension = Path.GetExtension(textFile);
            if (fileExtension != ".txt")
                throw new Exception("Incorrect File Format Supplied");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", textFile);
            if (File.Exists(path))
            {
                string[] dataInFile = await File.ReadAllLinesAsync(path);
                return dataInFile;
            }
            throw new FileNotFoundException("Could not find file");
        }
    }
}
