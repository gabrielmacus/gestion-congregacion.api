using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;

namespace gestion_congregacion.api.Features.Operations
{
    public interface IUpdateOperation<T> : IDisposable
        where T : class, IBaseModel, new()
    {
        Task<IActionResult> Put(long key, T model);
        Task<IActionResult> Patch(long key, Delta<T> model);

    }
}
