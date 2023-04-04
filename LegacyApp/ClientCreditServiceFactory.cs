namespace LegacyApp
{
    public class ClientCreditServiceFactory : IClientCreditServiceFactory
    {
        public IDictionary<string, IClientCreditService> GetClientCreditServices()
        {
            // Initialize and return the dictionary with client credit services
            var clientCreditServices = new Dictionary<string, IClientCreditService>();
            clientCreditServices.Add("VeryImportantClient", new VeryImportantClientCreditService());
            clientCreditServices.Add("ImportantClient", new ImportantClientCreditService());
            clientCreditServices.Add("RegularClient", new RegularClientCreditService());
            return clientCreditServices;
        }
    }
}


