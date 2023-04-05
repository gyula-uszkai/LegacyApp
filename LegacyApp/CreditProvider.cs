﻿namespace LegacyApp
{
    public class CreditProvider : ICreditProvider
    {
        private const int MinimalCreditLimit = 500;

        public void CalculateCreditLimit(User user, IClientHierarchy hierarchy)
        {
            user.HasCreditLimit = hierarchy.HasCreditLimit;
            if (user.HasCreditLimit)
            {
                user.CreditLimit = hierarchy.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
            }
        }

        public bool IsUserCreditValid(User user)
        {
            return !user.HasCreditLimit || user.CreditLimit >= MinimalCreditLimit;
        }
    }
}