using System;

namespace CDP.AdoNet.Interfaces
{
    public interface IUnitOfWorkAdo : IDisposable
    {
        void SaveChanges();

        void Rollback();
    }
}
