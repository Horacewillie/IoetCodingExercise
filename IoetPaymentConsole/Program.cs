using IoetPaymentService.Manager;
using IoetPaymentServiceBase;
using Microsoft.Extensions.DependencyInjection;
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
                //setup our Dependency Injection 
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<IPaymentService, PaymentService>()
                    .AddSingleton<IFileSystem, FileSystem>()
                    .BuildServiceProvider();

                //do the actual work here
                var paymentService = serviceProvider.GetService<IPaymentService>();

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
