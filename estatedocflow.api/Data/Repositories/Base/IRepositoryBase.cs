using System.Linq.Expressions;

namespace estatedocflow.api.Data.Repositories.Base
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        void BulkCreate(IEnumerable<T> entities);
        void BulkUpdate(IEnumerable<T> entities);
        void BulkDelete(IEnumerable<T> entities);
    }
}
