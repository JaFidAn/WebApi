using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Entities;

namespace WebApi.Repository
{
    public interface IRepository<TEntity, in TPrimaryKey> : IDisposable where TEntity : BaseEntity<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<List<TEntity>> GetAllList();

        Task<List<TEntity>> GetAllListIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        ValueTask<TEntity> Find(TPrimaryKey id);

        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

        Task<bool> All(Expression<Func<TEntity, bool>> predicate);

        Task<int> Count();

        Task<int> Count(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteWhere(Expression<Func<TEntity, bool>> predicate);

        Task Commit(CancellationToken cancellationToken = default);
    }
}
