using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VcksCore.BLL.Services;
using VcksCore.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace VcksCore.Controllers
{
    public class RelationshipController :  VcksController
    {
        public RelationshipController(IHttpContextAccessor httpContextAccessor, AccountService accountService, DialogService dialogService, FileService fileService, ProfileService profileService, RelationshipService relationshipService, WallService wallService) : base(httpContextAccessor, accountService, dialogService, fileService, profileService, relationshipService, wallService)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Follow(int id)
        {
            int callerId = GetUserId();
            int userId = id;

            if (!callerId.Equals(userId) && userId > 0)
                await relationshipService.Follow(callerId, userId);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unfollow(int id)
        {
            int callerId = GetUserId();
            int userId = id;

            if (!callerId.Equals(userId) && userId > 0)
                await relationshipService.Unfollow(callerId, userId);

            return Ok();
        }
    }
}
