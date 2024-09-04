using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        IRepository<T> GetRepository<T>()
            where T : class;      
    }
}
