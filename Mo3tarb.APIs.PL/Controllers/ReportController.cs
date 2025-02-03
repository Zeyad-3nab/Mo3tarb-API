using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mo3tarb.APIs.Controllers;
using Mo3tarb.APIs.Errors;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using System.Security.Claims;

namespace Mo3tarb.APIs.PL.Controllers
{
    [Authorize]
    public class ReportController :APIBaseController
    {
        private readonly IReportRepository _ReportRepository;
        private readonly UserManager<AppUser> _UserManager;

        public ReportController(IReportRepository reportRepository , UserManager<AppUser> userManager)
        {
            _ReportRepository = reportRepository;
            _UserManager = userManager;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetAllReports")]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllReports() 
        {
            var result =  await _ReportRepository.GetAllReports();
            return Ok(result);
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsOfUser(string userId) 
        {
            var user = await _UserManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));

            var result = await _ReportRepository.GetAllReportsWithUser(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetMyReports")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsOfUser() 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _ReportRepository.GetAllReportsWithUser(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddReport(string reportText) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var report = new Report() 
            {
                Text = reportText,
                UserId = userId
            };
            var count = await _ReportRepository.AddReport(report);
            if (count > 0) 
                return Ok();
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Save Report , please try again"));
        }



        [Authorize(Roles ="Admin")]
        [HttpDelete("RemoveReport")]
        public async Task<ActionResult> RemoveReport(int reportId)
        {
            var report = await _ReportRepository.GetReportById(reportId);
            if(report is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Report with this Id is not found"));

            var count = await _ReportRepository.RemoveReport(report);
            if (count > 0)
                return Ok();
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Remove Report , please try again"));
        }
    }
}
