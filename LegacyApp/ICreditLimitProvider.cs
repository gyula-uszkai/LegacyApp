namespace LegacyApp
{
    public interface ICreditLimitProvider
    {
        void ApplyCreditLimit(User user);

        bool ValidateCreditLimit(User user);
    }
}