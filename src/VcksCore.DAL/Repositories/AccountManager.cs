using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using VcksCore.DAL.Entities;
using VcksCore.DAL.EF;

namespace VcksCore.DAL.Repositories
{
    public class AccountManager
    {
        readonly VcksDbContext db;
        public AccountManager(VcksDbContext db)
        {
            this.db = db;
            System.Diagnostics.Debug.WriteLine("{0} : {1}", GetType().Name, ((object)db).GetHashCode());
        }

        public async Task<VcksUser> GetUser(int userId)
        {
            VcksUser user = db.Users
                .Include(z => z.Followers)
                .ThenInclude(x => x.Profile.Avatar)
                .Include(z => z.Friends)
                .ThenInclude(x => x.Profile.Avatar)
                .Include(z => z.Profile.Avatar)
                .FirstOrDefault(u => u.Id == userId);

            if (user != null)
                return user;
            else
                return null;
        }

    }
}
