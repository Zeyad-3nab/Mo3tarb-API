using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
    public interface IChatRepository
    {
        Task<List<ChatMessage>> GetChatHistoryAsync(string receiverId, string senderId);
        Task<ChatMessage> GetMessageAsync(int MessageId);
        Task<int> SendMessageAsync(ChatMessage chatMessage);
        Task<int> GetUnreadMessagesCountAsync(string receiverId);
        Task<int> DeleteAsync(ChatMessage chatMessage);
        Task<IEnumerable<(AppUser User, int UnreadCount)>> GetContactedUsersWithUnreadCountAsync(string userId);
        public Task<int> DeleteAll(string UserId);
        Task<int> MarkMessagesAsReadAsync(string senderId, string receiverId);
    }
}
