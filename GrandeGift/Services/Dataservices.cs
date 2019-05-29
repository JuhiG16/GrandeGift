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

        public IQueryable<T> GetAll()
        {
            return _dbset;
        }

        public IQueryable<T> GetAll(string navProp)
        {
            return _dbset.Include(navProp)   ;
        }
        public T QueryGetSingle(Expression<Func<T, bool>> predecate, string navProp)
        {
            return _dbset.Include(navProp).Single(predecate);
        }
        public T GetById(int? id)
        {
            return _dbset.Find(id);
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> QueryGetAll(Expression<Func<T, bool>> predecate, string navProp)
        {
            return _dbset.Where(predecate).Include(navProp);
        }
    }
}
