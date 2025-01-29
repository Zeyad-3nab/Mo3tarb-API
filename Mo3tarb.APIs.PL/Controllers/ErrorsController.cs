using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mo3tarb.APIs.Errors;

namespace GraduationProject.API.PL.Controllers
{
	[Route("error/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)] //ده Controller تجاهل ال swagger هنا بقول ل
	public class ErrorsController : ControllerBase
	{
		//مش موجوده endpoint انادي عليها في حاله اني نديت علي endpoint هعمل 
		public IActionResult Error(int code)
		{
			return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found End Point !!"));
		}
	}
}
