using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mo3tarb.APIs.Controllers;
using Mo3tarb.APIs.Errors;
using Mo3tarb.APIs.PL.DTOs;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using System.Security.Claims;

namespace Mo3tarb.APIs.PL.Controllers
{
    public class RatingController : APIBaseController
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _Mapper;

        public RatingController(IRatingRepository ratingRepository, IApartmentRepository apartmentRepository , IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _apartmentRepository = apartmentRepository;
            _Mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddRating([FromBody] RatingDTO ratingDTO)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(ratingDTO.ApartmentId);
            if (apartment is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Apartment with this id is not found"));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID
            if (userId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid in userId please sure a sign in "));

            var rate = await _ratingRepository.getByIdAsync(userId, ratingDTO.ApartmentId);
            if(rate is not null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "User Add Rate to this apartment before"));

            var map = _Mapper.Map<Rating>(ratingDTO);
            map.UserId = userId;
            var count = await _ratingRepository.AddRatingAsync(map);
            if(count > 0) 
            return Ok(new { message = "Rating added successfully." });

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in add rating please try again"));

        }

        [HttpGet("{apartmentId}")]
        public async Task<ActionResult<double>> GetRatingofApartment(int apartmentId)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(apartmentId);
            if (apartment is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Apartment with this id is not found"));
            

            var ratings = await _ratingRepository.GetRatingsByApartmentIdAsync(apartmentId);

            double rate = ratings.Sum(e => e.Score);
            if(rate == 0)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "This Apartment don't have any rate"));

            rate/=ratings.Count();
            return Ok(rate);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteRating(int ApartmentId)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(ApartmentId);

            if (apartment is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Apartment with this id is not found"));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID
            if (userId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid in userId please sure a sign in "));


            var rate = await _ratingRepository.getByIdAsync(userId, ApartmentId);
            if(rate is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Rate with this user is not found"));


            var count = await _ratingRepository.DeleteRatingAsync(rate);
            if (count > 0) 
            return Ok(new { message = "Rating deleted successfully." });

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in delete rate please try again"));
        }

    }

}

