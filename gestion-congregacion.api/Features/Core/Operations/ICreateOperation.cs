using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;

namespace gestion_congregacion.api.Features.Operations
{
    public interface ICreateOperation<T> : IDisposable
        where T : class, IBaseModel, new()
    {
        Task<IActionResult> Post(T model);

    }
}
