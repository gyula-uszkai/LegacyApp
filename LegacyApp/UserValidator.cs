using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserValidator : IUserValidator
    {
        private const int AgeLimit = 21;
        private readonly IDateTimeService dateTimeService;

        public UserValidator(IDateTimeService dateTimeService)
        {
            this.dateTimeService = dateTimeService;
        }


        public bool ValidateUser(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Firstname) || string.IsNullOrWhiteSpace(user.Surname) || string.IsNullOrWhiteSpace(user.EmailAddress))
            {
                return false;
            }

            // TODO have a better way of validating email patterns...ex regex? see https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
            if (!Regex.IsMatch(user.EmailAddress,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                return false;
            }

            var now = this.dateTimeService.Now;
            int age = now.Year - user.DateOfBirth.Year;
            if (now.Month < user.DateOfBirth.Month || (now.Month == user.DateOfBirth.Month && now.Day < user.DateOfBirth.Day))
            {
                age--;
            }

            if (age < AgeLimit)
            {
                return false;
            }

            return true;
        }
    }
}
