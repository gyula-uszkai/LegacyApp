using Moq;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserValidatorTests
    {
        private readonly IUserValidator sut;

        public UserValidatorTests()
        {
            // Create a mock of the IDateTimeService interface using Moq
            Mock<IDateTimeService> dateTimeServiceMock = new Mock<IDateTimeService>();

            // Set the mock to return a fixed DateTime value for testing purposes
            dateTimeServiceMock.Setup(m => m.Now).Returns(new DateTime(2023, 4, 4));

            // Instantiate the UserValidator class with the mock object
            sut = new UserValidator(dateTimeServiceMock.Object);
        }

        [Fact]
        public void ValidateUser_NullUser_ReturnsFalse()
        {
            // Arrange
            User user = null;

            // Act
            bool result = sut.ValidateUser(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateUser_MissingFields_ReturnsFalse()
        {
            // Arrange
            User user = new User
            {
                Firstname = "",
                Surname = "Doe",
                EmailAddress = "johndoe@example.com",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            // Act
            bool result = sut.ValidateUser(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateUser_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            User user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "invalid-email",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            // Act
            bool result = sut.ValidateUser(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateUser_UnderAgeLimit_ReturnsFalse()
        {
            // Arrange
            User user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "johndoe@example.com",
                DateOfBirth = new DateTime(2003, 4, 4)
            };

            // Act
            bool result = sut.ValidateUser(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateUser_ValidUser_ReturnsTrue()
        {
            // Arrange
            User user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "johndoe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act
            bool result = sut.ValidateUser(user);

            // Assert
            Assert.True(result);
        }
    }

}