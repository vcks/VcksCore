using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;
using VcksCore.DAL.Repositories;
using VcksCore.BLL.DTO;
using VcksCore.BLL.Infrastructure;

namespace VcksCore.BLL.Services
{
    public class ProfileService
    {
        readonly ProfileManager profileManager;
        readonly IMapper mapper;
        public ProfileService(ProfileManager profileManager)
        {
            mapper = AutoMapperConfiguration.Get().CreateMapper();
            this.profileManager = profileManager;            
        }

        public async Task Create(VcksUserProfile profile)
        {
            await profileManager.Create(profile);
        }

        public async Task<List<UserProfileDTO>> SearchUsers(string q, int count, int offset)
        {
            var profiles = await profileManager.SearchUsers(q, count, offset);
            var profilesDTO = mapper.Map<List<VcksUserProfile>, List<UserProfileDTO>>(profiles);
            return profilesDTO;
        }

        public async Task<UserProfileDTO> GetUserProfile(int userId)
        {
            var profile = await profileManager.GetUserProfile(userId);
            var profileDTO = mapper.Map<VcksUserProfile, UserProfileDTO>(profile);
            return profileDTO; 
        }

        public async Task<List<UserProfileDTO>> GetUserProfiles(int count, int offset)
        {
            var profiles = await profileManager.GetUserProfiles(count, offset);
            var profilesDTO =  mapper.Map<List<VcksUserProfile>, List<UserProfileDTO>>(profiles);
            return profilesDTO;
        }
    }
}
