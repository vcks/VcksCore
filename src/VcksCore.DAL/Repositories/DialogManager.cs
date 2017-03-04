using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;

namespace VcksCore.DAL.Repositories
{
    public class DialogManager
    {
        VcksDbContext db;

        public DialogManager(VcksDbContext db)
        {
            this.db = db;
            System.Diagnostics.Debug.WriteLine("{0} : {1}", GetType().Name, ((object)db).GetHashCode());
        }

        public async Task<Dialog> GetDialogBetweenUsers(int[] users)
        {
            int dipper = users[0], mabel = users[1];

            VcksUser user1 = db.Users.Find(dipper);
            VcksUser user2 = db.Users.Find(mabel);

            if (user1 != null && user2 != null)
            {
                Dialog dialog = db.Dialogs.Include(x=>x.Participants).Include(x=>x.Messages).FirstOrDefault(d => d.Participants.Any(p => p.ProfileId == user1.Id) && d.Participants.Any(p => p.ProfileId == user2.Id));
                if (dialog == null)
                {
                    dialog = new Dialog() { Participants = new List<Participant>() { new Participant() { ProfileId = user1.Id }, new Participant() { ProfileId = user2.Id } } };
                    db.Dialogs.Add(dialog);
                    await db.SaveChangesAsync();
                }
                return dialog;
            }
            return null;
        }

        public async Task MarkMessagesAsRead(List<Message> messages)
        {
            foreach (var m in messages)
            {
                var msg = await db.Messages.FindAsync(m.Id);
                msg.Read = true;
            }
            await db.SaveChangesAsync();
        }

        public async Task<List<Dialog>> GetByUserId(int userId, int count, int offset, bool last)
        {
            List<Dialog> dialogs;

            if (last)
                dialogs = db.Dialogs
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Original)
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Square)
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Square_100)
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Square_300)
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Square_600)
                    .Where(d => d.Participants.Any(p => p.ProfileId == userId))
                    .Include(i => i.Messages)
                    .OrderByDescending(d => d.Messages.Max(m => m.Date)).Skip(offset).Take(count).ToList();
            else
                dialogs = db.Dialogs
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Original)
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Square)
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Square_100)
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Square_300)
                    .Include(x => x.Participants).ThenInclude(x => x.Profile.Avatar.Square_600)
                    .Where(d => d.Participants.Any(p => p.ProfileId == userId))
                    .Include(i => i.Messages).OrderBy(d => d.Id).Skip(offset).Take(count).ToList();
            return dialogs;
        }


        public async Task<Dialog> GetById(int Id)
        {
            var r = db.Dialogs
                .Include(x => x.Messages)
                .FirstOrDefault(x=>x.Id == Id);
            return r;
        }


        public async Task WriteMessage(int dialogId, Message message)
        {
            var dialog = await db.Dialogs.FindAsync(dialogId);

            if (dialog != null)
            {
                db.Entry(dialog).Collection(r => r.Messages).Load();
                dialog.Messages.Add(message);
                await db.SaveChangesAsync();
            }
        }
    }
}
