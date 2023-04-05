using Xunit;

namespace LegacyApp.Tests
{
    public class UserServiceTests
    {
        [Fact()]
        public void AddUserTest_Executes()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "john@doe.com", new DateTime(1993, 1, 1), 4);
            Assert.True(true, "This test needs an implementation");
        }
    }
}