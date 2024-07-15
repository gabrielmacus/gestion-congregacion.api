using Microsoft.AspNetCore.OData.Query;

namespace gestion_congregacion.api.Features.Common
{
    public interface IBaseDbRepository<TModel> : IDisposable
        where TModel : BaseDbModel
    {

        Task<IEnumerable<TModel>> GetAll();
        Task<IEnumerable<TModel>> Find(ODataQueryOptions<TModel> options);
        Task<TModel?> GetById(long id);

        TModel Add(TModel entity);
        void Update(TModel entity);
        Task<bool> Delete(long id);
        Task SaveChanges();

    }
}
