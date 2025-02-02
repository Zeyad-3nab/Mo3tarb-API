using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Models;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _Context;

        public RatingRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<IReadOnlyList<Rating>> GetRatingsByApartmentIdAsync(int apartmentId)
        {
            return await _Context.Ratings.Where(r => r.ApartmentId == apartmentId).ToListAsync();
        }
        public async Task<Rating> getByIdAsync(string UserId , int ApartmentId)
        {
            return await _Context.Ratings.Where(r => r.UserId == UserId && r.ApartmentId == ApartmentId).FirstOrDefaultAsync();
        }

        public async Task<int> AddRatingAsync(Rating rating)
        {
            await _Context.Ratings.AddAsync(rating);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> DeleteRatingAsync(Rating rating)
        {
            _Context.Ratings.Remove(rating);
            return await _Context.SaveChangesAsync();
        }
    }
}
