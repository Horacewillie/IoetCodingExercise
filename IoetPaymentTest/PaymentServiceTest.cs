using AutoFixture;
using IoetPaymentService.Manager;
using IoetPaymentServiceBase;
//using IoetPaymentTest.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IoetPaymentTest
{
    [TestClass]
    public class PaymentServiceTest
    {
        private readonly Mock<IFileSystem> _fileSystem;

        private readonly Mock<IPaymentService> _paymentService;
        private readonly string filePath =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "./TestData/EmployeesTestData.txt");

        public PaymentServiceTest()
        {
            _fileSystem = new Mock<IFileSystem>();
            _paymentService = new Mock<IPaymentService>();
        }

        [TestMethod]
        public void CalculateEmployeesSalary_GivenCorrectFile_ReturnCorrectValue()
        {
           
            _paymentService.Setup(f => f.CalculateEmployeesSalary(filePath).Result)
                .Returns(new Dictionary<string, decimal> 
            {
                {"AKAN", 215 },
                {"ROSCO", 70 },
                {"BASH", 85 },
                {"EDDIE", 95 },
                {"CYRIL", 255 }
            });

            var fileAccess = new Mock<FileSystem>();
            var paymentService = new PaymentService(fileAccess.Object);

            Task<Dictionary<string, decimal>> response = paymentService.CalculateEmployeesSalary(filePath);

            Task<Dictionary<string, decimal>> result = _paymentService.Object.CalculateEmployeesSalary(filePath);

            CollectionAssert.AreEquivalent(response.Result, result.Result);

            Assert.AreEqual(result.Result.TryGetValue("AKAN", out decimal akan), response.Result.TryGetValue("AKAN", out decimal akanPay));

            Assert.AreEqual(result.Result.TryGetValue("ROSCO", out decimal rosco), response.Result.TryGetValue("ROSCO", out decimal roscoPay));

            Assert.AreEqual(result.Result.TryGetValue("BASH", out decimal bash), response.Result.TryGetValue("BASH", out decimal bashPay));

            Assert.AreEqual(result.Result.TryGetValue("EDDIE", out decimal eddie), response.Result.TryGetValue("EDDIE", out decimal eddiePay));

            Assert.AreEqual(result.Result.TryGetValue("CYRIL", out decimal cyril), response.Result.TryGetValue("CYRIL", out decimal cyrilPay));
        }



        [TestMethod]
        public void GivenThatCorrectFileNameIsSupplied_ReturnFileContent()
        {
            _fileSystem.Setup(f => f.ReadTextFile(filePath).Result).Returns(new string[]
            {
                "AKAN=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00",
                "ROSCO=MO10:00-12:00,TH13:00-14:00,SU20:00-21:00",
                "BASH=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00",
                "EDDIE=MO10:00-12:00,SA12:00-14:00,SU20:00-21:00",
                "CYRIL=MO10:00-12:00,TU12:00-15:00,TH01:00-04:00,SA14:00-18:00,SU20:00-21:00"
            });

            var fileAccess = new FileSystem();

            string[] response = fileAccess.ReadTextFile(filePath).Result;

            string[] result = _fileSystem.Object.ReadTextFile(filePath).Result;

            CollectionAssert.AreEqual(response, result);
        }

    }
}
