using IoetPaymentServiceBase;
using Moq;
using NUnit.Framework;

namespace TestProjectUsingNUnit
{
    [TestFixture]
    public class Tests
    {
        private const string DummyDataFilePath = "/TextFile1.txt";
        private Mock<IFileSystem> m_SystemFileOperationsManagerMock;

        private FileSystemManager m_Sut;
        [SetUp]
        public void Setup()
        {
            m_SystemFileOperationsManagerMock = new Mock<IFileSystem>();
            m_Sut = new FileSystemManager(m_SystemFileOperationsManagerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            m_Sut = null;
            m_SystemFileOperationsManagerMock = null;
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var text = new string[] { "This is the sample text" };
            m_SystemFileOperationsManagerMock
                .Setup
                (
                    m => m.ReadAllLines(It.Is<string>(p => p == DummyDataFilePath))
                )
                .Returns(text)
                .Verifiable();

            // Act
            var actual = m_Sut.ReadTextFile(DummyDataFilePath);

            //// Assert
            m_SystemFileOperationsManagerMock
                .Verify
                (
                    m => m.ReadAllLines(DummyDataFilePath)
                );

            Assert.AreEqual(text, actual);
        }
    }
}