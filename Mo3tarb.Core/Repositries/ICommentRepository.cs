using Mo3tarb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetAllCommentsForApartmentAsync(int apartmentId);
        public Task<Comment> GetByIdAsync(int id);
        public Task<int> AddCommentAsync(Comment comment);
        public Task<int> UpdateCommentAsync(Comment comment);
        public Task<int> DeleteCommentAsync(Comment comment);

    }
}
