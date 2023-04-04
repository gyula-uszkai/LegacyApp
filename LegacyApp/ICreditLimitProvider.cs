namespace LegacyApp
{
    public interface ICreditLimitProvider
    {
        void ApplyCreditLimit(User user, IClient client);

        bool ValidateCreditLimit(User user);
    }
}