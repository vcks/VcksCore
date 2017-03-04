using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

using VcksCore.BLL.Services;
using VcksCore.BLL.DTO;
using VcksCore.Models;

using System.ComponentModel.DataAnnotations;

namespace VcksCore.Controllers
{
    [Route("api/wall")]
    public class WallController : VcksController
    {
        public WallController(IHttpContextAccessor httpContextAccessor, AccountService accountService, DialogService dialogService, FileService fileService, ProfileService profileService, RelationshipService relationshipService, WallService wallService) : base(httpContextAccessor, accountService, dialogService, fileService, profileService, relationshipService, wallService)
        {
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task Post([FromBody]WallPostDTO _post)
        {
            var post = new WallPostDTO()
            {
                Date = DateTime.Now,
                FromId = GetUserId(),
                OwnerId = _post.OwnerId,
                Text = _post.Text
            };
            await wallService.Create(post);
        }

        [HttpPut]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task Put([FromBody]WallPostDTO post)
        {
            post.FromId = GetUserId();
            await wallService.EditById(post);
        }

        [HttpDelete]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task Delete([FromBody]int postId)
        {
            int requestedByUserId = GetUserId();
            await wallService.DeleteById(requestedByUserId, postId);
        }
    }
}
