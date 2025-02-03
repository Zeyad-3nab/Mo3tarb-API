using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mo3tarb.APIs.Controllers;
using Mo3tarb.APIs.Errors;
using Mo3tarb.APIs.PL.DTOs;
using Mo3tarb.APIs.PL.Errors;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using System.Security.Claims;

namespace Mo3tarb.APIs.PL.Controllers
{
    public class FavouriteController : APIBaseController
    {
        private readonly IMapper _Mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FavouriteController(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _Mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favourite>>> GetUserFavorites(string userId)
        {
            var favorites = await _unitOfWork.favouriteRepository.GetFavouritesByUserIdAsync(userId);
            return Ok(favorites);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddToFavorites([FromBody] FavouriteDTO favoriteDTO)
        {
            if (ModelState.IsValid) 
            {
                var apartment = await _unitOfWork.apartmentRepository.GetByIdAsync(favoriteDTO.apartmentId);
                if(apartment is null)
                    return NotFound(new ApiErrorResponse(404, "Apartment with this id is not found"));

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var fav = await _unitOfWork.favouriteRepository.GetFavouritesAsync(userId , favoriteDTO.apartmentId);
                if(fav is not null)
                    return BadRequest(new ApiErrorResponse(400, "This Apartment is added to this user before"));



                var map = _Mapper.Map<Favourite>(favoriteDTO);
                map.UserId= userId;
                var count = await _unitOfWork.favouriteRepository.AddAsync(map);
                if (count > 0)
                {

                    return Ok(favoriteDTO);
                }
                return BadRequest(new ApiErrorResponse(400, "Error in save favourite"));
            }
            return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                    , "a bad Request , You have made"
                    , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        }


        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> RemoveFromFavorites(string UserId, int ApartmentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == UserId)
            {
                var fav = await _unitOfWork.favouriteRepository.GetFavouritesAsync(UserId, ApartmentId);
                var count = await _unitOfWork.favouriteRepository.DeleteFavouritesAsync(fav);
                if (count > 0)
                {
                    return Ok();
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest , "Error when remove favourite"));
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest , "Don't have access to remove this comment"));

        }
    }
}
