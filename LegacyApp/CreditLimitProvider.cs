namespace LegacyApp
{
    public class CreditLimitProvider : ICreditLimitProvider
    {
        private const int MinimalCreditLimit = 500;

        public void ApplyCreditLimit(User user, IClient client)
        {
            if (client.Name == "VeryImportantClient")
            {
                // Skip credit check
                user.HasCreditLimit = false;
            }
            else if (client.Name == "ImportantClient")
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
