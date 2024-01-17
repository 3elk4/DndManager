using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //DbSet<TEntity> Entities { get; } //???

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        ValueTask<TEntity> GetByID(object id, string includeProperties = "");
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void UpdateMany(IQueryable<TEntity> entitiesToUpdate);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Save();
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
