using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace gestion_congregacion.api.Features.Operations
{
    public class UpdateOperation<T, TRepository> : ODataController, IUpdateOperation<T>
            where T : class, IBaseModel, new()
            where TRepository : IBaseRepository<T>
    {
        private readonly TRepository _repository;
        private bool _disposed = false;

        public UpdateOperation(TRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] long key, [FromBody] T model)
        {
            var entity = await _repository.GetById(key);
            if (entity == null) return NotFound();

            await _repository.UpdateById(model, key);
            await _repository.SaveChanges();
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] long key, [FromBody] Delta<T> model)
        {
            var entity = await _repository.GetById(key);
            if (entity == null) return NotFound();

            model.Patch(entity);
            await _repository.UpdateById(entity, key);
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
