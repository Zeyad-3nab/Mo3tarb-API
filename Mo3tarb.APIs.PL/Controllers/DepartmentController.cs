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
	private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

	public DepartmentController(IMapper mapper , IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
        _unitOfWork = unitOfWork;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAll()
	{
		var departments = await _unitOfWork.departmentRepository.GetAllAsync();
		var result = _mapper.Map<IReadOnlyList<DepartmentDTO>>(departments);
		return Ok(result);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<DepartmentDTO>> GetById(int id)
	{
		var department = await _unitOfWork.departmentRepository.GetByIdAsync(id);
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
		var count = await _unitOfWork.departmentRepository.AddAsync(department);

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
		var count = await _unitOfWork.departmentRepository.UpdateAsync(department);

		if (count > 0)
			return Ok(departmentDto);

		return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult> Delete(int id)
	{
		var department = await _unitOfWork.departmentRepository.GetByIdAsync(id);
		if (department == null)
			return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

		var count = await _unitOfWork.departmentRepository.DeleteAsync(department);
		if (count > 0)
			return Ok("Department deleted successfully.");

		return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
	}

}
