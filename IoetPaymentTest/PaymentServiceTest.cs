using AutoFixture;
using IoetPaymentService.Manager;
using IoetPaymentServiceBase;
using IoetPaymentTest.TestData;
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

        [TestInitialize]
        public void SetUp()
        {
            _fileSystem.Setup(x => x.ReadTextFile(It.IsAny<string>())).Returns(MockData.EmployeesTestData());
        }

        [TestMethod]
        public void CalculateEmployeesSalary_GivenCorrectFile_ReturnCorrectValue()
        {
           
            _paymentService.Setup(f => f.CalculateEmployeesSalary(It.IsAny<string>()).Result)
                .Returns(MockData.EmployeesNameAndSalary());

            //var fileAccess = new Mock<FileSystem>();
            var paymentService = new PaymentService(_fileSystem.Object);

            Task<Dictionary<string, decimal>> response = paymentService.CalculateEmployeesSalary(It.IsAny<string>());

            Task<Dictionary<string, decimal>> result = _paymentService.Object.CalculateEmployeesSalary(It.IsAny<string>());

            CollectionAssert.AreEquivalent(response.Result, result.Result);

            Assert.AreEqual(result.Result.TryGetValue("AKAN", out decimal akan), response.Result.TryGetValue("AKAN", out decimal akanPay));

            Assert.AreEqual(result.Result.TryGetValue("ROSCO", out decimal rosco), response.Result.TryGetValue("ROSCO", out decimal roscoPay));

            Assert.AreEqual(result.Result.TryGetValue("BASH", out decimal bash), response.Result.TryGetValue("BASH", out decimal bashPay));

            Assert.AreEqual(result.Result.TryGetValue("EDDIE", out decimal eddie), response.Result.TryGetValue("EDDIE", out decimal eddiePay));

            Assert.AreEqual(result.Result.TryGetValue("CYRIL", out decimal cyril), response.Result.TryGetValue("CYRIL", out decimal cyrilPay));
        }



        [TestMethod]
        public void GivenThatCorrectFileNameIsSupplied_ReturnContent()
        {
            //_fileSystem.Setup(f => f.ReadTextFile(It.IsAny<string>()).Result).Returns(MockData.EmployeesTestData().Result);

            _fileSystem.Setup(x => x.ReadTextFile(It.IsAny<string>())).Returns(MockData.EmployeesTestData());

            var fileAccess = new FileSystem();
            Task<string[]> result = _fileSystem.Object.ReadTextFile(It.IsAny<string>());

            Assert.IsNotNull(result.Result);
        }

    }
}
