#if DNX451

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IotWebApp.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace IotWebApp.Events
{
    public class Processor : IEventProcessor
    {
        private PartitionContext partitionContext;

        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            if (reason == CloseReason.Shutdown)
            {
                await context.CheckpointAsync();
            }
        }

        public Task OpenAsync(PartitionContext context)
        {
            this.partitionContext = context;
            return Task.FromResult<object>(null);
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            if (messages != null)
            {
                IConnectionManager connectionManager = (IConnectionManager)Startup.ServiceProvider.GetService(typeof(IConnectionManager));
                IHubContext hubContext = connectionManager.GetHubContext<RealTimeHub>();
                foreach (EventData @event in messages)
                {
                    string json = Encoding.UTF8.GetString(@event.GetBytes());
                    hubContext.Clients.All.status(json);
                }
                await context.CheckpointAsync();
            }
        }
    }
}

#endif
