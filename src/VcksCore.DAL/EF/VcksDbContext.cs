using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using VcksCore.DAL.Entities;

namespace VcksCore.DAL.EF
{
    public class VcksDbContext : IdentityDbContext<VcksUser,VcksRole,int>
    {
        public DbSet<WallPost> WallPosts { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<VcksUserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<VcksUser>().HasOne(z => z.Profile).WithOne().HasForeignKey<VcksUserProfile>(x => x.Id);
            builder.Entity<VcksUserProfile>().HasOne(z => z.Avatar).WithOne().HasForeignKey<Avatar>(x => x.Id);

            builder.Entity<VcksUser>().HasMany(x => x.Files).WithOne().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.SetNull);
            builder.Entity<File>().Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Entity<Participant>().HasKey("DialogId", "ProfileId");
            builder.Entity<Friend>().HasKey("VcksUserId", "ProfileId");
            builder.Entity<Follower>().HasKey("VcksUserId", "ProfileId");

            builder.Entity<Follower>().HasOne(x => x.Profile).WithOne().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            builder.Entity<Friend>().HasOne(x => x.Profile).WithOne().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<Follower>().HasIndex(x => x.ProfileId).IsUnique(false);
            builder.Entity<Friend>().HasIndex(x => x.ProfileId).IsUnique(false);

            base.OnModelCreating(builder);
        }

        public VcksDbContext(DbContextOptions<VcksDbContext> options) : base(options) {}
    }
}
