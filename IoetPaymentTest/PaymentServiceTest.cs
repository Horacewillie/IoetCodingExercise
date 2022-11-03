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
        public void CalculateEmployeesSalary_IfCalculationIsRight_ReturnCorrectValue()
        {
           
            _paymentService.Setup(f => f.CalculateEmployeesSalary(It.IsAny<string>()).Result)
                .Returns(MockData.EmployeesNameAndSalary());

            //var fileAccess = new Mock<FileSystem>();
            var paymentService = new PaymentService(_fileSystem.Object);

            Task<Dictionary<string, decimal>> response = paymentService.CalculateEmployeesSalary(It.IsAny<string>());

            Task<Dictionary<string, decimal>> result = _paymentService.Object.CalculateEmployeesSalary(It.IsAny<string>());

            CollectionAssert.AreEquivalent(response.Result, result.Result);
            CollectionAssert.AreEqual(response.Result, result.Result);

            Assert.AreEqual(result.Result.TryGetValue("AKAN", out decimal akan), response.Result.TryGetValue("AKAN", out decimal akanPay));

            Assert.AreEqual(result.Result.TryGetValue("ROSCO", out decimal rosco), response.Result.TryGetValue("ROSCO", out decimal roscoPay));

            Assert.AreEqual(result.Result.TryGetValue("BASH", out decimal bash), response.Result.TryGetValue("BASH", out decimal bashPay));

            Assert.AreEqual(result.Result.TryGetValue("EDDIE", out decimal eddie), response.Result.TryGetValue("EDDIE", out decimal eddiePay));

            Assert.AreEqual(result.Result.TryGetValue("CYRIL", out decimal cyril), response.Result.TryGetValue("CYRIL", out decimal cyrilPay));
        }

        [TestMethod]
        public void CalculateEmployeesSalary_IfCalculationIsWrong_ReturnWrongValues()
        {

            _paymentService.Setup(f => f.CalculateEmployeesSalary(It.IsAny<string>()).Result)
                .Returns(MockData.EmployeesNameAndSalaryWrongValues());


            //var fileAccess = new Mock<FileSystem>();
            var paymentService = new PaymentService(_fileSystem.Object);

            Task<Dictionary<string, decimal>> response = paymentService.CalculateEmployeesSalary(It.IsAny<string>());

            Task<Dictionary<string, decimal>> result = _paymentService.Object.CalculateEmployeesSalary(It.IsAny<string>());

            CollectionAssert.AreNotEqual(response.Result, result.Result);
        }



        [TestMethod]
        public void ReadFileContent_ReturnContent()
        {

            _fileSystem.Setup(x => x.ReadTextFile(It.IsAny<string>())).Returns(MockData.EmployeesTestData());

            Task<string[]> result = _fileSystem.Object.ReadTextFile(It.IsAny<string>());

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void ReadFileContent_IfFileDoesNotExist_ThrowException()
        {

            // Arrange
            string filePathWithExtension = "file1.txt";

            _fileSystem.Setup(x => x.ReadTextFile(filePathWithExtension)).Throws(MockData.FileNotFoundException());

            var paymentService = new PaymentService(_fileSystem.Object);
            var result = paymentService.CalculateEmployeesSalary(filePathWithExtension);

            Assert.AreEqual("Could not find file", result.Exception.InnerException.Message);
        }

        [TestMethod]
        public void ReadFileContent_IfFileExtensionDoesntMatch_ThrowException()
        {

            // Arrange
            string filePathWithExtension = "file1.pdf";

            _fileSystem.Setup(x => x.ReadTextFile(filePathWithExtension)).Throws(MockData.InvalidFileFormatException());

            var paymentService = new PaymentService(_fileSystem.Object);
            var result = paymentService.CalculateEmployeesSalary(filePathWithExtension);

            Assert.AreEqual("Incorrect File Format Supplied", result.Exception.InnerException.Message);
        }
    }
}
