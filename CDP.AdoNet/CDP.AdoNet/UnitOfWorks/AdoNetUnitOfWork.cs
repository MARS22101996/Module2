using System;
using System.Data;
using CDP.AdoNet.Interfaces;

namespace CDP.AdoNet.UnitOfWorks
{
    public class AdoNetUnitOfWork : IUnitOfWorkAdo
    {
        private IDbConnection _connection;

        private readonly bool _ownsConnection;

        private IDbTransaction _transaction;

        public AdoNetUnitOfWork(IDbConnection connection, bool ownsConnection)
        {
            _connection = connection;
            _ownsConnection = ownsConnection;
            _transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction have already been commited. Check your transaction handling.");
            _transaction.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction have already been rallbacked. Check your transaction handling.");
            _transaction.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null && _ownsConnection)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
