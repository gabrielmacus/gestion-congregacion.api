using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;

namespace gestion_congregacion.api.Features.Operations
{
    public interface IReadOperation<T> : IDisposable
        where T : class, IBaseModel, new()
    {
        Task<IActionResult> Get(ODataQueryOptions<T> options, ODataValidationSettings queryValidationSettings);
        Task<IActionResult> Get(long key, ODataQueryOptions<T> options);
    }

}
