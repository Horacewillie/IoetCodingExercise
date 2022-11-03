using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IoetPaymentTest.TestData
{
    public static class MockData
    {
        public async static Task<string[]> EmployeesTestData()
        {
            await Task.Delay(0);
            var file = @"AKAN=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00
ROSCO=MO10:00-12:00,TH13:00-14:00,SU20:00-21:00
BASH=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00
EDDIE=MO10:00-12:00,SA12:00-14:00,SU20:00-21:00
CYRIL=MO10:00-12:00,TU12:00-15:00,TH01:00-04:00,SA14:00-18:00,SU20:00-21:00";

            return file.Split(Environment.NewLine);
        }

        public static Dictionary<string, decimal> EmployeesNameAndSalary()
        {
            return new Dictionary<string, decimal>
            {
                {"AKAN", 215 },
                {"ROSCO", 70 },
                {"BASH", 85 },
                {"EDDIE", 95 },
                {"CYRIL", 255 }
            };
        }

        public static FileNotFoundException Yeye()
        {
            throw new FileNotFoundException();
        }

    }
}
