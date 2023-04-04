using Xunit;

namespace LegacyApp.Tests
{
    public class UserValidatorTests
    {
        private readonly IUserValidator _userValidator;

        public UserValidatorTests()
        {
            _userValidator = new UserValidator();
        }

        [Fact]
        public void ValidateUser_NullUser_ReturnsFalse()
        {
            // Arrange
            User user = null;

            // Act
            bool result = _userValidator.ValidateUser(user);

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
            bool result = _userValidator.ValidateUser(user);

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
            bool result = _userValidator.ValidateUser(user);

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
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            // Act
            bool result = _userValidator.ValidateUser(user);

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
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            // Act
            bool result = _userValidator.ValidateUser(user);

            // Assert
            Assert.True(result);
        }
    }

}