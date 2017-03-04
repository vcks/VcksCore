using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;

namespace VcksCore.DAL.Repositories
{
    public class ProfileManager
    {
        VcksDbContext db;

        public ProfileManager(VcksDbContext db)
        {
            this.db = db;
            System.Diagnostics.Debug.WriteLine("{0} : {1}", GetType().Name, ((object)db).GetHashCode());
        }

        public async Task Create(VcksUserProfile profile)
        {
            db.UserProfiles.Add(profile);
            await db.SaveChangesAsync();
        }

        public async Task<List<VcksUserProfile>> SearchUsers(string q, int count, int offset = 0)
        {
            return await db.UserProfiles.Include(s => s.Avatar).Where(p => p.FirstName.Contains(q) || p.LastName.Contains(q)).OrderByDescending(u => u.Id).Skip(offset).Take(count).ToListAsync();
        }

        public async Task<VcksUserProfile> GetUserProfile(int userId)
        {
            return await db.UserProfiles.Include(s => s.Avatar).FirstOrDefaultAsync(i => i.Id == userId);
        }

        public async Task<List<VcksUserProfile>> GetUserProfiles(int count, int offset = 0)
        {
            return await db.UserProfiles.Include(s=>s.Avatar).OrderByDescending(u => u.Id).Skip(offset).Take(count).ToListAsync();
        }
    }
}
