namespace LegacyApp
{
    public interface IClientHierarchy
    {
        bool HasCreditLimit { get; }

        int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);
    }
}