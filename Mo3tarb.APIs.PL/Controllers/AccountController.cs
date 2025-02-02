using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Mo3tarb.APIs.DTOs;
using Mo3tarb.APIs.Errors;
using Mo3tarb.APIs.Extensions;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Services;
using Mo3tarb.Services;
using Mo3tarb.APIs.PL.Controllers;
using AutoMapper;
using Mo3tarb.API.Extensions;
using Azure;
using System.Net;
using Mo3tarb.APIs.PL.Helper;
using System.ComponentModel.DataAnnotations;
using Mo3tarb.Core.Entites;
using Mo3tarb.APIs.PL.DTOs;
using Microsoft.VisualBasic;
using Mo3tarb.APIs.PL.Errors;
using Microsoft.EntityFrameworkCore;

namespace Mo3tarb.APIs.Controllers
{
    public class AccountController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenServices,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetAllUsers() 
        {
            var users = await _userManager.Users.ToListAsync();
            var map=_mapper.Map<IEnumerable<GetUserDTO>>(users);
            return Ok(map);
        }


        [HttpGet("SearchByName")]
        public async Task<ActionResult<IEnumerable<RegisterDto>>> SearchByName(string Name)
        {
            var users = await _userManager.Users.Where(u=>u.NormalizedUserName.Contains(Name.ToUpper())).ToListAsync();
            var map = _mapper.Map<IEnumerable<RegisterDto>>(users);
            return Ok(map);
        }


        [AllowAnonymous]
        [HttpGet("GetUserById")]
        public async Task<ActionResult<RegisterDto>> GetUserById(string userId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));
                }

                var map = _mapper.Map<RegisterDto>(user);
                return Ok(map);
                
            }
            return BadRequest(new ApiValidationResponse(400
                     , "a bad Request , You have made"
                     , ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList()));
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null) return Unauthorized(new ApiErrorResponse(401, "User with this Email is not found"));
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!result.Succeeded) return Unauthorized(new ApiErrorResponse(401, "Password is InCorrect"));

                return Ok(new UserDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenServices.CreateTokenAsync(user, _userManager)
                });
            }
            return BadRequest(new ApiValidationResponse(400
               , "a bad Request , You have made"
               , ModelState.Values
               .SelectMany(v => v.Errors)
               .Select(e => e.ErrorMessage)
               .ToList()));
        }

        // Register
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                if (CheckEmailExists(model.Email).Result.Value) // هنا بيشوف لو الايميل بتاعي موجود ولا لا
                {
                    return BadRequest(new ApiErrorResponse(400, "Email Is Already in Used"));
                }

                var user = _mapper.Map<AppUser>(model);    //Map From AppUserDTO TO App User 

                var Result = await _userManager.CreateAsync(user, model.Password);

                if (!Result.Succeeded)
                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                    , "a bad Request , You have made"
                    , Result.Errors.Select(e => e.Description).ToList())); //UnSaved

                await _userManager.AddToRoleAsync(user, model.Type);

                var ReturnedUser = new UserDto()
                {

                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenServices.CreateTokenAsync(user, _userManager)
                };
                return Ok(ReturnedUser);
            }
            return BadRequest(new ApiValidationResponse(400
                     , "a bad Request , You have made"
                     , ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList()));
        }

        // GetCurrentUser
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme/*"Bearer"*/)]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            // User =>Login اللي عامل User بتاعه ال Claim دي بتكون شايله ال
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            var ReturntedObject = new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            };
            return Ok(ReturntedObject);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string Id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user is not null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest 
                        , "a bad Request , You have made" 
                        , result.Errors.Select(e=>e.Description).ToList()));
                }
                return NotFound(new ApiErrorResponse(404 , "User with this id is not found"));
            }
            return BadRequest(new ApiValidationResponse(400
                , "a bad Request , You have made"
                , ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList()));
        }


        [AllowAnonymous]
        [HttpPost("SendEmail")]
        public async Task<ActionResult> SendEmail([DataType(DataType.EmailAddress)] string Email)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    Random random = new Random();
                    int Code = random.Next(1000, 9999);
                    var email = new Emails()
                    {
                        To = Email,
                        Subject = "Reset Password",
                        Body = $"Resetting Your Password in Mo3tarib App\r\n\r\nOpen the app and click \"Forgot Password?\r\n\r\nEnter your email or username.Code\r\n\r\nCode = {Code}"
                    };

                    EmailSettings.SendEmail(email);
                    return Ok(Code);
                }
                return NotFound(new ApiErrorResponse(404, "User with this Email is not found"));
            }
            return BadRequest(new ApiValidationResponse(400 
                , "a bad Request , You have made"
                , ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList()));
        }


        [AllowAnonymous]
        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody]UpdatePasswordDTO updatePasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(updatePasswordDTO.Email);
                if (user is not null)
                {
                    var result = await _userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddPasswordAsync(user, updatePasswordDTO.Password);
                        if (result.Succeeded)
                        {
                            return Ok("changed");
                        }
                    }
                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest 
                        , "a bad Request , You have made" 
                        , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
                }
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound , "User with this Email is not found"));
            }
            return BadRequest(new ApiValidationResponse(400
                , "a bad Request , You have made"
                , ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList()));
        }


        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null) return false;
            else return true;
        }


        [HttpGet("UserRoles")]
        public async Task<ActionResult> GetUserRoles(string Email)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return Ok(roles);
                }
                return NotFound(new ApiValidationResponse(404, "User with this Email is not found"));
            }
            return BadRequest(new ApiValidationResponse(400,
                             "a bad Request , You have made",
                             ModelState.Values
                             .SelectMany(v => v.Errors)
                             .Select(e => e.ErrorMessage)
                             .ToList()));
        }

        [AllowAnonymous]
        [HttpPost("AddToRole")]
        public async Task<IActionResult> AddToRole([FromBody] AddToRoleDTO addToRoleDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(addToRoleDTO.Email);
                if (user is not null)
                {
                    var result = await _userManager.AddToRoleAsync(user, addToRoleDTO.Role);
                    if (result.Succeeded)
                        return Ok();

                    else
                        return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                                          , "a bad Request , You have made"
                                          , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));

                }
                return NotFound(new ApiErrorResponse(404, "User with this Email is not found"));
            }
            return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                              , "a bad Request , You have made"
                              , ModelState.Values
                              .SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage)
                              .ToList()));
        }
    }
}
