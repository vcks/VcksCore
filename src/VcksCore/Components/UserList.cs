using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using VcksCore.BLL.Services;
using VcksCore.BLL.DTO;

namespace VcksCore.Components
{
    public class UserList : ViewComponent
    {
        public enum ListType { Users, Friends, Followers}

        readonly ProfileService profileService;
        readonly RelationshipService relationshipService;

        public UserList(ProfileService profileService, RelationshipService relationshipService)
        {
            this.profileService = profileService;
            this.relationshipService = relationshipService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int offset, string q, ListType type, int userId, int count = 100)
        {
            List<UserProfileDTO> users = null;
            switch (type)
            {
                case ListType.Users:
                    users = q == null ? await profileService.GetUserProfiles(count, offset) : await profileService.SearchUsers(q, count, offset);
                    break;
                case ListType.Friends:
                    users = await relationshipService.GetFriendsByUserId(userId, count, offset, false);
                    break;
                case ListType.Followers:
                    users = await relationshipService.GetFollowersByUserId(userId, count, offset, false);
                    break;
            }
            return View(users);
        }
    }

}
