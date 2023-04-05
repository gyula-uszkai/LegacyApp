namespace LegacyApp
{
    public class UserValidator : IUserValidator
    {
        private const int AgeLimit = 21;

        public bool IsUserValid(User user)
        {
            return IsNameValid(user) && IsEmailValid(user) && IsAgeValid(user);
        }

        private bool IsNameValid(User user)
        {
            return !string.IsNullOrEmpty(user.Firstname) && !string.IsNullOrEmpty(user.Surname);
        }

        private bool IsEmailValid(User user)
        {
            // Consider using regex or other pattern matching
            return user.EmailAddress.Contains("@") || user.EmailAddress.Contains(".");
        }

        private bool IsAgeValid(User user)
        {
            int age = CalculateAge(user);
            return age >= AgeLimit;
        }

        private static int CalculateAge(User user)
        {
            var now = DateTime.Now; // TODO extract datetime service
            int age = now.Year - user.DateOfBirth.Year;
            bool isBirthDayLaterThisYear = now.Month < user.DateOfBirth.Month ||
                                           (now.Month == user.DateOfBirth.Month && now.Day < user.DateOfBirth.Day);
            if (isBirthDayLaterThisYear)
            {
                age--;
            }

            return age;
        }
    }
}
