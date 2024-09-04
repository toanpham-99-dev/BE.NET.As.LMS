using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Infrastructures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Repository
{
    public class ERepository<T> : IRepository<T> where T : class
    {
        private readonly LMSDataContext _db;
        public ERepository(LMSDataContext db)
        {
            _db = db;
        }

        public T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            return entity;
        }

        public IQueryable<T> AsQueryable()
        {
            return _db.Set<T>().AsQueryable();
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public T Get(long id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
