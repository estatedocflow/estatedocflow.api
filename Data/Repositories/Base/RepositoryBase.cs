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
        public virtual void Create(T Entity)
        {
            _context.Set<T>().Add(Entity);
        }
        public virtual void Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
        }
        public void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
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
        public virtual void BulkCreate(IQueryable<T> Entities)
        {
            _context.Set<T>().AddRange(Entities);
        }
        public virtual void BulkUpdate(IQueryable<T> Entities)
        {
            _context.Set<T>().UpdateRange(Entities);
        }
        public virtual void BulkDelete(IQueryable<T> Entities)
        {
            _context.Set<T>().RemoveRange(Entities);
        }
    }
}
