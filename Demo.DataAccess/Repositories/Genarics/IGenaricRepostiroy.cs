using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models;
using Demo.DataAccess.Models.Empolyees;

namespace Demo.DataAccess.Repositories.Genarics
{
    public interface IGenaricRepostiroy<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity , TResult>> selector);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicte);
        TEntity? GetById(int id);
        void Remove(TEntity entity);
        void Update(TEntity entity);

        #region IEnumrable,IQueryable
        //IEnumerable<TEntity> GetEnumrable();
        //IQueryable<TEntity> GetQueryable();
        #endregion
    }
}
