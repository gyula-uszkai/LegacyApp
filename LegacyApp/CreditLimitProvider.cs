namespace LegacyApp
{
    public class CreditLimitProvider : ICreditLimitProvider
    {
        private const int MinimalCreditLimit = 500;

        private readonly IDictionary<string, IClientCreditService> clientCreditServices;

        public CreditLimitProvider(IClientCreditServiceFactory clientCreditServiceFactory)
        {
            this.clientCreditServices = clientCreditServiceFactory.GetClientCreditServices();
        }

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

            if (!this.clientCreditServices.TryGetValue(user.Client.Name, out var clientCreditService))
            {
                throw new Exception($"Unsupported client: {user.Client.Name}");
            }

            user.HasCreditLimit = clientCreditService.HasCreditLimit;
            if (user.HasCreditLimit)
            {
                user.CreditLimit = clientCreditService.GetCreditLimit(user);
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


