using AutoMapper;
using Mo3tarb.APIs.PL.DTOs.AccountDTO;
using Mo3tarb.APIs.PL.DTOs.ApartmentDTO;
using Mo3tarb.APIs.PL.DTOs.ChatMessagesDTO;
using Mo3tarb.APIs.PL.DTOs.CommentDTO;
using Mo3tarb.APIs.PL.DTOs.DepartmentDTO;
using Mo3tarb.APIs.PL.DTOs.FavouriteDTO;
using Mo3tarb.APIs.PL.DTOs.RatingDTO;
using Mo3tarb.APIs.PL.DTOs.ReportDTO;
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
            CreateMap<GetUserDTO, AppUser>().ReverseMap();
            CreateMap<AppUser , GetSanaieeDTO > ()
                .ForMember(src=>src.DepartmentName , u=>u.MapFrom(s=>s.Department.Name));

			CreateMap<Department, DepartmentDTO>().ReverseMap();
			CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment, ReturnCommentDTO>()
                .ForMember(src => src.UserName, opt => opt.MapFrom(e => e.User.UserName))
                .ForMember(src => src.UserId, opt => opt.MapFrom(e => e.User.Id));
			CreateMap<Favourite, FavouriteDTO>().ReverseMap();


            CreateMap<Favourite, ReturnFavouriteDTO>()
           .ForMember(e => e.Id , opt => opt.MapFrom(src => src.apartmentId))
           .ForMember(e => e.UserName , opt => opt.MapFrom(src => src.User.UserName))
           .ForMember(e => e.UserWhatsapp , opt => opt.MapFrom(src => src.User.WhatsappNumber))
           .ForMember(e => e.UserPhone , opt => opt.MapFrom(src => src.User.PhoneNumber))
           .ForMember(e => e.City, opt => opt.MapFrom(src => src.apartment.City))
           .ForMember(e => e.Village, opt => opt.MapFrom(src => src.apartment.Village))
           .ForMember(e => e.DateOfCreation, opt => opt.MapFrom(src => src.apartment.DateOfCreation))
           .ForMember(e => e.Type, opt => opt.MapFrom(src => src.apartment.Type))
           .ForMember(e => e.DistanceByMeters, opt => opt.MapFrom(src => src.apartment.DistanceByMeters))
           .ForMember(e => e.Location, opt => opt.MapFrom(src => src.apartment.Location))
           .ForMember(e => e.BaseImageURL, opt => opt.MapFrom(src => src.apartment.BaseImageURL))
           .ForMember(e => e.NumOfRooms, opt => opt.MapFrom(src => src.apartment.NumOfRooms))
           .ForMember(e => e.Price, opt => opt.MapFrom(src => src.apartment.Price))
           .ForMember(e => e.IsRent, opt => opt.MapFrom(src => src.apartment.IsRent))
           .ForMember(e => e.ImagesURL, opt => opt.MapFrom(src => src.apartment.ImagesURL));



            CreateMap<Apartment, ReturnApartmentDTO>()
           .ForMember(e => e.UserName, opt => opt.MapFrom(src => src.User.UserName)) // Map UserName
           .ForMember(e => e.UserPhone, opt => opt.MapFrom(src => src.User.PhoneNumber)) // Map UserName
           .ForMember(e => e.UserWhatsapp, opt => opt.MapFrom(src => src.User.WhatsappNumber)); // Map UserName

            CreateMap<Rating, RatingDTO>().ReverseMap();
            CreateMap<ChatMessage, ReturnChat>()
                .ForMember(c=>c.SenderName , opt =>opt.MapFrom(src=>src.Sender.UserName))
                .ForMember(c=>c.ReceiverName , opt=>opt.MapFrom(src=>src.Receiver.UserName));

            CreateMap<Report, ReturnReportDTO>()
                .ForMember(R=>R.UserName , opt=>opt.MapFrom(src=>src.User.UserName));
		}

    }
}