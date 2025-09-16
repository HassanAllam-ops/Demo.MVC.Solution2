using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.Empolyees;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Repositories.Genarics
{
    public class GenaricRepository<TEntity> : IGenaricRepostiroy<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenaricRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // CRUD Operations
        // Get All
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set<TEntity>().ToList();

            }
            else
            {
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();

            }
        }
        // Get By Id
        public TEntity? GetById(int id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            return entity;
        }

        // Insert
        public int Add(TEntity entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }

        // Update
        public int Update(TEntity entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }

        // Remove
        public int Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
