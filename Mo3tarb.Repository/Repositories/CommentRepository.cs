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
        private readonly ApplicationDbContext _Context;

        public CommentRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<int> AddCommentAsync(Comment comment)
        {
            await _Context.Comments.AddAsync(comment);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> UpdateCommentAsync(Comment comment)
        {
            _Context.Comments.Update(comment);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> DeleteCommentAsync(Comment comment)
        {
             _Context.Comments.Remove(comment);
            return await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsForApartmentAsync(int apartmentId)
        {
            return await _Context.Comments.Where(e=>e.ApartmentId == apartmentId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _Context.Comments.Include(c => c.User).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}