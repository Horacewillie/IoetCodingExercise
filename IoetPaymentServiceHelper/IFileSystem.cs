using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoetPaymentServiceBase
{
    public interface IFileSystem
    {
        Task<string[]> ReadTextFile(string textFile);
    }
}
