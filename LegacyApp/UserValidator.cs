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

            if (CalculateAge(user.DateOfBirth, now) < AgeLimit)
            {
                return false;
            }

            return true;
        }

        private static int CalculateAge(DateTime dateOfBirth, DateTime now)
        {
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
    }
}
