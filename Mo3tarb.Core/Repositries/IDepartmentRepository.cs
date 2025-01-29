using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Repositries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
	public interface IDepartmentRepository : IGenaricRepository<Department>
	{
		public Task<IEnumerable<Department>> Search(string Name); 
	}
}
