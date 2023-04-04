namespace LegacyApp.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProveAddUser(args);
        }
        public static void ProveAddUser(string[] args)
        {
            // DO NOT CHANGE THIS FILE AT ALL - so basically no breacking changes in the UserService
            var userService = new UserService();
            var addResult = userService.AddUser("John", "Doe", "john@doe.com", new DateTime(1993, 1, 1), 4);
            Console.WriteLine("Adding John Doe to the service was " + (addResult ? "successful" : "unsuccessful"));


        }
    }
}