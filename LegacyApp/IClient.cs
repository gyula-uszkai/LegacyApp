namespace LegacyApp
{
    public interface IClient
    {
        ClientStatus ClientStatus { get; set; }
        int Id { get; set; }
        string Name { get; set; }
    }
}