using System.Collections.Generic;

using VcksCore.DAL.Entities;

namespace VcksCore.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public UserProfileDTO Profile { get; set; }
        public List<UserProfileDTO> Followers { get; set; }
        public List<UserProfileDTO> Friends { get; set; }
        public List<WallPostDTO> Wall { get; set; }
        public List<FileDTO> Files { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
