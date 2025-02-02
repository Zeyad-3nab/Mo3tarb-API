using Microsoft.EntityFrameworkCore;
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
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ApartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Apartment entity)       //Add Apartment
        {
            await _context.Apartments.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Apartment entity)    //Update Apartment
        {
            _context.Apartments.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Apartment entity)    //Delete Apartment
        {
            _context.Apartments.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Apartment>> GetAllAsync()
            => await _context.Apartments.ToListAsync();

        public async Task<Apartment> GetByIdAsync(int Id)
            => await _context.Apartments.FirstOrDefaultAsync(a => a.Id == Id);


        public async Task<IEnumerable<Apartment>> GetAllWithUserAsync(string Id)
            => await _context.Apartments.Where(e => e.UserId == Id).ToListAsync();


        public async Task<IEnumerable<Apartment>> Search(string? temp, double? MinPrice, double? MaxPrice, double? Distance)
        {
            var result = await _context.Apartments.ToListAsync();

            if (temp is not null)
            {
                result = result.Where(e => e.City.Contains(temp) || e.Village.Contains(temp)).ToList();
            }
            if (MinPrice != 0)
            {
                result = result.Where(e => e.Price >= MinPrice).ToList();
            }
            if (MaxPrice != 0)
            {
                result = result.Where(e => e.Price <= MaxPrice).ToList();
            }

            if (Distance != 0)
            {
                result = result.Where(e => e.DistanceByMeters <= Distance).ToList();
            }

            return result;
        }
    }
}
