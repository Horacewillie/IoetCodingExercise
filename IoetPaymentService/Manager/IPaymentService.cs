
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoetPaymentService.Manager
{
    public interface IPaymentService
    {
        Task<Dictionary<string, decimal>> CalculateEmployeesSalary(string textFile);
    }
}

