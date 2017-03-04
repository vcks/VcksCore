using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;
using VcksCore.DAL.Repositories;
using VcksCore.BLL.DTO;
using VcksCore.BLL.Infrastructure;

namespace VcksCore.BLL.Services
{
    public class AccountService
    {
        readonly UserManager<VcksUser> userManager;
        readonly RoleManager<VcksRole> roleManager;
        readonly SignInManager<VcksUser> signInManager;

        readonly AccountManager accountManager;
        readonly ProfileManager profileManager;

        readonly IMapper mapper;

        public AccountService(AccountManager accountManager, ProfileManager profileManager, UserManager<VcksUser> userManager, RoleManager<VcksRole> roleManager, SignInManager<VcksUser> signInManager)
        {
            mapper = AutoMapperConfiguration.Get().CreateMapper();
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.accountManager = accountManager;
            this.profileManager = profileManager;
        }

        public async Task<OperationDetails> Login(LoginModelDTO lm)
        {
            var result = await signInManager.PasswordSignInAsync(lm.Email, lm.Password,true,true);
            return new OperationDetails(result.Succeeded, "","");
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<OperationDetails> Create(RegisterModelDTO rm)
        {
            VcksUser user = await userManager.FindByNameAsync(rm.Email);

            if (user == null)
            {
                var original = new File()
                {
                    FileName = rm.Avatar.FileName,
                    ContentType = rm.Avatar.ContentType,
                    Content = rm.Avatar.Content,
                };
                var square = new File()
                {
                    FileName = rm.Avatar.FileName,
                    ContentType = rm.Avatar.ContentType,
                    Content = ImageHelpers.GetCroppedImage(rm.Avatar.Content),
                };
                var square_100 = new File()
                {
                    FileName = rm.Avatar.FileName,
                    ContentType = rm.Avatar.ContentType,
                    Content = ImageHelpers.GetCroppedImage(rm.Avatar.Content, 100),
                };
                var square_300 = new File()
                {
                    FileName = rm.Avatar.FileName,
                    ContentType = rm.Avatar.ContentType,
                    Content = ImageHelpers.GetCroppedImage(rm.Avatar.Content, 100),
                };
                var square_600 = new File()
                {
                    FileName = rm.Avatar.FileName,
                    ContentType = rm.Avatar.ContentType,
                    Content = ImageHelpers.GetCroppedImage(rm.Avatar.Content, 100),
                };

                user = new VcksUser() { Email = rm.Email, UserName = rm.Email };

                user.Files = new List<File>() { original, square, square_100, square_300, square_600 };

                user.Profile = new VcksUserProfile() { FirstName = rm.FirstName, LastName = rm.LastName, Email = rm.Email,
                    Avatar = new Avatar { Default = !rm.HasAvatar, Original = original, Square =square, Square_100 = square_100, Square_300 = square_300, Square_600 =square_600 }
                };

                var result = await userManager.CreateAsync(user, rm.Password);

                if (result.Errors.Count() > 0) return new OperationDetails(false, result.Errors.FirstOrDefault().Description, "");

                await userManager.AddToRoleAsync(user, rm.Role);
                
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task SetRoles(List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new VcksRole { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }
        }

        public async Task<UserDTO> GetUser(int userId)
        {
            VcksUser user = await accountManager.GetUser(userId);
            var userDTO = mapper.Map<VcksUser, UserDTO>(user);
            return userDTO;
        }
    }
}
