using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<T> Query(Expression<Func<T, bool>> predecate, string navProp)
        {
            return _dbset.Include(navProp).Where(predecate).ToList();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predecate)
        {
            return _dbset.Where(predecate).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> predecate)
        {
            return _dbset.FirstOrDefault(predecate);
        }

        public T GetSingle(Expression<Func<T, bool>> predecate, string navProp)
        {
            return _dbset.Include(navProp).FirstOrDefault(predecate);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
            _dbContext.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
            _dbContext.SaveChanges();
        }
         public IEnumerable<T> GetAll(string navProp)
        {
            return _dbset.Include(navProp).ToList();
        }

    }
}
