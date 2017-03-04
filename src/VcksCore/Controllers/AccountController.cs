using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

using VcksCore.Models;
using VcksCore.BLL.DTO;
using VcksCore.BLL.Infrastructure;
using VcksCore.BLL.Services;

namespace VcksCore.Controllers
{
    public class AccountController : VcksController
    {
        public AccountController(IHttpContextAccessor httpContextAccessor, AccountService accountService, DialogService dialogService, FileService fileService, ProfileService profileService, RelationshipService relationshipService, WallService wallService) : base(httpContextAccessor, accountService, dialogService, fileService, profileService, relationshipService, wallService)
        {
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Profile", "Home");
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                LoginModelDTO lm = new LoginModelDTO()
                {
                    Email = model.Email,
                    Password = model.Password
                };

                OperationDetails result = await accountService.Login(lm);
                
                if (result.Succedeed)
                {
                    return RedirectToAction("Profile", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await accountService.Logout();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Profile", "Home");
            ViewBag.Title = "Register";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromServices] IHostingEnvironment env,RegisterModel model, IFormFile upload)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                RegisterModelDTO rm = new RegisterModelDTO()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    Role = "user"
                };

                if (upload != null && upload.Length > 0)
                {
                    using (var reader = new BinaryReader(upload.OpenReadStream()))
                    {
                        rm.HasAvatar = true;
                        rm.Avatar = new FileDTO()
                        {
                            FileName = Path.GetFileName(upload.FileName),
                            ContentType = upload.ContentType,
                            Content = reader.ReadBytes((int)upload.Length)
                        };
                    }
                }
                else
                {
                    var webRoot = env.WebRootPath;
                    var p = Path.Combine(webRoot, "Images/pf.jpg");
                    rm.Avatar = new FileDTO()
                    {
                        FileName = "pf.jpg",
                        ContentType = "image/jpeg",
                        Content = System.IO.File.ReadAllBytes(p)
                    };
                }

                OperationDetails operationDetails = await accountService.Create(rm);

                if (operationDetails.Succedeed)
                    return RedirectToAction("Login");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(model);
        }

        [NonAction]
        private async Task SetInitialDataAsync()
        {
            await accountService.SetRoles(new List<string> { "user", "admin" });
        }
    }
}
