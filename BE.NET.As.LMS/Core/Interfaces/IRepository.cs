using BE.NET.As.LMS.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(long id);
    }
}
