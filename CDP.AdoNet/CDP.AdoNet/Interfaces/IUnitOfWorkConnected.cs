using System;

namespace CDP.AdoNet.Interfaces
{
    public interface IUnitOfWorkConnected : IDisposable
    {
        void SaveChanges();

        void Rollback();
    }
}
