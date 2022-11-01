using IoetPaymentService.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IoetPaymentTest
{
    [TestClass]
    public class PaymentServiceTest
    {
        [DataRow("../IoetPaymentConsole/Resources/EmployeesData.txt")]
        [DataTestMethod]
        public void CalculateEmployeesSalary_GivenHoursWorkedAndTimeSchedule_ReturnCorrectValue(string textFile)
        {
            //Arrange
            var paymentService = new PaymentService();
            //Act
            Task<Dictionary<string, decimal>> response = paymentService.CalculateEmployeesSalary(textFile);
            //Assert
            response.Result.TryGetValue("IMAOBONG", out decimal imaobongPay);
            Assert.AreEqual(215, imaobongPay);

            response.Result.TryGetValue("HORACE", out decimal horacePay);
            Assert.AreEqual(70, horacePay);

            response.Result.TryGetValue("MADUKAKU", out decimal madukakuPay);
            Assert.AreEqual(85, madukakuPay);

            response.Result.TryGetValue("EDDIE", out decimal eddiePay);
            Assert.AreEqual(95, eddiePay);

            response.Result.TryGetValue("CYRIL", out decimal cyrilPay);
            Assert.AreEqual(255, cyrilPay);
        }

        [DataRow("../IoetPaymentConsole/Resources/EmployeesData.pdf")]
        [DataTestMethod]
        public void CalculateEmployeesSalary_GivenThatTheWrongFileFormat_IsSupplied_ThrowsAnException(string textFile)
        {
            var paymentService = new PaymentService();
            Assert.ThrowsExceptionAsync<Exception>(() => paymentService.CalculateEmployeesSalary(textFile));
        }

        [DataRow("../IoetPaymentConsole/Resources/EmployeeData.txt")]
        [DataTestMethod]
        public void CalculateEmployeesSalary_GivenThatWrongFileNameIsSupplied_ThrowsAnException(string textFile)
        {
            var paymentService = new PaymentService();
            Assert.ThrowsExceptionAsync<FileNotFoundException>(() => paymentService.CalculateEmployeesSalary(textFile));
        }

    }
}
