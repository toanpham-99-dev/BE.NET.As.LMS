using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Infrastructures;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LMSDataContext _db;
        private readonly IServiceProvider _serviceProvider;
        private IDbContextTransaction _transaction;
        public UnitOfWork(LMSDataContext db, IServiceProvider serviceProvider)
        {
            _db = db;
            this._serviceProvider = serviceProvider;
        }
        public void BeginTransaction()
        {
            _transaction = _db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
            }
        }
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _serviceProvider.GetService<IRepository<T>>();
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
