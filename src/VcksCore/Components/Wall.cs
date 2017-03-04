using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using VcksCore.BLL.Services;

namespace VcksCore.Components
{
    public class Wall : ViewComponent
    {
        readonly WallService wallService;

        public Wall(WallService wallService)
        {
            this.wallService = wallService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId, int count, int offset)
        {
            var cp = User as ClaimsPrincipal;
            var posts = await wallService.GetByUserId(userId, count, offset);
            ViewBag.UserId = int.Parse(cp.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(posts);
        }
    }

}
