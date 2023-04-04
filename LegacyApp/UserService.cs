

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository clientRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserValidator userValidator;
        private readonly ICreditLimitProvider creditLimitProvider;

        public UserService() : this(new ClientRepository(), new UserRepository(), new UserValidator(new DateTimeService()), new CreditLimitProvider())
        { }

        public UserService(IClientRepository clientRepository, IUserRepository userRepository, IUserValidator userValidator, ICreditLimitProvider creditLimitProvider)
        {
            this.clientRepository = clientRepository;
            this.userRepository = userRepository;
            this.userValidator = userValidator;
            this.creditLimitProvider = creditLimitProvider;
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

            if (!userValidator.ValidateUser(user))
            {
                return false;
            }

            user.Client = this.clientRepository.Get(clientld);

            creditLimitProvider.ApplyCreditLimit(user);

            if (!creditLimitProvider.ValidateCreditLimit(user))
            {
                return false;
            }

            this.userRepository.AddUser(user);

            return true;
        }
    }
}