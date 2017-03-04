using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;

namespace VcksCore.DAL.Repositories
{
    public class WallManager
    {
        VcksDbContext db;
        public WallManager(VcksDbContext db)
        {
            this.db = db;
            System.Diagnostics.Debug.WriteLine("{0} : {1}", GetType().Name, ((object)db).GetHashCode());
        }

        public async Task Create(WallPost post)
        {
            db.WallPosts.Add(post);
            await db.SaveChangesAsync();
        }

        public async Task<List<WallPost>> GetByUserIdAsync(int userId, int count, int offset)
        {
            return await db.WallPosts
                .Include(z => z.Owner).ThenInclude(z => z.Avatar)
                .Include(z => z.From).ThenInclude(z => z.Avatar)
                .Where(p => p.OwnerId == userId).OrderByDescending(p => p.Date).Skip(offset).Take(count).ToListAsync();
        }

        public async Task DeleteById(int requestedByUserId, int postId)
        {
            WallPost post = await db.WallPosts.FindAsync(postId);
            if (post != null)
            {
                if (post.FromId.Equals(requestedByUserId) || post.OwnerId.Equals(requestedByUserId))
                {
                    db.WallPosts.Remove(post);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task EditById(WallPost post)
        {
            WallPost originalPost = await db.WallPosts.FindAsync(post.Id);

            if (originalPost != null)
            {
                if (originalPost.FromId.Equals(post.FromId))
                {
                    originalPost.Text = post.Text;
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
