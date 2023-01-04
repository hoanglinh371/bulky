using BulkyBook.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string[]? includeProps = null)
        {
            IQueryable<T> q = _dbSet;
            if (includeProps != null)
            {
                foreach(var prop in includeProps)
                {
                    q = q.Include(prop);
                }
            }
            return q.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> e, string[]? includeProps = null)
        {
            IQueryable<T> q = _dbSet;
            q = q.Where(e);
            return q.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
