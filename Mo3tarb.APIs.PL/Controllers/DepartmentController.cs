using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mo3tarb.API.DTOs.DepartmentDTOs;
using Mo3tarb.APIs.Errors;
using Mo3tarb.APIs.Controllers;
using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Repositries;

namespace Mo3tarb.APIs.Controllers;

public class DepartmentController : APIBaseController
{
	//private readonly IMapper _mapper;
	//private readonly UserManager<AppUser> _userManager;
	//   private readonly IDepartmentRepository _departmentRepository;

	//   public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper, UserManager<AppUser> userManager )
	//{
	//	_mapper = mapper;
	//	_userManager = userManager;
	//       _departmentRepository = departmentRepository;
	//   }

	//[HttpGet]
	//public async Task<ActionResult> GetAll()
	//{
	//	var result = _mapper.Map<IReadOnlyList<DepartmentDTO>>(await _departmentRepository.GetAllAsync());
	//	return Ok(result);
	//}

	//[ProducesResponseType(typeof(DepartmentDTO), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
	////اللي راجع Respone بتكزن تحسين لشكل ال ProducesResponseType ال 2 حاجات بتاعت ال
	//[HttpGet("{id:int}")]
	//public async Task<ActionResult> GetById(int id)
	//{
	//	var department = await _departmentRepository.GetByIdAsync(id);
	//	if (department == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

	//	var result = _mapper.Map<DepartmentDTO>(department);
	//	return Ok(result);
	//}



	//[HttpGet("search/{searchInput}")]
	//public async Task<ActionResult> Search(string searchInput)
	//{
	//	var result = _mapper.Map<IReadOnlyList<DepartmentDTO>>(await _departmentRepository.Search(searchInput));
	//	return Ok(result);
	//}



	//[ProducesResponseType(typeof(DepartmentDTO), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
	//[HttpPost]
	//public async Task<ActionResult> Add([FromBody] DepartmentDTO departmentDto)
	//{
	//	if (!ModelState.IsValid) return BadRequest(new ApiErrorResponse(404));

	//	var department = _mapper.Map<Department>(departmentDto);
	//	var count = await _departmentRepository.AddAsync(department);

	//	if (count > 0)
	//	return Ok(departmentDto);

	//	return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
	//}

	//[ProducesResponseType(typeof(DepartmentDTO), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
	//[HttpPut]
	//public async Task<ActionResult> Update([FromBody] DepartmentDTO departmentDto)
	//{
	//	if (!ModelState.IsValid) return BadRequest(new ApiErrorResponse(404));

	//	var department = _mapper.Map<Department>(departmentDto);
	//	var count = await _departmentRepository.UpdateAsync(department);

	//	if (count > 0) return Ok(departmentDto);

	//	return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
	//}

	//[ProducesResponseType(typeof(DepartmentDTO), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
	//[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
	//[HttpDelete("{id:int}")]
	//public async Task<ActionResult> Delete(int id)
	//{
	//	var department = await _departmentRepository.GetByIdAsync(id);
	//	if (department == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

	//	var count = await _departmentRepository.DeleteAsync(department);
	//	if (count > 0)
	//		return Ok("Department deleted successfully.");
	//	return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
	//}
	///////////////////////////////////////////////////////////
	private readonly IMapper _mapper;
	private readonly IDepartmentRepository _departmentRepository;

	public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
	{
		_mapper = mapper;
		_departmentRepository = departmentRepository;
	}

	[HttpGet]
	public async Task<ActionResult> GetAll()
	{
		var departments = await _departmentRepository.GetAllAsync();
		var result = _mapper.Map<IReadOnlyList<DepartmentDTO>>(departments);
		return Ok(result);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult> GetById(int id)
	{
		var department = await _departmentRepository.GetByIdAsync(id);
		if (department == null)
			return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

		var result = _mapper.Map<DepartmentDTO>(department);
		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult> Add([FromBody] DepartmentDTO departmentDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

		var department = _mapper.Map<Department>(departmentDto);
		var count = await _departmentRepository.AddAsync(department);

		if (count > 0)
			return Ok(departmentDto);

		return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
	}

	[HttpPut]
	public async Task<ActionResult> Update([FromBody] DepartmentDTO departmentDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

		var department = _mapper.Map<Department>(departmentDto);
		var count = await _departmentRepository.UpdateAsync(department);

		if (count > 0)
			return Ok(departmentDto);

		return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult> Delete(int id)
	{
		var department = await _departmentRepository.GetByIdAsync(id);
		if (department == null)
			return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

		var count = await _departmentRepository.DeleteAsync(department);
		if (count > 0)
			return Ok("Department deleted successfully.");

		return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
	}

}
