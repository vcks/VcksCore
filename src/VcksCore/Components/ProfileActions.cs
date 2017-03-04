using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

using VcksCore.BLL.Services;
using VcksCore.Models;

namespace VcksCore.Components
{
    public class ProfileActions : ViewComponent
    {
        readonly AccountService accountService;

        public ProfileActions(AccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var cp = User as ClaimsPrincipal;
            int currentUserId = int.Parse(cp.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await accountService.GetUser(id);

            if (user != null)
            {
                ProfileActionsModel pam = new ProfileActionsModel();
                pam.Id = user.Id;
                pam.Self = user.Id.Equals(currentUserId);
                pam.AmIFollowing = pam.Self ? false : user.Followers.Any(f => f.Id.Equals(currentUserId));
                pam.IsMyFriend = pam.Self ? false : user.Friends.Any(f => f.Id.Equals(currentUserId));
                pam.Friends = user.Friends;
                pam.FollowerCount = user.Followers.Count;

                return View(pam);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return null;
            }
        }
    }

}
