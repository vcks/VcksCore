using System.Collections.Generic;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace VcksCore.DAL.Entities
{
    public class VcksUser : IdentityUser<int>
    {
        public VcksUserProfile Profile { get; set; }

        [ForeignKey("VcksUserId")]
        public ICollection<Follower> Followers { get; set; }
        [ForeignKey("VcksUserId")]
        public ICollection<Friend> Friends { get; set; }

        [ForeignKey("OwnerId")]
        public ICollection<WallPost> Wall { get; set; }

        [ForeignKey("OwnerId")]
        public ICollection<File> Files { get; set; }

        [ForeignKey("OwnerId")]
        public ICollection<Photo> Photos { get; set; }

        public VcksUser()
        {
            Followers = new List<Follower>();
            Friends = new List<Friend>();
            Files = new List<File>();
            Wall = new List<WallPost>();
            Photos = new List<Photo>();
        }
    }
}
