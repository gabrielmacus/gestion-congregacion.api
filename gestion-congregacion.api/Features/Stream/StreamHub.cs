using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace gestion_congregacion.api.Features.Stream
{
    public class StreamHub : Hub
    {
        const string VIEWERS_HASH_KEY = "viewers";

        private readonly IConnectionMultiplexer _redis;
        public StreamHub(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task SendViewers()
        {
            var viewers = await _redis.GetDatabase().HashGetAllAsync(VIEWERS_HASH_KEY);
            var viewersList = viewers.Select(x =>
            {
                var split = x.Value.ToString().Split(":");
                return new Dictionary<string, dynamic>() {
                    ["name"] = split[0],
                    ["participants"] = int.Parse(split[1])
                };
            });
            await Clients.All.SendAsync("Viewers", viewersList);

        }

        public override async Task OnConnectedAsync()
        {
            var name = Context?.GetHttpContext()?.GetRouteValue("Name")?.ToString();
            var participants = Context?.GetHttpContext()?.GetRouteValue("Participants")?.ToString();

            await _redis.GetDatabase().HashSetAsync(VIEWERS_HASH_KEY, Context?.ConnectionId, $"{name}:{participants}");
            await SendViewers();

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _redis.GetDatabase().HashDeleteAsync(VIEWERS_HASH_KEY, Context?.ConnectionId);
            await SendViewers();
            await base.OnDisconnectedAsync(exception);
        }
    }
}

