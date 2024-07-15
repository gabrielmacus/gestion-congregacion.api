using gestion_congregacion.api.Features.Events;
using gestion_congregacion.api.Features.Publishers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace gestion_congregacion.api.Features.Common
{
    public class AppModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Publisher>("Publisher");
            modelBuilder.EntitySet<Event>("Event");

            return modelBuilder.GetEdmModel();

        }
    }
}
