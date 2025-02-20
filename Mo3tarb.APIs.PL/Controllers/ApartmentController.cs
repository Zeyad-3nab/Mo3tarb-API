﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Core.Models;
using Mo3tarb.APIs.Controllers;
using Mo3tarb.APIs.PL.DTOs;
using AutoMapper;
using Mo3tarb.APIs.PL.Helper;
using Microsoft.AspNetCore.Identity;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.APIs.Errors;
using Mo3tarb.APIs.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Mo3tarb.API.DTOs.DepartmentDTOs;
using Mo3tarb.Core.Entites;

namespace Mo3tarb.APIs.PL.Controllers
{
    public class ApartmentController : APIBaseController
    {
        private readonly IMapper _Mapper;
        private readonly UserManager<AppUser> _UserManager;
        private readonly IUnitOfWork _unitOfWork;

        public ApartmentController( IMapper mapper , UserManager<AppUser> userManager , IUnitOfWork unitOfWork)
        {
            _Mapper = mapper;
            _UserManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnApartmentDTO>>> GetAllApartments() 
        {
            var Apartments = await _unitOfWork.apartmentRepository.GetAllAsync();
            var map = _Mapper.Map<IEnumerable<ReturnApartmentDTO>>(Apartments);
                return Ok(map);
        }

        [Authorize]
        [HttpGet("GetApartmentForSignInUser")]
        public async Task<ActionResult<IEnumerable<ReturnApartmentDTO>>> GetAllApartmentWithUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Apartments = await _unitOfWork.apartmentRepository.GetAllWithUserAsync(userId);
            var map = _Mapper.Map<IEnumerable<ReturnApartmentDTO>>(Apartments);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetApartmentForUser")]
        public async Task<ActionResult<IEnumerable<ReturnApartmentDTO>>> GetAllApartmentWithUser(string UserId)
        {
            var Apartments = await _unitOfWork.apartmentRepository.GetAllWithUserAsync(UserId);
            var map = _Mapper.Map<IEnumerable<ReturnApartmentDTO>>(Apartments);
            return Ok(map);
        }


        [AllowAnonymous]
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<ReturnApartmentDTO>>> SearchOfApartment(string? temp, double? MinPrice, double? MaxPrice, double? Distance)
        {
            var apartments = await _unitOfWork.apartmentRepository.Search(temp, MinPrice, MaxPrice, Distance);
            var map = _Mapper.Map<IEnumerable<ReturnApartmentDTO>>(apartments);
            return Ok(map);
        }

        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<ActionResult<ReturnApartmentDTO>> GetApartmentById(int Id) 
        {
            var Apartment= await _unitOfWork.apartmentRepository.GetByIdAsync(Id);
            var map = _Mapper.Map<ReturnApartmentDTO>(Apartment);

            return Ok(map);
        }


        [Authorize(Roles ="Semsar,Admin")]
        [HttpPost]
        public async Task<ActionResult> Add(ApartmentDTO apartmentDTO) 
        {
            if (ModelState.IsValid) 
            {

                var apartment = new Apartment()   //    Can't use auto mapper because noa all data in apartment in apartmentDTO Like(Distance , Image)
                {
                    City = apartmentDTO.City,
                    Village= apartmentDTO.Village,
                    Location= apartmentDTO.Location,
                    Price= apartmentDTO.Price,
                    NumOfRooms= apartmentDTO.NumOfRooms,
                    Type=apartmentDTO.Type,
                    IsRent= apartmentDTO.IsRent
                };
                if (apartmentDTO.BaseImage is not null)
                {
                    apartment.BaseImageURL = DocumentSettings.Upload(apartmentDTO.BaseImage, "Images");   //add image of apartment in wwwroot
                }
                apartment.DistanceByMeters=CalcDistance.CalculateDistance(apartmentDTO.address_Lat,apartmentDTO.address_Lon);

                apartment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                foreach (var item in apartmentDTO.Images) 
                {
                    apartment.ImagesURL.Add(DocumentSettings.Upload(item , "Images"));
                }

                var count = await _unitOfWork.apartmentRepository.AddAsync(apartment);
                if (count > 0) 
                {
                    return Ok();
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Save Apartment"));
            }
            return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                , "a bad Request , You have made"
                , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateApartment(UpdateApartmentDTO apartmentDTO) 
        {
            if (ModelState.IsValid)
            {
                var apartment = await _unitOfWork.apartmentRepository.GetByIdAsync(apartmentDTO.Id);
                if (apartment is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Apartment with this Id is not found"));

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (apartment.UserId == userId || User.IsInRole("Admin"))
                {

                    if (apartmentDTO.BaseImage is not null) 
                    {
                        DocumentSettings.Delete(apartment.BaseImageURL, "Images");
                        apartment.BaseImageURL = DocumentSettings.Upload(apartmentDTO.BaseImage, "Images");   //Save image && return name

                    }
                    if(apartmentDTO.Location != apartment.Location) 
                       apartment.DistanceByMeters = CalcDistance.CalculateDistance(apartmentDTO.address_Lat, apartmentDTO.address_Lon);

                    apartment.City = apartmentDTO.City;
                    apartment.Village = apartmentDTO.Village;
                    apartment.Location = apartmentDTO.Location;
                    apartment.Price = apartmentDTO.Price;
                    apartment.NumOfRooms = apartmentDTO.NumOfRooms;
                    apartment.Type = apartmentDTO.Type;
                    apartment.IsRent = apartmentDTO.IsRent;

                    var count = await _unitOfWork.apartmentRepository.UpdateAsync(apartment);
                    if (count > 0)
                    {
                        return Ok();
                    }
                    return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Save Apartment"));
                }

                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Don't have an access to update this apartment"));
            }
            return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                , "a bad Request , You have made"
                , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id) 
        {
            var apartment = await _unitOfWork.apartmentRepository.GetByIdAsync(id);
            if (apartment is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Apartment with this Id is not found"));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(apartment.UserId == userId || User.IsInRole("Admin"))
            {
                var count = await _unitOfWork.apartmentRepository.DeleteAsync(apartment);
                if(count > 0) 
                {
                    DocumentSettings.Delete(apartment.BaseImageURL, "Images");
                    foreach (var item in apartment.ImagesURL)
                    {
                        DocumentSettings.Delete(item, "Images");
                    }

                    return Ok();
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Save Comment"));
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Don't have an access to remove this apartment"));

        }


       
    }
}
