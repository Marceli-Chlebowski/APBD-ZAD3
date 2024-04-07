using System;
using Moq;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_WithValidData_ReturnsTrue()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var clientRepositoryMock = new Mock<IClientRepository>();
            var userCreditServiceMock = new Mock<IUserCreditService>();

            var userService = new UserService(userRepositoryMock.Object, clientRepositoryMock.Object, userCreditServiceMock.Object);

            clientRepositoryMock.Setup(c => c.GetById(It.IsAny<int>())).Returns(new Client { Type = "NormalClient" });
            userCreditServiceMock.Setup(u => u.GetCreditLimit(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1000);

            // Act
            var result = userService.AddUser("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddUser_WithInvalidData_ReturnsFalse()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var clientRepositoryMock = new Mock<IClientRepository>();
            var userCreditServiceMock = new Mock<IUserCreditService>();

            var userService = new UserService(userRepositoryMock.Object, clientRepositoryMock.Object, userCreditServiceMock.Object);

            // Act
            var result = userService.AddUser("", "", "invalidemail", new DateTime(2005, 1, 1), 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddUser_WithNonexistentClient_ReturnsFalse()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var clientRepositoryMock = new Mock<IClientRepository>();
            var userCreditServiceMock = new Mock<IUserCreditService>();

            var userService = new UserService(userRepositoryMock.Object, clientRepositoryMock.Object, userCreditServiceMock.Object);
            clientRepositoryMock.Setup(c => c.GetById(It.IsAny<int>())).Returns((Client)null);

            // Act
            var result = userService.AddUser("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddUser_WithInsufficientCreditLimit_ReturnsFalse()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var clientRepositoryMock = new Mock<IClientRepository>();
            var userCreditServiceMock = new Mock<IUserCreditService>();

            var userService = new UserService(userRepositoryMock.Object, clientRepositoryMock.Object, userCreditServiceMock.Object);
            clientRepositoryMock.Setup(c => c.GetById(It.IsAny<int>())).Returns(new Client { Type = "ImportantClient" });
            userCreditServiceMock.Setup(u => u.GetCreditLimit(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(200);

            // Act
            var result = userService.AddUser("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 1);

            // Assert
            Assert.False(result);
        }

        // Additional tests can be added to cover more scenarios
    }
}
