using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
    public interface IApartmentRepository:IGenaricRepository<Apartment>
    {
        public Task<IEnumerable<Apartment>> GetAllWithUserAsync(string Id);

        public Task<IEnumerable<Apartment>> Search(string? temp, double? MinPrice, double? MaxPrice, double? Distance);
    }
}
