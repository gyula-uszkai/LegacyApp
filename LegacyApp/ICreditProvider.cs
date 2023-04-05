namespace LegacyApp
{
    public interface ICreditProvider
    {
        void CalculateCreditLimit(User user, IClientHierarchy hierarchy);
    }
}