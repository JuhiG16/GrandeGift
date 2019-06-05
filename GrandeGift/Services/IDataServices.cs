using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GrandeGift.Services
{
    public interface IDataServices<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Expression<Func<T, bool>> predecate, string navProp);
        IEnumerable<T> Query(Expression<Func<T, bool>> predecate);

        IEnumerable<T> GetAll(string navProp);
       

        T GetSingle(Expression<Func<T, bool>> predecate);
        T GetSingle(Expression<Func<T, bool>> predecate, string navProp);
       
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Update(T entity);       
    }
}
