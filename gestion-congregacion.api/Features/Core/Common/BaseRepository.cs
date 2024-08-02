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
        private readonly IMapper _mapper;

        public BaseRepository(TContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
            _mapper = mapper;
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

        public async Task<IEnumerable<TMap>> Find<TMap>(
            ODataQueryOptions<TMap> options,
            bool includeDeleted = false) 
                where TMap : class, IBaseModel, new()
        {
            var querySet = includeDeleted ? _dbSet.AsQueryable() : _dbSet.AsQueryable().Where(i => !i.IsDeleted);

            var mapRequired = typeof(TMap) != typeof(TModel);
            if (!mapRequired) return await options.ApplyTo(querySet).OfType<TMap>().ToListAsync();

            var query = await querySet.GetQueryAsync(_mapper, options, null);
            return await query.ToListAsync();

        }

        public IQueryable FindDynamic<T>(
            ODataQueryOptions<T> options,
            bool includeDeleted = false) where T : class, IBaseModel, new()
        {
            var querySet = includeDeleted ? _dbSet.AsQueryable() : _dbSet.AsQueryable().Where(i => !i.IsDeleted);

            return  options.ApplyTo(querySet);
        }

        public async Task<TModel?> GetById(long id, bool includeDeleted = false)
        {
            var query = includeDeleted ? _dbSet.AsQueryable() : _dbSet.Where(e => !e.IsDeleted);
            return await query.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TMap?> GetById<TMap>(long id, ODataQueryOptions<TMap> options, bool includeDeleted = false)
            where TMap : class, IBaseModel, new()
        {
            var query = includeDeleted ? _dbSet.AsQueryable() : _dbSet.Where(e => !e.IsDeleted);
            query = query.Where(i => i.Id == id);

            var mapRequired = typeof(TMap) != typeof(TModel);
            if (!mapRequired) return await options.ApplyTo(query).OfType<TMap>().FirstOrDefaultAsync();

            var querySet = await query.GetQueryAsync(_mapper, options, null);
            return await querySet.FirstOrDefaultAsync();
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
