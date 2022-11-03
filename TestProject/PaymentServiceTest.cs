using Moq;
using System;
using Xunit;

namespace IoetTestProject
{
    public class PaymentServiceTest
    {
        
        [Fact]
        public void FileManager_throws_exception_when_file_doesnt_exist()
        {
            string filePath = "file.txt";
            var mock = new Mock();
            mock.Setup(t => t.Exists(filePath)).Returns(false);

            FileManagerWithWrapper fileManager = new FileManagerWithWrapper(mock.Object);

            Assert.Throws(() => fileManager.FileOperation(filePath));
        }
    }
}
