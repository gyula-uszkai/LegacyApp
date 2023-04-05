namespace LegacyApp
{
    public class StandardClient : IClientHierarchy
    {
        public bool HasCreditLimit => true;

        public int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(firstname, surname, dateOfBirth);
                return creditLimit;
            }
        }
    }
}
