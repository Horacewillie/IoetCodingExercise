using IoetPaymentService.Manager;
using System;
using System.Threading.Tasks;

namespace IoetPaymentConsole
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            try
            {
                var paymentService = new PaymentService();
                var employeesPayment = await paymentService.CalculateEmployeesSalary("./Resources/EmployeesData.txt");
                foreach (var employeeRecord in employeesPayment)
                {
                    Console.WriteLine($"The amount to pay {employeeRecord.Key} is: {employeeRecord.Value} USD");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
