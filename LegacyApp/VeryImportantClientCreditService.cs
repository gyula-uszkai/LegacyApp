namespace LegacyApp
{
    public class VeryImportantClientCreditService : IClientCreditService
    {
        public bool HasCreditLimit => false;

        public int GetCreditLimit(User user)
        {
            throw new NotImplementedException();
        }
    }
}
