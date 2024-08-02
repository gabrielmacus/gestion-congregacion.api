using gestion_congregacion.api.Features.Operations;

namespace gestion_congregacion.api.Features.Common
{
    public interface ICRUDController <T>
        : IReadOperation<T>, ICreateOperation<T>, IUpdateOperation<T>, IDeleteOperation<T>, IDisposable
        where T : class, IBaseModel, new()
    {
    }
}
