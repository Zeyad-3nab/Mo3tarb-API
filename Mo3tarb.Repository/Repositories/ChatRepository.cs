using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Identity;
using Mo3tarb.Repository.RealTime;

namespace Mo3tarb.Repository.Repositories
{
    public class ChatRepository:IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
        }
        public async Task<int> SendMessageAsync(ChatMessage chatMessage)
        {
            await _context.ChatMessages.AddAsync(chatMessage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ChatMessage>> GetChatHistoryAsync(string receiverId, string senderId)
            => await _context.ChatMessages.Include(u=>u.Sender).Include(u=>u.Receiver).Where(e => (e.SenderId == senderId && e.ReceiverId == receiverId) ||
                                                      (e.SenderId == receiverId && e.ReceiverId == senderId))
                                                      .OrderBy(t=>t.Timestamp)
                                                      .ToListAsync();

        public async Task<int> DeleteAsync(ChatMessage chatMessage)
        {
            _context.ChatMessages.Remove(chatMessage);
            return await _context.SaveChangesAsync();
        }

        public async Task<ChatMessage> GetMessageAsync(int MessageId)
            => await _context.ChatMessages.FindAsync(MessageId);

        public async Task<int> GetUnreadMessagesCountAsync(string receiverId)
        {
            return await _context.ChatMessages
                .Where(m => m.ReceiverId == receiverId && !m.IsRead)
                .CountAsync();
        }

        public async Task<IEnumerable<(AppUser User, int UnreadCount)>> GetContactedUsersWithUnreadCountAsync(string userId)
        {
            var userIds = await _context.ChatMessages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .Select(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Distinct()
                .ToListAsync();

            var unreadCounts = await _context.ChatMessages
                .Where(m => m.ReceiverId == userId && !m.IsRead)
                .GroupBy(m => m.SenderId)
                .Select(g => new { SenderId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.SenderId, g => g.Count);

            var users = await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .AsNoTracking()
                .ToListAsync();

            var result = users.Select(u => (
                User: u,
                UnreadCount: unreadCounts.ContainsKey(u.Id) ? unreadCounts[u.Id] : 0
            ));

            return result;
        }

        public async Task<int> DeleteAll(string UserId)
        {
            var ChatMessages = await _context.ChatMessages.Where(e=>e.SenderId == UserId ||e.ReceiverId ==UserId).ToListAsync();
            _context.ChatMessages.RemoveRange(ChatMessages);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> MarkMessagesAsReadAsync(string senderId, string receiverId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.ReceiverId == senderId && !m.IsRead)
                .ToListAsync();

            foreach (var msg in messages)
                msg.IsRead = true;

            return await _context.SaveChangesAsync();
        }
    }
}