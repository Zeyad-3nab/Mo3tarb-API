using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.RealTime
{
    public class ChatHub:Hub
    {
        private static ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();
        public override Task OnConnectedAsync() 
        {
            string userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                _connections[userId] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = _connections.FirstOrDefault(x => x.Value == Context.ConnectionId);
            if (!string.IsNullOrEmpty(user.Key))
            {
                _connections.TryRemove(user.Key, out _);
            }
            return base.OnDisconnectedAsync(exception);
        }



        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            if (_connections.TryGetValue(receiverId, out string receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", senderId, message);
            }
        }

    }
}
