using AutoMapper;
using gestion_congregacion.api.Features.Common;
namespace gestion_congregacion.api.Features.Publishers
{
    public class PublisherRepository : BaseRepository<Publisher, AppDbContext>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext context) : base(context)
        {
        }

    }
}
