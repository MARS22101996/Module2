using System;

namespace CDP.AdoNet.Interfaces
{
    public interface ITransactionWrapperConnected : IDisposable
    {
        void SaveChanges();

        void Rollback();
    }
}
