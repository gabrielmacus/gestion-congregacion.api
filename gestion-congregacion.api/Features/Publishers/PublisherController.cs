using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;

namespace gestion_congregacion.api.Features.Publishers
{
    public class PublisherController : CRUDController<Publisher, IPublisherRepository>
    {
        public PublisherController(PublisherRepository repository) : base(repository)
        {
        }
        
    }
}
