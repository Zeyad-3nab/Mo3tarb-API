using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Models;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Data;
using Mo3tarb.Repository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteCommentAsync(Comment comment)
        {
             _context.Comments.Remove(comment);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsForApartmentAsync(int apartmentId)
        {
            return await _context.Comments.Where(e=>e.ApartmentId == apartmentId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> DeleteAll(string UserId)
        {
            var comments = await _context.Comments.Where(e=>e.UserId == UserId).ToListAsync();
            _context.Comments.RemoveRange(comments);
            return await _context.SaveChangesAsync();
        }

    }
}