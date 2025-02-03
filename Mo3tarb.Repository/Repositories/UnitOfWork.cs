using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        private IApartmentRepository _apartmentRepository;
        private ICommentRepository _commentRepository;
        private IDepartmentRepository _departmentRepository;
        private IFavouriteRepository _favouriteRepository;
        private IRatingRepository _ratingRepository;
        private IReportRepository _reportRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _Context = context;
            _apartmentRepository = new ApartmentRepository(_Context);
            _commentRepository = new CommentRepository(_Context);
            _departmentRepository = new DepartmentRepository(_Context);
            _favouriteRepository = new FavouriteRepository(_Context);
            _ratingRepository = new RatingRepository(_Context);
            _reportRepository = new ReportRepository(_Context);
        }

        public IApartmentRepository apartmentRepository => _apartmentRepository;

        public ICommentRepository commentRepository => _commentRepository;

        public IDepartmentRepository departmentRepository => _departmentRepository;

        public IFavouriteRepository favouriteRepository => _favouriteRepository;

        public IRatingRepository ratingRepository => _ratingRepository;

        public IReportRepository reportRepository => _reportRepository;
    }
}
