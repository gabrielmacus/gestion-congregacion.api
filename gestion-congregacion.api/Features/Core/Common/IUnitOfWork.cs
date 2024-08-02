using System;
using System.Threading.Tasks;

namespace gestion_congregacion.api.Features.Common
{
    /// <summary>
    /// Define la interfaz para la unidad de trabajo, que encapsula el acceso a los repositorios 
    /// y la operación de guardar los cambios.
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {

        /// <summary>
        /// Guarda todos los cambios realizados en el contexto de la base de datos.
        /// </summary>
        /// <returns>El número de entidades afectadas.</returns>
        Task<int> SaveChanges();
    }
}
