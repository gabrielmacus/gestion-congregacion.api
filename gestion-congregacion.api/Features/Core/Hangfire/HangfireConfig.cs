
using gestion_congregacion.api.Features.Core.Hangfire;
using Hangfire;
using Hangfire.MySql;

public static partial class ServicesConfiguration
{
    public static void AddHangfire(
        this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(
        new MySqlStorage(
                        configuration.GetConnectionString("HangfireDb"),
                    new MySqlStorageOptions { }))
                .UseFilter(new AutomaticRetryAttribute { Attempts = 0 })

            );
        services.AddHangfireServer();
    }

    public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = [new HangfireAuthorizationFilter()]
        });

        return app;
    }
}