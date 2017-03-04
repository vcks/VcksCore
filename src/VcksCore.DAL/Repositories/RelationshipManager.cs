using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;

namespace VcksCore.DAL.Repositories
{
    public class RelationshipManager
    {
        VcksDbContext db;

        public RelationshipManager(VcksDbContext db)
        {
            this.db = db;
            System.Diagnostics.Debug.WriteLine("{0} : {1}", GetType().Name, ((object)db).GetHashCode());
        }

        public async Task<List<Friend>> GetFriendsByUserId(int userId, int count, int offset, bool random)
        {
            var friends = new List<Friend>();
            var user = await db.Users.Include(x => x.Friends).ThenInclude(x=>x.Profile.Avatar).FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                if (random)
                    friends = user.Friends.Skip(offset).Take(count).ToList(); // need to change
                else
                    friends = user.Friends.Skip(offset).Take(count).ToList();
            }
            return friends;
        }

        public async Task<List<Follower>> GetFollowersByUserId(int userId, int count, int offset, bool random)
        {
            var followers = new List<Follower>();
            var user = await db.Users.Include(x=>x.Followers).ThenInclude(x => x.Profile.Avatar).FirstOrDefaultAsync(u=>u.Id == userId);
            if (user != null)
            {
                if (random)
                    followers = user.Followers.Skip(offset).Take(count).ToList(); // need to change
                else
                    followers = user.Followers.Skip(offset).Take(count).ToList();
            }
            return followers;
        }


        public async Task Follow(int callerId, int userId)
        {
            var caller = db.Users.Include(x => x.Followers).Include(x => x.Friends).FirstOrDefault(u => u.Id == callerId);
            var user = db.Users.Include(x => x.Followers).Include(x => x.Friends).FirstOrDefault(u => u.Id == userId);

            if (caller == null || user == null) return;

            if (caller.Friends.Any(f => f.ProfileId.Equals(user.Id)) || user.Friends.Any(f => f.ProfileId.Equals(caller.Id))) // They're already friends
            {
                return;
            }

            var r = caller.Followers.FirstOrDefault(f => f.ProfileId.Equals(user.Id));

            if (r != null) // MakeEmFriends
            {
                caller.Followers.Remove(r);
                caller.Friends.Add(new Friend() { ProfileId = user.Id });
                user.Friends.Add(new Friend() { ProfileId = caller.Id });
            }
            else // Subscribe
            {
                user.Followers.Add(new Follower() { ProfileId = caller.Id });
            }

            await db.SaveChangesAsync();
        }

        public async Task Unfollow(int callerId, int userId)
        {
            var caller = db.Users.Include(x => x.Followers).Include(x => x.Friends).FirstOrDefault(u => u.Id == callerId);
            var user = db.Users.Include(x => x.Followers).Include(x => x.Friends).FirstOrDefault(u => u.Id == userId);

            if (caller == null || user == null) return;

            var f1 = caller.Friends.FirstOrDefault(f => f.ProfileId.Equals(user.Id));
            var f2 = user.Friends.FirstOrDefault(f => f.ProfileId.Equals(caller.Id));

            if (f1 != null || f2 != null) // They're friends
            {
                caller.Friends.Remove(f1);
                user.Friends.Remove(f2);
                caller.Followers.Add(new Follower() { ProfileId = user.Id });
            }

            var r = user.Followers.FirstOrDefault(f => f.ProfileId.Equals(caller.Id));

            if (r != null) // Unfollow
            {
                user.Followers.Remove(r);
            }
            await db.SaveChangesAsync();
        }
        
    }
}
