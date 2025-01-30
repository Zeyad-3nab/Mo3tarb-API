using AutoMapper;
using Mo3tarb.API.DTOs.DepartmentDTOs;
using Mo3tarb.APIs.DTOs;
using Mo3tarb.APIs.PL.DTOs;
using Mo3tarb.Core.Entites;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Models;

namespace GraduationProject.API.PL.Mapping
{
    public class Applicationprofile:Profile
    {
        public Applicationprofile(IConfiguration configuration)
        {
            CreateMap<RegisterDto, AppUser>().ReverseMap();

            //CreateMap<ApartmentDTO, Apartment>();

            //CreateMap<Apartment, ApartmentDTO>();

			CreateMap<Department, DepartmentDTO>().ReverseMap();
			CreateMap<Comment, CommentDTO>().ReverseMap();
			CreateMap<Favourite, FavouriteDTO>().ReverseMap();
		}

    }
}
