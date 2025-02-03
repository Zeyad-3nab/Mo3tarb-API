using Mo3tarb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Core.Repositries
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllReports();
        Task<IEnumerable<Report>> GetAllReportsWithUser(string userId);
        Task<Report> GetReportById(int reportId);
        Task<int> AddReport(Report report);
        Task<int> RemoveReport(Report report);
    }
}
