namespace LegacyApp
{
    public class Client : IClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClientStatus ClientStatus { get; set; }
    }
}

