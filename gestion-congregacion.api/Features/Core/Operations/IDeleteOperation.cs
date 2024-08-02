using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;

namespace gestion_congregacion.api.Features.Operations
{
    public interface IDeleteOperation<T> : IDisposable
        where T : class, ISoftDeleteModel, new()
    {
        Task<IActionResult> Delete(long key);
    }
}
