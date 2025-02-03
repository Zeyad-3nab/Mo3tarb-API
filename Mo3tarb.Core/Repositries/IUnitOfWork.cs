using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
    public interface IUnitOfWork
    {
        public IApartmentRepository apartmentRepository { get; }
        public ICommentRepository commentRepository { get; }
        public IDepartmentRepository departmentRepository { get; }
        public IFavouriteRepository favouriteRepository { get; }
        public IRatingRepository ratingRepository { get; }
        public IReportRepository reportRepository { get; }
    }
}
