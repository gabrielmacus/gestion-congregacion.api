
using gestion_congregacion.api.Features.FetchMeetingEvents;
using Hangfire;

public static partial class ServicesConfiguration
{
    public static void AddFetchMeetingEventsFeature(
        this IServiceCollection services)
    {
        
        services.AddScoped<IFetchMeetingEventsServices, FetchMeetingEventsServices>();
    }

    public static IApplicationBuilder UseFetchMeetingEventsFeature(this IApplicationBuilder app)
    {
        RecurringJob.AddOrUpdate<IFetchMeetingEventsServices>("1", x => x.SaveEventsFromJW(), "0 0 * * *");
        return app;
    }

}