namespace LegacyApp
{
    public class ImportantClientCreditService : IClientCreditService
    {
        public bool HasCreditLimit => true;

        public int GetCreditLimit(User user)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                return creditLimit * 2;
            }
        }
    }
}
