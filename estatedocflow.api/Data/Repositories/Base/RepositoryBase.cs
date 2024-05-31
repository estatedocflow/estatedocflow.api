using estatedocflow.api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace estatedocflow.api.Data.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RealEstateDbContext _context;
        protected RepositoryBase(RealEstateDbContext context)
        {
            _context = context;
        }
        public virtual void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }
        public virtual void BulkCreate(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public virtual void BulkUpdate(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }
        public virtual void BulkDelete(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
