using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace gestion_congregacion.api.Features.Operations
{
    public class CreateOperation<T, TRepository> : ODataController, ICreateOperation<T>
            where T : class, IBaseModel, new()
            where TRepository : IBaseRepository<T>
    {
        protected readonly TRepository _repository;
        private bool _disposed = false;

        public CreateOperation(TRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        /// <returns>El modelo creado.</returns>
        public virtual async Task<IActionResult> Post([FromBody] T model)
        {
            _repository.Add(model);
            await _repository.SaveChanges();
            return Created(model);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _repository.Dispose();
                }

                // Dispose unmanaged resources

                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
