using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;

namespace gestion_congregacion.api.Features.Common
{
    /// <summary>
    /// Interfaz para controladores CRUD genéricos.
    /// </summary>
    /// <typeparam name="TModel">El tipo de modelo de base de datos.</typeparam>
    public interface ICRUDController<TModel> where TModel : BaseDbModel
    {
        /// <summary>
        /// Crea un nuevo modelo.
        /// </summary>
        /// <param name="model">El modelo a crear.</param>
        /// <returns>El resultado de la operación.</returns>
        Task<IActionResult> Post([FromBody] TModel model);

        /// <summary>
        /// Obtiene una lista de modelos con opciones de consulta OData.
        /// </summary>
        /// <param name="options">Las opciones de consulta OData.</param>
        /// <returns>Una lista de modelos.</returns>
        Task<IEnumerable<TModel>> Get(ODataQueryOptions<TModel> options);

        /// <summary>
        /// Obtiene un modelo por su clave.
        /// </summary>
        /// <param name="key">La clave del modelo.</param>
        /// <returns>El resultado de la operación.</returns>
        Task<IActionResult> Get([FromODataUri] long key);

        /// <summary>
        /// Actualiza parcialmente un modelo por su clave.
        /// </summary>
        /// <param name="key">La clave del modelo.</param>
        /// <param name="delta">Los cambios a aplicar al modelo.</param>
        /// <returns>El resultado de la operación.</returns>
        Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TModel> delta);

        /// <summary>
        /// Actualiza completamente un modelo por su clave.
        /// </summary>
        /// <param name="key">La clave del modelo.</param>
        /// <param name="model">El modelo actualizado.</param>
        /// <returns>El resultado de la operación.</returns>
        Task<IActionResult> Put([FromODataUri] int key, [FromBody] TModel model);

        /// <summary>
        /// Elimina un modelo por su clave.
        /// </summary>
        /// <param name="key">La clave del modelo.</param>
        /// <returns>El resultado de la operación.</returns>
        Task<IActionResult> Delete([FromODataUri] int key);
    }
}
