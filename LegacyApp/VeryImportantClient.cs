namespace LegacyApp
{
    public class VeryImportantClient : IClientHierarchy
    {
        public bool HasCreditLimit => false;

        public int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }
    }
}
