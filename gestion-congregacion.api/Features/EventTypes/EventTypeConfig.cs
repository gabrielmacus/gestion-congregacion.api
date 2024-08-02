using gestion_congregacion.api.Features.EventTypes;
using gestion_congregacion.api.Features.Operations;
using Microsoft.OData.ModelBuilder;

public static partial class ServicesConfiguration
{
    public static void AddEventTypesFeature(this IServiceCollection services, ODataConventionModelBuilder modelBuilder)
    {
        services.AddScoped<ICreateOperation<EventType>, CreateOperation<EventType, IEventTypeRepository>>();
        services.AddScoped<IReadOperation<EventType>, ReadOperation<EventType, EventType, IEventTypeRepository>>();
        services.AddScoped<IUpdateOperation<EventType>, UpdateOperation<EventType, IEventTypeRepository>>();
        services.AddScoped<IDeleteOperation<EventType>, DeleteOperation<EventType, IEventTypeRepository>>();

        services.AddScoped<IEventTypeRepository, EventTypeRepository>();

        modelBuilder.EntitySet<EventType>("EventType");



    }

}