using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace gestion_congregacion.api.Features.Common
{

    /// <summary>
    /// Clase que implementa la interfaz IBaseDbRepository y proporciona funcionalidad básica para acceder a la base de datos
    /// </summary>
    public class BaseDbRepository<TModel, TContext> : IBaseDbRepository<TModel>
        where TModel : class, IBaseDbModel
        where TContext : AppDbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TModel> _dbSet;

        public BaseDbRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        public TModel Add(TModel entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<bool> Delete(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<TModel>> GetAll(bool includeDeleted = false)
        {
            if (includeDeleted) return await _dbSet.ToListAsync();
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<TModel>> Find(ODataQueryOptions<TModel> options, bool includeDeleted = false)
        {
            if (includeDeleted) return await options.ApplyTo(_dbSet.AsQueryable()).OfType<TModel>().ToListAsync();
            return await options.ApplyTo(_dbSet.AsQueryable()).OfType<TModel>().Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<TModel?> GetById(long id, bool includeDeleted = false)
        {
            if (includeDeleted) return await _dbSet.FindAsync(id);
            return await _dbSet.Where(e => !e.IsDeleted && e.Id == id).FirstOrDefaultAsync();
        }

        public void Update(TModel entity)
        {
            _dbSet.Update(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
