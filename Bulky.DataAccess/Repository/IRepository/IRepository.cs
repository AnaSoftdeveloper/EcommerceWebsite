using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T-Category
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> predicate); // when we want to write LINQ expression this is the general syntax
        //FirstOrDefault(u => u.name = name)
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
