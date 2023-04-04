using Moq;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IClientRepository> mockClientRepository;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<IUserValidator> mockUserValidator;
        private readonly Mock<ICreditLimitProvider> mockCreditLimitProvider;
        private readonly UserService userService;

        public UserServiceTests()
        {
            mockClientRepository = new Mock<IClientRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            mockUserValidator = new Mock<IUserValidator>();
            mockCreditLimitProvider = new Mock<ICreditLimitProvider>();
            userService = new UserService(
                mockClientRepository.Object,
                mockUserRepository.Object,
                mockUserValidator.Object,
                mockCreditLimitProvider.Object
            );
        }

        [Fact]
        public void AddUser_ShouldReturnFalse_WhenUserValidationFails()
        {
            // Arrange
            var user = new User();
            mockUserValidator.Setup(x => x.ValidateUser(user)).Returns(false);

            // Act
            var result = userService.AddUser("test", "test", "test@example.com", DateTime.UtcNow, 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ShouldReturnFalse_WhenClientIsNull()
        {
            // Arrange
            var user = new User();
            mockUserValidator.Setup(x => x.ValidateUser(user)).Returns(true);
            mockClientRepository.Setup(x => x.Get(1)).Returns((Client)null);

            // Act
            var result = userService.AddUser("test", "test", "test@example.com", DateTime.UtcNow, 1);

            // Assert
            Assert.False(result);
            mockClientRepository.Verify(x => x.Get(1), Times.Once);
        }

        [Fact]
        public void AddUser_ShouldReturnFalse_WhenCreditLimitValidationFails()
        {
            // Arrange
            var user = new User();
            mockUserValidator.Setup(x => x.ValidateUser(user)).Returns(true);
            mockClientRepository.Setup(x => x.Get(1)).Returns(new Client());
            mockCreditLimitProvider.Setup(x => x.ValidateCreditLimit(user)).Returns(false);

            // Act
            var result = userService.AddUser("test", "test", "test@example.com", DateTime.UtcNow, 1);

            // Assert
            Assert.False(result);
            mockCreditLimitProvider.Verify(x => x.ValidateCreditLimit(user), Times.Once);
        }

        [Fact]
        public void AddUser_ShouldAddUser_WhenAllValidationPasses()
        {
            // Arrange
            var user = new User();
            mockUserValidator.Setup(x => x.ValidateUser(user)).Returns(true);
            mockClientRepository.Setup(x => x.Get(1)).Returns(new Client());
            mockCreditLimitProvider.Setup(x => x.ValidateCreditLimit(user)).Returns(true);

            // Act
            var result = userService.AddUser("test", "test", "test@example.com", DateTime.UtcNow, 1);

            // Assert
            Assert.True(result);
            mockUserRepository.Verify(x => x.AddUser(user), Times.Once);
        }
    }

}