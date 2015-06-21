using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Hubs
{
    public class NotifyHub : Hub
    {
        public static ConcurrentDictionary<string, string> ConnectionIdList { get; set; }

        static NotifyHub()
        {
            ConnectionIdList = new ConcurrentDictionary<string, string>();
        }

        public override Task OnConnected()
        {
            string userId = Context.QueryString["userid"];
            ConnectionIdList.TryAdd(Context.ConnectionId, userId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string value;
            ConnectionIdList.TryRemove(Context.ConnectionId, out value);

            return base.OnDisconnected(stopCalled);
        }
    }
}
