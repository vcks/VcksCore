using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;
using VcksCore.DAL.Repositories;
using VcksCore.BLL.DTO;
using VcksCore.BLL.Infrastructure;

namespace VcksCore.BLL.Services
{
    public class WallService
    {
        readonly WallManager wallManager;
        readonly IMapper mapper;

        public WallService(WallManager wallManager)
        {
            mapper = AutoMapperConfiguration.Get().CreateMapper();
            this.wallManager = wallManager;            
        }

        public async Task Create(WallPostDTO postDTO)
        {
            var post = mapper.Map<WallPostDTO, WallPost>(postDTO);
            await wallManager.Create(post);
        }

        public async Task<List<WallPostDTO>> GetByUserId(int userId, int count, int offset)
        {
            var posts = await wallManager.GetByUserIdAsync(userId, count, offset);
            var postsDTO = mapper.Map<List<WallPost>, List<WallPostDTO>>(posts);
            return postsDTO;
        }

        public async Task DeleteById(int requestedByUserId, int postId)
        {
            await wallManager.DeleteById(requestedByUserId, postId);
        }

        public async Task EditById(WallPostDTO postDTO)
        {
            var post = mapper.Map<WallPostDTO, WallPost>(postDTO);
            await wallManager.EditById(post);
        }      
    }
}
