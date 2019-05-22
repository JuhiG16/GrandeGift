using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Services
{
    public interface IDataServices<T>
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        T GetById(int? id);
        void Update(T entity);
    }
}
