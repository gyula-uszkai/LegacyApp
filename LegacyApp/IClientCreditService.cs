namespace LegacyApp
{
    public interface IClientCreditService
    {
        bool HasCreditLimit { get; }
        int GetCreditLimit(User user);
    }
}
