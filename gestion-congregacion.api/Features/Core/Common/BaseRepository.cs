using AutoMapper;
using AutoMapper.AspNet.OData;
using AutoMapper.QueryableExtensions;
using gestion_congregacion.api.Features.Publishers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace gestion_congregacion.api.Features.Common
{

    /// <summary>
    /// Clase que implementa la interfaz IBaseDbRepository y proporciona funcionalidad básica para acceder a la base de datos
    /// </summary>
    public class BaseRepository<TModel, TContext> : IBaseRepository<TModel>
        where TModel : class, IBaseModel
        where TContext : AppDbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TModel> _dbSet;

        public BaseRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        public TModel Add(TModel entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<bool> Delete(long id, bool softDelete = false)
        {
            var entity = await GetById(id);
            if (entity == null) return false;
            if (softDelete)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
            return true;
        }

        public async Task<IEnumerable<TModel>> GetAll(bool includeDeleted = false)
        {
            if (includeDeleted) return await _dbSet.ToListAsync();
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        public IQueryable<TModel> GetQuery(bool includeDeleted = false)
        {
            return includeDeleted ? _dbSet.AsQueryable() : _dbSet.Where(e => !e.IsDeleted);
        }

        public async Task<TModel?> GetById(long id, bool includeDeleted = false)
        {
            var query = includeDeleted ? _dbSet.AsQueryable() : _dbSet.Where(e => !e.IsDeleted);
            return await query.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateById(TModel entity, long id, bool updateDeleted = false)
        {
            if (await GetById(id, updateDeleted) == null) return false;
            entity.Id = id;
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            return true;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
