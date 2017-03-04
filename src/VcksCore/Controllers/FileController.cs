using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using VcksCore.BLL.Services;
using VcksCore.Models;

namespace VcksCore.Controllers
{
    public class FileController : VcksController
    {
        public FileController(IHttpContextAccessor httpContextAccessor, AccountService accountService, DialogService dialogService, FileService fileService, ProfileService profileService, RelationshipService relationshipService, WallService wallService) : base(httpContextAccessor, accountService, dialogService, fileService, profileService, relationshipService, wallService)
        {
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = int.MaxValue)]
        public async Task<ActionResult> Get(Guid id)
        {
            var fileToRetrieve = await fileService.GetById(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}
