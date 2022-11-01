
using IoetPaymentServiceBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoetPaymentService.Manager
{
    public class PaymentService : IPaymentService
    {
        public async Task<Dictionary<string, decimal>> CalculateEmployeesSalary(string textFile)
        {
            Dictionary<string, decimal> paymentStructure = new();
            Dictionary<string, decimal> employeesPaymentRecord = new();

            string[] timeIntervals = { "00:01-09:00", "09:01-18:00", "18:01-23:59" };
            decimal[] payments = { 25, 15, 20 };

            decimal WeekEndExtraPay = 5;

            for (var i = 0; i < timeIntervals.Length; i++)
                paymentStructure[timeIntervals[i]] = payments[i];

            try
            {
                string[] dataInFile = await PaymentServiceHelper.ReadTextFile(textFile);

                foreach (var data in dataInFile)
                {
                    var separatedData = data.Split("=");
                    var employeeName = separatedData[0];
                    var employeeWorkDetails = separatedData[1].Split(",");
                    decimal employeeSalary = 0;
                    foreach (var workDetail in employeeWorkDetails)
                    {
                        var workSchedule = workDetail[2..];
                        (decimal amountPaidPerHour, int numberOfHoursWorked) = PaymentServiceHelper.CalculateNumberOfHoursWorkedAndAmount(workSchedule, timeIntervals, paymentStructure);

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

