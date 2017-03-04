using Microsoft.AspNetCore.Mvc;
using VcksCore.BLL.Services;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace VcksCore.Models
{   
    public class VcksController : Controller
    {        
        public VcksController(IHttpContextAccessor httpContextAccessor, AccountService accountService, DialogService dialogService, FileService fileService, ProfileService profileService, RelationshipService relationshipService, WallService wallService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.accountService = accountService;
            this.dialogService = dialogService;
            this.fileService = fileService;
            this.profileService = profileService;
            this.relationshipService = relationshipService;
            this.wallService = wallService;            
        }

        protected int GetUserId() => int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        protected IHttpContextAccessor httpContextAccessor;
        protected AccountService accountService;
        protected DialogService dialogService;
        protected FileService fileService;
        protected ProfileService profileService;
        protected RelationshipService relationshipService;
        protected WallService wallService;
    }
}