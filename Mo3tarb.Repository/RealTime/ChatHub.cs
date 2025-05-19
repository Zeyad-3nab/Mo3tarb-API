using Microsoft.AspNetCore.SignalR;
using Mo3tarb.Core.Repositries;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.RealTime
{
    public class ChatHub:Hub
    {
        // Dictionary to track connected users by userId
        private static readonly ConcurrentDictionary<string, string> UserConnections = new();

        public async Task RegisterUser(string userId)
        {
            UserConnections[userId] = Context.ConnectionId;
            await Clients.All.SendAsync("userconnected");
        }

        public async Task SendMessageToUser(string senderId, string receiverId, string message)
        {
            if (UserConnections.TryGetValue(receiverId, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", senderId, receiverId, message, 0);
            }

            // Also send the message back to the sender in case of UI confirmation
            if (UserConnections.TryGetValue(senderId, out var senderConnectionId))
            {
                await Clients.Client(senderConnectionId).SendAsync("ReceiveMessage", senderId, receiverId, message, 0);
            }
        }

        public async Task MessageDeleted(string messageId, string receiverId)
        {
            if (UserConnections.TryGetValue(receiverId, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("MessageDeleted", messageId, receiverId);
            }

            // Optional: Notify the sender too
            var senderConnectionId = Context.ConnectionId;
            await Clients.Client(senderConnectionId).SendAsync("MessageDeleted", messageId, receiverId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(userId))
            {
                UserConnections.TryRemove(userId, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}





