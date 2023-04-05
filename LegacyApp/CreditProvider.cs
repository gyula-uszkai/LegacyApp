namespace LegacyApp
{
    public class CreditProvider : ICreditProvider
    {
        public void CalculateCreditLimit(User user, IClientHierarchy hierarchy)
        {
            user.HasCreditLimit = hierarchy.HasCreditLimit;
            if (user.HasCreditLimit)
            {
                user.CreditLimit = hierarchy.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
            }


        }
    }
}
