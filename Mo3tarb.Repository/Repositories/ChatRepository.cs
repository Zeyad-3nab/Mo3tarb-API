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

        public async Task<IEnumerable<AppUser>> GetContactedUserAsync(string userId)
        {
            //var contactedUser = await _context.ChatMessages
            //    .Where(m=>m.SenderId == UserId || m.ReceiverId == UserId)
            //    .Select(m=>m.SenderId==UserId ? m.Receiver : m.Sender)
            //    .Distinct()
            //    .ToListAsync();
            //return contactedUser;

            var userIds = await _context.ChatMessages
       .Where(m => m.SenderId == userId || m.ReceiverId == userId)
       .Select(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
       .Distinct()
       .ToListAsync();

            var contactedUsers = await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .AsNoTracking()
                .ToListAsync();

            return contactedUsers;
        }
    }
}