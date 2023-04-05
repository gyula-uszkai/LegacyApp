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

        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var user = CreateUser(firname, surname, email, dateOfBirth);

            if (!userValidator.IsUserValid(user))
            {
                return false;
            }

            var client = GetClient(clientId);
            user.Client = client;

            this.creditProvider.CalculateCreditLimit(user, client.ClientHierarchy);

            if (!this.creditProvider.IsUserCreditValid(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }

        private static User CreateUser(string firstname, string surname, string email, DateTime dateOfBirth)
        {
            return new User
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstname,
                Surname = surname
            };
        }

        private static Client GetClient(int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            SetClientHierarchy(client);

            return client;
        }

        private static void SetClientHierarchy(Client client)
        {
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
        }
    }
}