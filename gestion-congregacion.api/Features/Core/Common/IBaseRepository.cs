using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;

namespace gestion_congregacion.api.Features.Common
{
    /// <summary>
    /// Interfaz que define los métodos básicos para acceder a la base de datos
    /// </summary>
    public interface IBaseRepository<TModel> : IDisposable
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
        /// <param name="softDelete">Indica si se debe realizar un borrado suave o eliminar la entrada de la base de datos (opcional, valor predeterminado: true)</param>
        /// <returns>Un valor booleano que indica si la eliminación fue exitosa</returns>
        Task<bool> Delete(long id, bool softDelete = true);

        /// <summary>
        /// Método asincrónico para obtener todas las entidades de la base de datos.
        /// </summary>
        /// <param name="includeDeleted">Indica si se deben incluir las entidades eliminadas en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una tarea que representa la operación de obtención de todas las entidades</returns>
        Task<IEnumerable<TModel>> GetAll(bool includeDeleted = false);

        /// <summary>
        /// Método para obtener una consulta IQueryable de las entidades de la base de datos.
        /// </summary>
        /// <param name="includeDeleted">Indica si se deben incluir las entidades eliminadas en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una consulta IQueryable de las entidades</returns>
        IQueryable<TModel> GetQuery(bool includeDeleted = false);

        /// <summary>
        /// Método asincrónico para obtener una entidad de la base de datos por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad</param>
        /// <param name="includeDeleted">Indica si se debe incluir la entidad eliminada en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Una tarea que representa la operación de obtención de la entidad</returns>
        Task<TModel?> GetById(long id, bool includeDeleted = false);


        /// <summary>
        /// Método asincrónico para actualizar una entidad en la base de datos por su identificador.
        /// </summary>
        /// <param name="entity">La entidad a actualizar</param>
        /// <param name="id">El identificador de la entidad</param>
        /// <param name="updateDeleted">Indica si se debe actualizar la entidad eliminada en la consulta (opcional, valor predeterminado: false)</param>
        /// <returns>Un valor booleano que indica si la actualización fue exitosa</returns>
        Task<bool> UpdateById(TModel entity, long id, bool updateDeleted = false);

        /// <summary>
        /// Método asincrónico para guardar los cambios en la base de datos
        /// </summary>
        /// <returns>Una tarea que representa la operación de guardado de cambios</returns>
        Task SaveChanges();
    }
}
