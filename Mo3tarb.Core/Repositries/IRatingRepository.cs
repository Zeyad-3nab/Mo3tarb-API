using Mo3tarb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
    public interface IRatingRepository
    {
        Task<int> AddRatingAsync(Rating rating);
        Task<IReadOnlyList<Rating>> GetRatingsByApartmentIdAsync(int apartmentId);
        Task<int> DeleteRatingAsync(Rating rating);
        Task<Rating> getByIdAsync(string UserId, int ApartmentId);
    }
}
