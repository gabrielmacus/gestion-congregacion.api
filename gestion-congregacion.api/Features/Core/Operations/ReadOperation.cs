using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace gestion_congregacion.api.Features.Operations
{
    public class ReadOperation<TMap, TModel, TRepository> : ODataController, IReadOperation<TMap>
            where TModel : class, IBaseModel, new()
            where TMap : class, IBaseModel, new()
            where TRepository : IBaseRepository<TModel>
    {
        protected readonly TRepository _repository;
        private bool disposed = false;

        public ReadOperation(TRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        /// <returns>Los modelos filtrados.</returns>
        /// 
        public virtual async Task<IActionResult> Get(
            ODataQueryOptions<TMap> options,
            ODataValidationSettings validationSettings)
        {
            options.Validate(validationSettings);

            var shouldMap = typeof(TMap) != typeof(TModel);
            if(shouldMap) return Ok(await _repository.Find(options));
            return Ok(_repository.FindDynamic(options));

        }

        /// <inheritdoc/>
        /// <returns>El modelo encontrado.</returns>
        public virtual async Task<IActionResult> Get(long key, ODataQueryOptions<TMap> options )
        {
            var validationSettings = new ODataValidationSettings();
            validationSettings.AllowedQueryOptions =
                AllowedQueryOptions.Select | AllowedQueryOptions.Expand;
            options.Validate(validationSettings);

            var model = await _repository.GetById(key, options);
            if (model == null) return NotFound();
            return Ok(model);
        }

        /// <inheritdoc/>
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Dispose managed resources here
                _repository.Dispose();
            }

            // Dispose unmanaged resources here

            disposed = true;
        }
    }
}
