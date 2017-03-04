using System.Collections.Generic;
using VcksCore.BLL.DTO;

namespace VcksCore.Models
{
    public class ProfileActionsModel
    {
        public int Id { get; set; }
        public bool Self { get; set; }
        public bool AmIFollowing { get; set; }
        public bool IsMyFriend { get; set; }

        public int FollowerCount;

        public List<UserProfileDTO> Friends;
    }
}