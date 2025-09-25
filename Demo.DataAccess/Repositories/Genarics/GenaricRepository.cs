using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models;
using Demo.DataAccess.Models.Empolyees;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Repositories.Genarics
{
    public class GenaricRepository<TEntity> : IGenaricRepostiroy<TEntity> where TEntity : BaseEntity
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
                return _dbContext.Set<TEntity>().Where(T => T.IsDeleted != true).ToList();

            }
            else
            {
                return _dbContext.Set<TEntity>().Where(T => T.IsDeleted != true).AsNoTracking().ToList();

            }
        }

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).Select(selector).ToList();
        }

        #region IEnumrable,IQueryable
        //public IEnumerable<TEntity> GetEnumrable()
        //{
        //    return _dbContext.Set<TEntity>().Where(T => T.IsDeleted != true).AsNoTracking().ToList();
        //}

        //public IQueryable<TEntity> GetQueryable()
        //{
        //    return _dbContext.Set<TEntity>().Where(T => T.IsDeleted != true).AsNoTracking();
        //}
        #endregion

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicte)
        {
            return _dbContext.Set<TEntity>().Where(predicte).Where(predicte).ToList(); 
        }

        // Get By Id
        public TEntity? GetById(int id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            return entity;
        }

        // Insert
        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        // Update
        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        // Remove
        public void Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

    }
}
