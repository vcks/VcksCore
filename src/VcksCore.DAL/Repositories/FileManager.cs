using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;

namespace VcksCore.DAL.Repositories
{
    public class FileManager
    {
        VcksDbContext db;

        public FileManager(VcksDbContext db)
        {
            this.db = db;
            System.Diagnostics.Debug.WriteLine("{0} : {1}", GetType().Name, ((object)db).GetHashCode());
        }

        public async Task Create(File file)
        {
            db.Files.Add(file);
            await db.SaveChangesAsync();
        }

        public async Task<File> GetById(Guid id)
        {
            return await db.Files.FindAsync(id);
        }

        public async Task Update(File file)
        {
            db.Entry(file).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
            }
}
