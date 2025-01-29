using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Data;
using Mo3tarb.Repository.Identity;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppIdentityDbContext _context;

        public DepartmentRepository(AppIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Department entity)
        {
            await _context.Departments.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(Department entity)
        {
            _context.Departments.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Department entity)
        {
            _context.Departments.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
            => await _context.Departments.ToListAsync();

        public async Task<Department> GetByIdAsync(int Id)
            => await _context.Departments.FindAsync(Id);

        public async Task<IEnumerable<Department>> Search(string Name)
            => await _context.Departments.Where(d=>d.Name == Name).ToListAsync();
    }
}
