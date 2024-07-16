using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace gestion_congregacion.api.Features.Common
{

    public class CRUDController<TModel, TRepository> : ODataController, ICRUDController<TModel>
                where TModel : class
                where TRepository : IBaseDbRepository<TModel>
    {
        protected readonly TRepository _repository;
        public CRUDController(TRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        /// <returns>The created model.</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(model);
            await _repository.SaveChanges();
            return Created(model);
        }

        /// <inheritdoc/>
        [HttpGet]
        public virtual async Task<IActionResult> Get(ODataQueryOptions<TModel> options)
        {
            var items = await _repository.Find(options);
            return Ok(items);
        }

        /// <inheritdoc/>
        [HttpGet]
        [EnableQuery]
        /// <returns>El modelo encontrado.</returns>
        public virtual async Task<IActionResult> Get([FromODataUri] long key)
        {
            var model = await _repository.GetById(key);
            if (model == null) return NotFound();
            return Ok(model);
        }

        /// <inheritdoc/>
        [HttpPatch]
        /// <returns>El modelo actualizado.</returns>
        public virtual async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TModel> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _repository.GetById(key);
            if (model == null) return NotFound();

            delta.Patch(model);
            _repository.Update(model);
            await _repository.SaveChanges();

            return Ok(model);
        }

        /// <inheritdoc/>
        [HttpPut]
        /// <returns>El modelo actualizado.</returns>
        public virtual async Task<IActionResult> Put([FromODataUri] int key, [FromBody] TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelFound = await _repository.GetById(key);
            if (modelFound == null) return NotFound();
            _repository.Update(model);
            await _repository.SaveChanges();
            return Ok(model);
        }

        /// <inheritdoc/>
        [HttpDelete]
        public virtual async Task<IActionResult> Delete([FromODataUri] long key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entityFound = await _repository.Delete(key);
            if (!entityFound) return NotFound();
            await _repository.SaveChanges();
            return Ok();
        }

    }
}
