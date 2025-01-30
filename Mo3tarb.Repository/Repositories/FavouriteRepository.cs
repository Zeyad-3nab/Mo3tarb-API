using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Repositories
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly ApplicationDbContext _Context;

        public FavouriteRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<int> AddAsync(Favourite fav)
        {
            await _Context.Favourites.AddAsync(fav);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> DeleteFavouritesAsync(Favourite fav)
        {
            _Context.Favourites.Remove(fav);
            return await _Context.SaveChangesAsync();
        }

        public async Task<Favourite> GetFavouritesAsync(string userId, int ApartmentId)
        {
            return await _Context.Favourites
                            .Where(F => F.UserId == userId && F.apartmentId == ApartmentId)
                            .FirstOrDefaultAsync();

        }

        public async Task<IReadOnlyList<Favourite>> GetFavouritesByUserIdAsync(string userId)
        {
            return await _Context.Favourites
                             .Where(F => F.UserId == userId)
                             .Include(P => P.apartment)
                             .ToListAsync();

        }
    }
}
