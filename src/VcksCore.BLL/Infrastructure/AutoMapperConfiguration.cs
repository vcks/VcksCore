using AutoMapper;

using VcksCore.DAL.Entities;
using VcksCore.BLL.DTO;

namespace VcksCore.BLL.Infrastructure
{
    public static class AutoMapperConfiguration
    {

        public class VcksMappingProfile : Profile
        {
            public VcksMappingProfile()
            {
                CreateMap<UserDTO, VcksUser>();
                CreateMap<VcksUser, UserDTO>();

                CreateMap<FileDTO, File>();
                CreateMap<File, FileDTO>();

                CreateMap<WallPostDTO, WallPost>();
                CreateMap<WallPost, WallPostDTO>();

                CreateMap<UserProfileDTO, VcksUserProfile>();
                CreateMap<VcksUserProfile, UserProfileDTO>();

                CreateMap<AvatarDTO, Avatar>();
                CreateMap<Avatar, AvatarDTO>();

                CreateMap<DialogDTO, Dialog>();
                CreateMap<Dialog, DialogDTO>();

                CreateMap<MessageDTO, Message>();
                CreateMap<Message, MessageDTO>();

                CreateMap<ParticipantDTO, Participant>();
                CreateMap<Participant, ParticipantDTO>();

                CreateMap<Follower, UserProfileDTO>().ForMember<int>(m => m.Id, o => o.MapFrom(p => p.Profile.Id))
                    .ForMember<string>(m => m.FirstName, o => o.MapFrom(p => p.Profile.FirstName))
                    .ForMember<string>(m => m.LastName, o => o.MapFrom(p => p.Profile.LastName))
                    .ForMember<string>(m => m.Email, o => o.MapFrom(p => p.Profile.Email))
                    .ForMember<bool>(m => m.Deleted, o => o.MapFrom(p => p.Profile.Deleted))
                    .ForMember<AvatarDTO>(m => m.Avatar, o => o.MapFrom(p => p.Profile.Avatar));

                CreateMap<Friend, UserProfileDTO>().ForMember<int>(m => m.Id, o => o.MapFrom(p => p.Profile.Id))
                   .ForMember<string>(m => m.FirstName, o => o.MapFrom(p => p.Profile.FirstName))
                   .ForMember<string>(m => m.LastName, o => o.MapFrom(p => p.Profile.LastName))
                   .ForMember<string>(m => m.Email, o => o.MapFrom(p => p.Profile.Email))
                   .ForMember<bool>(m => m.Deleted, o => o.MapFrom(p => p.Profile.Deleted))
                   .ForMember<AvatarDTO>(m => m.Avatar, o => o.MapFrom(p => p.Profile.Avatar));
            }
        }

        public static MapperConfiguration Get()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VcksMappingProfile());
            });
            return mapperConfiguration;
        }
    }
}