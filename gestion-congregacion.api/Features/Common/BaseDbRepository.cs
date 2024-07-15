using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace gestion_congregacion.api.Features.Common
{

    /// <summary>
    /// Clase que implementa la interfaz IBaseDbRepository y proporciona funcionalidad básica para acceder a la base de datos
    /// </summary>
    public class BaseDbRepository<TModel, TContext> : IBaseDbRepository<TModel>
        where TModel : BaseDbModel
        where TContext : AppDbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TModel> _dbSet;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos y asigna los valores a las variables de instancia
        /// </summary>
        /// <param name="context">El contexto de la base de datos</param>
        public BaseDbRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }


        /// <summary>
        /// Método para agregar una entidad a la base de datos
        /// </summary>
        /// <param name="entity">La entidad a agregar</param>
        /// <returns>La entidad agregada</returns>
        public TModel Add(TModel entity)
        {
            _dbSet.Add(entity);
            return entity;
        }


        /// <summary>
        /// Método para eliminar una entidad de la base de datos
        /// </summary>
        /// <param name="id">El identificador de la entidad a eliminar</param>
        /// <returns>Un valor booleano que indica si la entidad fue eliminada correctamente</returns>
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


        /// <summary>
        /// Método asincrónico para obtener todas las entidades de la base de datos.
        /// </summary>
        /// <param name="includeDeleted">Indica si se deben incluir las entidades eliminadas en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una tarea que representa la operación de obtención de todas las entidades</returns>
        public async Task<IEnumerable<TModel>> GetAll(bool includeDeleted = false)
        {
            if(includeDeleted) return await _dbSet.ToListAsync();
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        /// <summary>
        /// Método asincrónico para buscar entidades en la base de datos utilizando opciones de consulta OData.
        /// </summary>
        /// <param name="options">Las opciones de consulta OData</param>
        /// <param name="includeDeleted">Indica si se deben incluir las entidades eliminadas en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una tarea que representa la operación de búsqueda de entidades</returns>
        public async Task<IEnumerable<TModel>> Find(ODataQueryOptions<TModel> options, bool includeDeleted = false)
        {
            // Si se deben incluir las entidades eliminadas, se aplica la consulta OData a todas las entidades del DbSet y se convierte el resultado a una lista asincrónica de entidades del tipo TModel
            if (includeDeleted) return await options.ApplyTo(_dbSet.AsQueryable()).OfType<TModel>().ToListAsync();

            // Si no se deben incluir las entidades eliminadas, se aplica la consulta OData a las entidades del DbSet que no están marcadas como eliminadas y se convierte el resultado a una lista asincrónica de entidades del tipo TModel
            return await options.ApplyTo(_dbSet.AsQueryable()).OfType<TModel>().Where(e => !e.IsDeleted).ToListAsync();
        }


        /// <summary>
        /// Método asincrónico para obtener una entidad de la base de datos por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad</param>
        /// <param name="includeDeleted">Indica si se debe incluir la entidad eliminada en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una tarea que representa la operación de obtención de la entidad</returns>
        public async Task<TModel?> GetById(long id, bool includeDeleted = false)
        {
            if (includeDeleted) return await _dbSet.FindAsync(id);
            return await _dbSet.Where(e => !e.IsDeleted && e.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Método para actualizar una entidad en la base de datos
        /// </summary>
        /// <param name="entity">La entidad a actualizar</param>
        public void Update(TModel entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Método asincrónico para guardar los cambios en la base de datos
        /// </summary>
        /// <returns>Una tarea que representa la operación de guardado de cambios</returns>
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
