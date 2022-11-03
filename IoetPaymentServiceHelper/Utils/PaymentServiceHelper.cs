
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IoetPaymentServiceBase.Utils
{
    public class PaymentServiceHelper
    {
        private static (string startTime, string endTime, int numberofHoursWorked) CalculateIntervalsAndNumberOfHoursWorked(string workSchedule)
        {
            var timeIntervals = workSchedule.Split("-");
            
            TimeSpan timeDifference = DateTime.Parse(timeIntervals[1]) - DateTime.Parse(timeIntervals[0]);
            
            int totalNumberOfHoursWorkedByEmployee = timeDifference.TotalHours < 0 ? (int)timeDifference.TotalHours + 24 : (int)timeDifference.TotalHours;

            return (timeIntervals[0], timeIntervals[1], totalNumberOfHoursWorkedByEmployee);
        }

        public static (decimal amountEarned, int numberOfHoursWorked) CalculateNumberOfHoursWorkedAndAmount(string workSchedule, string[] timeIntervals, Dictionary<string, decimal> paymentStructure)
        {
            (string startTime, string endTime, int numberOfHoursWorked) = CalculateIntervalsAndNumberOfHoursWorked(workSchedule);

            decimal amountPaidForHoursWorked = 0;

            var timeIntervalsAndNumberOfHoursWorked = new List<(string, string, int)> { };

            foreach (var timeInterval in timeIntervals)
            {
                timeIntervalsAndNumberOfHoursWorked.Add(CalculateIntervalsAndNumberOfHoursWorked(timeInterval));
            }

            for (int i = 0; i < timeIntervalsAndNumberOfHoursWorked.Count; i++)
            {
                if (DateTime.Parse(startTime) >= DateTime.Parse(timeIntervalsAndNumberOfHoursWorked[i].Item1) 
                    && DateTime.Parse(endTime) <= DateTime.Parse(timeIntervalsAndNumberOfHoursWorked[i].Item2))
                {
                    amountPaidForHoursWorked = paymentStructure[timeIntervals[i]];
                    break;
                }
            }
            return (amountPaidForHoursWorked, numberOfHoursWorked);
        }
    }
}

