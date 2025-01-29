using Microsoft.AspNetCore.Http;
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

namespace Mo3tarb.APIs.PL.Controllers
{
    public class ApartmentController : APIBaseController
    {
        private readonly IMapper _Mapper;
        private readonly UserManager<AppUser> _UserManager;
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentController( IMapper mapper , UserManager<AppUser> userManager , IApartmentRepository apartmentRepository)
        {
            _Mapper = mapper;
            _UserManager = userManager;
            _apartmentRepository = apartmentRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetAllApartments() 
        {
            var Apartments = await _apartmentRepository.GetAllAsync();
                return Ok(Apartments);
        }
     

        [HttpGet("{Id}")]
        public async Task<ActionResult<Apartment>> GetApartmentById(int Id) 
        {
            var Apartment= await _apartmentRepository.GetByIdAsync(Id);

            return Ok(Apartment);
        }



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
                apartment.UserId = "Test"; /*_UserManager.GetUserId(User);*/

                foreach (var item in apartmentDTO.Images) 
                {
                    apartment.ImagesURL.Add(DocumentSettings.Upload(apartmentDTO.BaseImage, "Images"));
                }

                var count = await _apartmentRepository.AddAsync(apartment);
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
    }
}
