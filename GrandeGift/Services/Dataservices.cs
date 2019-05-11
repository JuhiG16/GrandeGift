using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GrandeGift.Services
{
    public class DataServices<T> : IDataServices<T> where T: class
    {
        GrandeHamperDbContext _dbContext;
        DbSet<T> _dbset;
       
        public DataServices()
        {
            _dbContext = new GrandeHamperDbContext();
            _dbset = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public T GetById(int id)
        {
            return _dbset.Find(id);
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }
    }
}
