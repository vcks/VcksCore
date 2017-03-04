using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using VcksCore.DAL.EF;

namespace VcksCore.DAL.Migrations
{
    [DbContext(typeof(VcksDbContext))]
    partial class VcksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Avatar", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("Default");

                    b.Property<Guid?>("OriginalId");

                    b.Property<Guid?>("SquareId");

                    b.Property<Guid?>("Square_100Id");

                    b.Property<Guid?>("Square_300Id");

                    b.Property<Guid?>("Square_600Id");

                    b.HasKey("Id");

                    b.HasIndex("OriginalId");

                    b.HasIndex("SquareId");

                    b.HasIndex("Square_100Id");

                    b.HasIndex("Square_300Id");

                    b.HasIndex("Square_600Id");

                    b.ToTable("Avatar");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Dialog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content");

                    b.Property<string>("ContentType")
                        .HasMaxLength(100);

                    b.Property<string>("FileName")
                        .HasMaxLength(255);

                    b.Property<int?>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Follower", b =>
                {
                    b.Property<int?>("VcksUserId");

                    b.Property<int>("ProfileId");

                    b.HasKey("VcksUserId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Follower");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Friend", b =>
                {
                    b.Property<int?>("VcksUserId");

                    b.Property<int>("ProfileId");

                    b.HasKey("VcksUserId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Friend");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("DialogId");

                    b.Property<int?>("FromId");

                    b.Property<bool>("Read");

                    b.Property<int?>("ToId");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Participant", b =>
                {
                    b.Property<int?>("DialogId");

                    b.Property<int?>("ProfileId");

                    b.HasKey("DialogId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("FileId");

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.VcksRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.VcksUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.VcksUserProfile", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.WallPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("FromId");

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("OwnerId");

                    b.ToTable("WallPosts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Avatar", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksUserProfile")
                        .WithOne("Avatar")
                        .HasForeignKey("VcksCore.DAL.Entities.Avatar", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VcksCore.DAL.Entities.File", "Original")
                        .WithMany()
                        .HasForeignKey("OriginalId");

                    b.HasOne("VcksCore.DAL.Entities.File", "Square")
                        .WithMany()
                        .HasForeignKey("SquareId");

                    b.HasOne("VcksCore.DAL.Entities.File", "Square_100")
                        .WithMany()
                        .HasForeignKey("Square_100Id");

                    b.HasOne("VcksCore.DAL.Entities.File", "Square_300")
                        .WithMany()
                        .HasForeignKey("Square_300Id");

                    b.HasOne("VcksCore.DAL.Entities.File", "Square_600")
                        .WithMany()
                        .HasForeignKey("Square_600Id");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.File", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithMany("Files")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Follower", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksUserProfile", "Profile")
                        .WithOne()
                        .HasForeignKey("VcksCore.DAL.Entities.Follower", "ProfileId");

                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithMany("Followers")
                        .HasForeignKey("VcksUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Friend", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksUserProfile", "Profile")
                        .WithOne()
                        .HasForeignKey("VcksCore.DAL.Entities.Friend", "ProfileId");

                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithMany("Friends")
                        .HasForeignKey("VcksUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Message", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.Dialog")
                        .WithMany("Messages")
                        .HasForeignKey("DialogId");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Participant", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.Dialog")
                        .WithMany("Participants")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VcksCore.DAL.Entities.VcksUserProfile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.Photo", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithMany("Photos")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.VcksUserProfile", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithOne("Profile")
                        .HasForeignKey("VcksCore.DAL.Entities.VcksUserProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VcksCore.DAL.Entities.WallPost", b =>
                {
                    b.HasOne("VcksCore.DAL.Entities.VcksUserProfile", "From")
                        .WithMany()
                        .HasForeignKey("FromId");

                    b.HasOne("VcksCore.DAL.Entities.VcksUser")
                        .WithMany("Wall")
                        .HasForeignKey("OwnerId");

                    b.HasOne("VcksCore.DAL.Entities.VcksUserProfile", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });
        }
    }
}
