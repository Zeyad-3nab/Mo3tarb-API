using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Identity;
using Mo3tarb.Repository.RealTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Repositories
{
    public class ChatRepository:IChatRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatRepository(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        public async Task<int> SendMessageAsync(ChatMessage chatMessage)
        {
            await _context.ChatMessages.AddAsync(chatMessage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ChatMessage>> GetChatHistoryAsync(string receiverId, string senderId)
            => await _context.ChatMessages.Where(e => (e.SenderId == senderId && e.ReceiverId == receiverId) ||
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

        public async Task<IEnumerable<AppUser>> GetContactedUserAsync(string UserId)
        {
            var contactedUser = await _context.ChatMessages
                .Where(m=>m.SenderId == UserId || m.ReceiverId == UserId)
                .Select(m=>m.SenderId==UserId ? m.Receiver : m.Sender)
                .Distinct()
                .ToListAsync();
            return contactedUser;
        }
    }
}