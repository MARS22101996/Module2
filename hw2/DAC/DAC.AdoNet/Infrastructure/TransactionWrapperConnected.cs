using System;
using System.Data;
using CDP.AdoNet.Interfaces;

namespace CDP.AdoNet.Infrastructure
{
    public class TransactionWrapperConnected : ITransactionWrapperConnected
    {
        private IDbConnection _connection;

        private readonly bool _ownsConnection;

        private IDbTransaction _transaction;

        private const string SaveChangesException =
            "Transaction have already been commited. Check your transaction handling.";

        private const string RollbackException =
            "Transaction have already been rollbacked. Check your transaction handling.";

        public TransactionWrapperConnected(IDbConnection connection, bool ownsConnection)
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
                throw new InvalidOperationException(
                    SaveChangesException);
            _transaction.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction == null)
                throw new InvalidOperationException(
                    RollbackException);
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