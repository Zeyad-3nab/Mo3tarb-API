using Mo3tarb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
    public interface IGenaricRepository<T> where T : BaseEntitiy
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
		Task<int> AddAsync(T entity);
		Task<int> UpdateAsync(T entity);
		Task<int> DeleteAsync(T entity);
	}
}
