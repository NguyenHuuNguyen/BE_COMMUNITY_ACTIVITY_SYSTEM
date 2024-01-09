using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.NewFolder;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserGetDto>();
            CreateMap<User, UserGetWithCommunityActivityScoreDto>();
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
                    opt.MapFrom(src => src.MajorHead != null ? string.Concat(src.MajorHead.FirstName, " ", src.MajorHead.LastName) : string.Empty));
            CreateMap<MajorCreateDto, Major>();
            CreateMap<MajorUpdateDto, Major>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Class, ClassGetDto>()
                .ForMember(dest => dest.MajorName, opt =>
                    opt.MapFrom(src => src.Major != null ? src.Major.Name : string.Empty))
                .ForMember(dest => dest.HeadTeacherFullName, opt =>
                    opt.MapFrom(src => src.HeadTeacher != null ? string.Concat(src.HeadTeacher.FirstName, " ", src.HeadTeacher.LastName) : string.Empty))
                .ForMember(dest => dest.ClassPresidentFullName, opt =>
                    opt.MapFrom(src => src.ClassPresident != null ? string.Concat(src.ClassPresident.FirstName, " ", src.ClassPresident.LastName): string.Empty));
            CreateMap<ClassCreateDto, Class>();
            CreateMap<ClassUpdateDto, Class>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CommunityActivity, CommunityActivityGetDto>()
                .ForMember(dest => dest.ActivityTypeName, opt =>
                    opt.MapFrom(src => src.CommunityActivityType != null ? src.CommunityActivityType.Name : string.Empty));
            CreateMap<CommunityActivityCreateDto, CommunityActivity>();
            CreateMap<CommunityActivityUpdateDto, CommunityActivity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Setting, SettingGetDto>();
        }
    }
}
