using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Data.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> e, string[]? includeProps = null);
        IEnumerable<T> GetAll(string[]? includeProps = null);
        void Add(T entity);
        void Remove(T entity);
    }
}
