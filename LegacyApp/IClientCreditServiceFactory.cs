namespace LegacyApp
{
    public interface IClientCreditServiceFactory
    {
        IDictionary<string, IClientCreditService> GetClientCreditServices();
    }
}


