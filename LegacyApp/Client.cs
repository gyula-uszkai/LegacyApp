namespace LegacyApp
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IClientHierarchy ClientHierarchy { get; set; }
        public ClientStatus ClientStatus { get; set; }
    }
}

