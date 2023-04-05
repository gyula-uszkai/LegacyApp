

namespace LegacyApp
{
    public class UserService
    {
        private readonly IUserValidator userValidator;

        public UserService() : this(new UserValidator())
        {
        }

        public UserService(IUserValidator userValidator)
        {
            this.userValidator = userValidator;
        }

        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientld)
        {
            var user = new User
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };

            if (!userValidator.IsUserValid(user))
            {
                return false;
            }

            var client = GetClient(clientld);
            user.Client = client;


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
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }

        private static Client GetClient(int clientld)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientld);
            return client;
        }
    }
}