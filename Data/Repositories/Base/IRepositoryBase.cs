using System.Linq.Expressions;

namespace estatedocflow.api.Data.Repositories.Base
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
        Task SaveChangesAsync();
        void BulkCreate(IQueryable<T> Entities);
        void BulkUpdate(IQueryable<T> Entities);
        void BulkDelete(IQueryable<T> Entities);
    }
}
