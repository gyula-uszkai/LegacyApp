namespace LegacyApp
{
    public interface ICreditProvider
    {
        void CalculateCreditLimit(User user, IClientHierarchy hierarchy);

        bool IsUserCreditValid(User user);
    }
}