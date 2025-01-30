using Mo3tarb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
    public interface IFavouriteRepository
    {
        Task<int> AddAsync(Favourite fav);
        Task<IReadOnlyList<Favourite>> GetFavouritesByUserIdAsync(string userId);
        Task<Favourite> GetFavouritesAsync(string userId, int ApartmentId);
        Task<int> DeleteFavouritesAsync(Favourite FavouriteId);
    }
}
