using gestion_congregacion.api.Features.Operations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace gestion_congregacion.api.Features.Common
{
    public class CRUDController<T> : ODataController, ICRUDController<T>
            where T : class, IBaseModel, new()
    {
        protected readonly IReadOperation<T>? _readOperation;
        protected readonly ICreateOperation<T>? _createOperation;
        protected readonly IUpdateOperation<T>? _updateOperation;
        protected readonly IDeleteOperation<T>? _deleteOperation;

        public CRUDController(
            IReadOperation<T>? readOperation = null,
            ICreateOperation<T>? createOperation = null,
            IUpdateOperation<T>? updateOperation = null,
            IDeleteOperation<T>? deleteOperation = null)
        {
            _readOperation = readOperation;
            _createOperation = createOperation;
            _updateOperation = updateOperation;
            _deleteOperation = deleteOperation;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(long key, ODataQueryOptions<T> options)
        {
            if (_readOperation is not null) return await _readOperation.Get(key, options);
            return NotFound();
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(ODataQueryOptions<T>  options, ODataValidationSettings validationSettings)
        {
            if (_readOperation is not null) return await _readOperation.Get(options, validationSettings);
            return NotFound();
        }

        [HttpPost]
        [ValidateModel]
        public virtual async Task<IActionResult> Post([FromBody] T model)
        {
            if (_createOperation is not null) return await _createOperation.Post(model);
            return NotFound();
        }

        [HttpPut]
        [ValidateModel]
        public virtual async Task<IActionResult> Put([FromODataUri] long key, [FromBody] T model)
        {
            if (_updateOperation is not null) return await _updateOperation.Put(key, model);
            return NotFound();
        }

        [HttpPatch]
        [ValidateModel]
        public virtual async Task<IActionResult> Patch([FromODataUri] long key, [FromBody] Delta<T> model)
        {
            if (_updateOperation is not null) return await _updateOperation.Patch(key, model);
            return NotFound();
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete([FromODataUri] long key)
        {
            if (_deleteOperation is not null) return await _deleteOperation.Delete(key);
            return NotFound();
        }

        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)  return;
            if (disposing)
            {
                // Dispose managed resources here
                _readOperation?.Dispose();
                _createOperation?.Dispose();
                _updateOperation?.Dispose();
                _deleteOperation?.Dispose();
            }
            // Dispose unmanaged resources here
            _disposed = true;
        }
    }
}
