namespace LegacyApp
{
    /*
     * Don't change any code in this file (imagine that this is a WCF service refference
     */
    public interface IUserCreditService
    {
        int GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth);
    }

    public class UserCreditServiceClient : IDisposable, IUserCreditService
    {
        private bool disposedValue;

        public int GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth)
        {
            return firstName.Length + lastName.Length + dateOfBirth.Day;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UserCreditServiceClient()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
