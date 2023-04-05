namespace LegacyApp
{
    public class UserService
    {
        private readonly IUserValidator userValidator;
        private readonly ICreditProvider creditProvider;

        public UserService() : this(new UserValidator(), new CreditProvider())
        {
        }

        public UserService(IUserValidator userValidator, ICreditProvider creditProvider)
        {
            this.userValidator = userValidator;
            this.creditProvider = creditProvider;
        }

        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientld)
        {
            var user = CreateUser(firname, surname, email, dateOfBirth);

            if (!userValidator.IsUserValid(user))
            {
                return false;
            }

            var client = GetClient(clientld);
            user.Client = client;

            this.creditProvider.CalculateCreditLimit(user, client.ClientHierarchy);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }

        private static User CreateUser(string firname, string surname, string email, DateTime dateOfBirth)
        {
            return new User
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };
        }

        private static Client GetClient(int clientld)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientld);

            if (client.Name == "VeryImportantClient")
            {
                client.ClientHierarchy = new VeryImportantClient();
            }
            else if (client.Name == "ImportantClient")
            {
                client.ClientHierarchy = new ImportantClient();
            }
            else
            {
                client.ClientHierarchy = new StandardClient();
            }

            return client;
        }
    }
}