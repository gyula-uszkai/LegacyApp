namespace LegacyApp
{
    public class CreditLimitProvider : ICreditLimitProvider
    {
        private const int MinimalCreditLimit = 500;

        public void ApplyCreditLimit(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Client == null)
            {
                throw new ArgumentException("User's client information is not set.", nameof(user));
            }

            if (user.Client.Name == "VeryImportantClient")
            {
                // Skip credit check
                user.HasCreditLimit = false;
            }
            else if (user.Client.Name == "ImportantClient")
            {
                // Do credit check and double credit limit
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Do credit check
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
        }

        public bool ValidateCreditLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < MinimalCreditLimit)
            {
                return false;
            }

            return true;
        }
    }
}
