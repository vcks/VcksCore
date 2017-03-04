using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

using VcksCore.BLL.Services;
using VcksCore.Models;

namespace VcksCore.Controllers
{
    public class HomeController : VcksController
    {
        public HomeController(IHttpContextAccessor httpContextAccessor, AccountService accountService, DialogService dialogService, FileService fileService, ProfileService profileService, RelationshipService relationshipService, WallService wallService) : base(httpContextAccessor, accountService, dialogService, fileService, profileService, relationshipService, wallService)
        { }       

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        [Authorize]
        public async Task<IActionResult> People()
        {
            ViewBag.Title = "People";
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Friends()
        {            
            ViewBag.CurrentUserId = GetUserId();
            ViewBag.Title = "Friends";
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Messages(int? id)
        {
            int currentUserId = GetUserId();
            ViewBag.CurrentUserId = currentUserId;
            ViewBag.Title = "Messages";

            if (id != null)
            {
                var dialog = await dialogService.GetDialogBetweenUsers(new int[] { currentUserId, (int)id });
                if (dialog != null) ViewBag.UserId = (int)id;
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile(int? id)
        {
            int currentUserId = GetUserId();
            int userId = (id == null || id < 1) ? currentUserId : (int)id;
            var profile = await profileService.GetUserProfile(userId) ?? await profileService.GetUserProfile(currentUserId);
            ViewBag.Title = $"{profile.FirstName} {profile.LastName}";
            return View(profile);
        }

        public IActionResult Error(string id)
        {
            switch(id)
            {
                case "401":
                    return File("~/errors/401.html", "text/html");
                    break ;
                case "403":
                    return File("~/errors/403.html", "text/html");
                    break;
                case "404":
                    return File("~/errors/404.html", "text/html");
                    break;
                default:
                    return View();
                    break;
            }
        }

        [Authorize]
        public IActionResult ProfileActions(int id)
        {
            return ViewComponent("ProfileActions", new { id = id });
        }

        [Authorize]
        public IActionResult Wall(int userId, int count, int offset)
        {
            return ViewComponent("Wall", new { userId = userId, count = count, offset = offset });
        }

        [Authorize]
        public IActionResult UserList(string q, int type, int userId, int offset, int count = 100)
        {
            return ViewComponent("UserList", new { count = count, offset = offset, q = q, type = type, userId = userId });
        }

    }
}
