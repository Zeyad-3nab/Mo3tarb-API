using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mo3tarb.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Data.Configurations
{
	public class DepartmentController: IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
     //    builder.HasMany(d => d.ApplicationUsers)
					//.WithOne(u => u.Department)
					//.HasForeignKey(u => u.DepartmentId);

		}
	}
}
