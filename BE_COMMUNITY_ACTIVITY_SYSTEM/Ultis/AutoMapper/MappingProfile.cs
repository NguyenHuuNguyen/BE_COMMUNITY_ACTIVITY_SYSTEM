using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserGetDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Announcement, AnnouncementGetDto>();
            CreateMap<AnnouncementCreateDto, Announcement>();
            CreateMap<AnnouncementUpdateDto, Announcement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CommunityActivityType, CommunityActivityTypeGetDto>();
            CreateMap<CommunityActivityTypeCreateDto, CommunityActivityType>();
            CreateMap<CommunityActivityTypeUpdateDto, CommunityActivityType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Major, MajorGetDto>()
                .ForMember(dest => dest.MajorHeadFullName, opt =>
                    opt.MapFrom(src => string.Concat(src.MajorHead!.FirstName, " ", src.MajorHead.LastName)));
            CreateMap<MajorCreateDto, Major>();
            CreateMap<MajorUpdateDto, Major>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
