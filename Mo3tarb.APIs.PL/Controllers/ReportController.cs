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
        private readonly UserManager<AppUser> _UserManager;
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(UserManager<AppUser> userManager , IUnitOfWork unitOfWork)
        {
            _UserManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetAllReports")]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllReports() 
        {
            var result =  await _unitOfWork.reportRepository.GetAllReports();
            return Ok(result);
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsOfUser(string userId) 
        {
            var user = await _UserManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));

            var result = await _unitOfWork.reportRepository.GetAllReportsWithUser(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetMyReports")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsOfUser() 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOfWork.reportRepository.GetAllReportsWithUser(userId);
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
            var count = await _unitOfWork.reportRepository.AddReport(report);
            if (count > 0) 
                return Ok();
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Save Report , please try again"));
        }



        [Authorize(Roles ="Admin")]
        [HttpDelete("RemoveReport")]
        public async Task<ActionResult> RemoveReport(int reportId)
        {
            var report = await _unitOfWork.reportRepository.GetReportById(reportId);
            if(report is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Report with this Id is not found"));

            var count = await _unitOfWork.reportRepository.RemoveReport(report);
            if (count > 0)
                return Ok();
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Remove Report , please try again"));
        }
    }
}
