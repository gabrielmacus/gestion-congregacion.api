using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace gestion_congregacion.api.Features.Operations
{
    public class DeleteOperation<T, TRepository> : ODataController, IDeleteOperation<T>
            where T : class, ISoftDeleteModel, new()
            where TRepository : IBaseRepository<T>
    {
        private readonly TRepository _repository;
        private bool _disposed = false;

        public DeleteOperation(TRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Delete([FromODataUri] long key)
        {
            var entityFound = await _repository.Delete(key);
            if (!entityFound) return NotFound();
            await _repository.SaveChanges();
            return Ok();
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
