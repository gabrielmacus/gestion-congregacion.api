using Microsoft.AspNetCore.OData.Query;

namespace gestion_congregacion.api.Features.Common
{
    /// <summary>
    /// Interfaz que define los métodos básicos para acceder a la base de datos
    /// </summary>
    public interface IBaseDbRepository<TModel> : IDisposable
    {
        /// <summary>
        /// Método para agregar una entidad a la base de datos
        /// </summary>
        /// <param name="entity">La entidad a agregar</param>
        /// <returns>La entidad agregada</returns>
        TModel Add(TModel entity);

        /// <summary>
        /// Método para eliminar una entidad de la base de datos
        /// </summary>
        /// <param name="id">El identificador de la entidad a eliminar</param>
        /// <returns>Un valor booleano que indica si la entidad fue eliminada correctamente</returns>
        Task<bool> Delete(long id);

        /// <summary>
        /// Método asincrónico para obtener todas las entidades de la base de datos.
        /// </summary>
        /// <param name="includeDeleted">Indica si se deben incluir las entidades eliminadas en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una tarea que representa la operación de obtención de todas las entidades</returns>
        Task<IEnumerable<TModel>> GetAll(bool includeDeleted = false);

        /// <summary>
        /// Método asincrónico para buscar entidades en la base de datos utilizando opciones de consulta OData.
        /// </summary>
        /// <param name="options">Las opciones de consulta OData</param>
        /// <param name="includeDeleted">Indica si se deben incluir las entidades eliminadas en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una tarea que representa la operación de búsqueda de entidades</returns>
        Task<IEnumerable<TModel>> Find(ODataQueryOptions<TModel> options, bool includeDeleted = false);

        /// <summary>
        /// Método asincrónico para obtener una entidad de la base de datos por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad</param>
        /// <param name="includeDeleted">Indica si se debe incluir la entidad eliminada en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una tarea que representa la operación de obtención de la entidad</returns>
        Task<TModel?> GetById(long id, bool includeDeleted = false);

        /// <summary>
        /// Método para actualizar una entidad en la base de datos
        /// </summary>
        /// <param name="entity">La entidad a actualizar</param>
        void Update(TModel entity);

        /// <summary>
        /// Método asincrónico para guardar los cambios en la base de datos
        /// </summary>
        /// <returns>Una tarea que representa la operación de guardado de cambios</returns>
        Task SaveChanges();
    }
}
