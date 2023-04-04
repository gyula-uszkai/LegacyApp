using Moq;
using Xunit;

namespace LegacyApp.Tests
{
    public class CreditLimitProviderTests
    {
        private Mock<IClientCreditServiceFactory> _clientCreditServiceFactoryMock;
        private CreditLimitProvider sut;

        public CreditLimitProviderTests()
        {
            _clientCreditServiceFactoryMock = new Mock<IClientCreditServiceFactory>();
            sut = new CreditLimitProvider(_clientCreditServiceFactoryMock.Object);
        }

        [Fact]
        public void ApplyCreditLimit_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            User user = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => sut.ApplyCreditLimit(user));
        }

        [Fact]
        public void ApplyCreditLimit_UserWithNullClient_ThrowsArgumentException()
        {
            // Arrange
            var user = new User { Client = null };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.ApplyCreditLimit(user));
        }

        [Fact]
        public void ApplyCreditLimit_UnsupportedClient_ThrowsException()
        {
            // Arrange
            var user = new User { Client = new Client { Name = "UnknownClient" } };
            var clientCreditServiceMock = new Mock<IClientCreditService>();
            _clientCreditServiceFactoryMock.Setup(x => x.GetClientCreditServices())
                                           .Returns(new Dictionary<string, IClientCreditService>
                                           {
                                           { "SupportedClient", clientCreditServiceMock.Object }
                                           });

            // Act & Assert
            Assert.Throws<Exception>(() => sut.ApplyCreditLimit(user));
        }

        [Fact]
        public void ApplyCreditLimit_UserWithSupportedClient_SetsHasCreditLimitAndCreditLimit()
        {
            // Arrange
            var user = new User { Client = new Client { Name = "SupportedClient" } };
            var clientCreditServiceMock = new Mock<IClientCreditService>();
            clientCreditServiceMock.Setup(x => x.HasCreditLimit)
                                  .Returns(true);
            clientCreditServiceMock.Setup(x => x.GetCreditLimit(user))
                                  .Returns(1000);
            _clientCreditServiceFactoryMock.Setup(x => x.GetClientCreditServices())
                                           .Returns(new Dictionary<string, IClientCreditService>
                                           {
                                           { "SupportedClient", clientCreditServiceMock.Object }
                                           });

            // Act
            sut.ApplyCreditLimit(user);

            // Assert
            Assert.True(user.HasCreditLimit);
            Assert.Equal(1000, user.CreditLimit);
        }

        [Fact]
        public void ValidateCreditLimit_UserHasCreditLimitAndCreditLimitLessThanMinimal_ReturnsFalse()
        {
            // Arrange
            var user = new User { HasCreditLimit = true, CreditLimit = 100 };

            // Act
            var result = sut.ValidateCreditLimit(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateCreditLimit_UserHasCreditLimitAndCreditLimitGreaterThanMinimal_ReturnsTrue()
        {
            // Arrange
            var user = new User { HasCreditLimit = true, CreditLimit = 600 };

            // Act
            var result = sut.ValidateCreditLimit(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateCreditLimit_UserHasNoCreditLimit_ReturnsTrue()
        {
            // Arrange
            var user = new User { HasCreditLimit = false };

            // Act
            var result = sut.ValidateCreditLimit(user);

            // Assert
            Assert.True(result);
        }
    }
}