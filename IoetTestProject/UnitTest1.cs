using IoetPaymentService.Manager;
using IoetPaymentServiceBase;
using Moq;
using System;
using System.IO;
using Xunit;

namespace IoetTestProject
{
    public class UnitTest1
    {
        private Mock<FileSystemManager> _fileSystemManager = new Mock<FileSystemManager>();
        private Mock<IPaymentService> paymentService = new Mock<IPaymentService>();



        private const string filePath = "EmployeesTestData.txt"; //"C:\\Users\\horace.saturday\\Desktop\\PersonalWorks\\IoetCodingExercise\\IoetPaymentTest\\TestData\\EmployeesTestData.txt";
        [Fact]
        public void ThrowException_IfFileIsNotFound()
        {

            string filePath = "file.txt";

            var mock = new Mock<IFileSystem>();
            mock.Setup(t => t.Exists(filePath)).Returns(true);

            FileSystemManager fileSystemManager = new FileSystemManager(mock.Object);
            var res = fileSystemManager.ReadTextFile(filePath);
            mock.Verify(f => f.ReadAllLinesAsync(It.IsAny<String>()), Times.Once);
            //Assert.ThrowsAsync<FileNotFoundException>(() => fileSystemManager.ReadTextFile(filePath));
        }


    }
}
