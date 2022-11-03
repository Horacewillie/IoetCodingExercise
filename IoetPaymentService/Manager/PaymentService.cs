
using IoetPaymentServiceBase;
using IoetPaymentServiceBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoetPaymentService.Manager
{
    public class PaymentService : IPaymentService
    {
        private readonly IFileSystem _fileSystem;
        public PaymentService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public async Task<Dictionary<string, decimal>> CalculateEmployeesSalary(string textFile)
        {
            Dictionary<string, decimal> employeesPaymentRecord = new();

            string[] timeIntervals = { "00:01-09:00", "09:01-18:00", "18:01-23:59" };
            decimal[] payments = { 25, 15, 20 };

            decimal WeekEndExtraPay = 5;

            Dictionary<string, decimal> paymentStructure = new()
            {
                { "00:01-09:00",  25 },
                { "09:01-18:00", 15 },
                { "18:01-23:59", 20 }
            };

            try
            {
                string[] dataInFile = await _fileSystem.ReadTextFile(textFile);

                foreach (var data in dataInFile)
                {
                    var separatedData = data.Split("=");
                    var employeeName = separatedData[0];
                    var employeeWorkDetails = separatedData[1].Split(",");
                    decimal employeeSalary = 0;
                    foreach (var workDetail in employeeWorkDetails)
                    {
                        var timeSchedule = workDetail[2..];
                        (decimal amountPaidPerHour, int numberOfHoursWorked) = PaymentServiceHelper.CalculateNumberOfHoursWorkedAndAmount(timeSchedule, timeIntervals, paymentStructure);

                        string[] daysOfWeek = { "MO", "TU", "WE", "TH", "FR", "SA", "SU" };

                        //check day of week, so as to add bonus accordingly.
                        if (daysOfWeek[..5].Contains(workDetail[..2]))
                            employeeSalary += amountPaidPerHour * numberOfHoursWorked;

                        else if (daysOfWeek[5..].Contains(workDetail[..2]))
                            employeeSalary += (amountPaidPerHour + WeekEndExtraPay) * numberOfHoursWorked;

                        else throw new Exception("Invalid Day of the Week Detected.");
                    }
                    employeesPaymentRecord.Add(employeeName, employeeSalary);
                }
                return employeesPaymentRecord;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

