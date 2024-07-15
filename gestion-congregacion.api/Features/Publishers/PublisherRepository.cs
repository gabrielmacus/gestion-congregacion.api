using gestion_congregacion.api.Features.Common;

namespace gestion_congregacion.api.Features.Publishers
{
    public class PublisherRepository : BaseDbRepository<Publisher, AppDbContext>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext context) : base(context)
        {
        }

    }
}
