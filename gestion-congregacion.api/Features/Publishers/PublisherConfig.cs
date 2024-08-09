

using gestion_congregacion.api.Features.Operations;
using gestion_congregacion.api.Features.Publishers;
using Microsoft.OData.ModelBuilder;

public static partial class ServicesConfiguration
{
    public static void AddPublishersFeature(
        this IServiceCollection services, 
        ODataConventionModelBuilder modelBuilder)
    {
        services.AddScoped<ICreateOperation<Publisher>, CreateOperation<Publisher, IPublisherRepository>>();
        services.AddScoped<IReadOperation<Publisher>, ReadOperation<Publisher, Publisher, IPublisherRepository>>();
        services.AddScoped<IUpdateOperation<Publisher>, UpdateOperation<Publisher, IPublisherRepository>>();
        services.AddScoped<IDeleteOperation<Publisher>, DeleteOperation<Publisher, IPublisherRepository>>();

        services.AddScoped<PublisherRepository>();

        services.AddScoped<IPublisherRepository, PublisherRepository>();

        modelBuilder.EntitySet<Publisher>("Publisher");

    }
}