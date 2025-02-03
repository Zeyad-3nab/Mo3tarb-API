using Microsoft.EntityFrameworkCore;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo3tarb.Repository.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _Context;

        public ReportRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<int> AddReport(Report report)
        {
            await _Context.Reports.AddAsync(report);
            return await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _Context.Reports.Include(r=>r.User).ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetAllReportsWithUser(string userId)
        {
            return await _Context.Reports.Where(r=>r.UserId==userId).ToListAsync();
        }

        public async Task<Report> GetReportById(int reportId)
        {
            return await _Context.Reports.FindAsync(reportId);
        }

        public Task<int> RemoveReport(Report report)
        {
            _Context.Reports.Remove(report);
            return _Context.SaveChangesAsync();
        }
    }
}
