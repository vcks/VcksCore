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
    public class RelationshipService
    {
        readonly RelationshipManager relationshipManager;
        readonly IMapper mapper;

        public RelationshipService(RelationshipManager relationshipManager)
        {
            mapper = AutoMapperConfiguration.Get().CreateMapper();
            this.relationshipManager = relationshipManager;
        }

        public async Task<List<UserProfileDTO>> GetFriendsByUserId(int userId, int count, int offset, bool random)
        {
            var friends = await relationshipManager.GetFriendsByUserId(userId,count,offset,random);
            var friendsDTO = mapper.Map<List<Friend>, List<UserProfileDTO>>(friends);
            return friendsDTO;
        }

        public async Task<List<UserProfileDTO>> GetFollowersByUserId(int userId, int count, int offset, bool random)
        {
            var followers = await relationshipManager.GetFollowersByUserId(userId, count, offset, random);
            var followersDTO = mapper.Map<List<Follower>, List<UserProfileDTO>>(followers);
            return followersDTO;
        }

        public async Task Follow(int callerId, int userId)
        {
            await relationshipManager.Follow(callerId, userId);
        }

        public async Task Unfollow(int callerId, int userId)
        {
            await relationshipManager.Unfollow(callerId, userId);
        }
    }
}
