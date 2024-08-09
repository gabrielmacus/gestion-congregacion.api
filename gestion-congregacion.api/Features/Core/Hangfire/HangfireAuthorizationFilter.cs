using Hangfire.Dashboard;

namespace gestion_congregacion.api.Features.Core.Hangfire
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            #if DEBUG
                return true;
            #endif

            return false;
        }
    }
}
