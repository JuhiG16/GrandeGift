using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GrandeGift.Services
{
    public interface IDataServices<T>
    {
        IQueryable<T> QueryGetAll(Expression<Func<T, bool>> predecate, string navProp);
        IQueryable<T> GetAll(string navProp);
        IQueryable<T> GetAll();
        T QueryGetSingle(Expression<Func<T, bool>> predecate, string navProp);
        void Add(T entity);
        T GetById(int? id);
        void Update(T entity);
        
        
    }
}
