using System;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTimeTrackerIdentity.Dal.Model;
using TaskTimeTrackerIdentity.Dal.Model.Identity;
using MyTask = TaskTimeTrackerIdentity.Dal.Model.Task;

namespace TaskTimeTrackerIdentity.Dal.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Space> Spaces { get; set; } = null!;
        public DbSet<MyTask> Tasks { get; set; } = null!;
        public DbSet<UserGroup> UserGroups { get; set; } = null!;
        public DbSet<UserSpace> UserSpaces { get; set; } = null!;
        public DbSet<JoinSpaceRequest> JoinSpaceRequests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.RoleId, ug.GroupId });
            builder.Entity<UserSpace>().HasKey(us => new { us.UserId, us.RoleId, us.SpaceId });

            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
                { Name = "Admin", NormalizedName = "ADMIN", Id = 1, ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
                { Name = "User", NormalizedName = "USER", Id = 2, ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
                { Name = "Lead", NormalizedName = "LEAD", Id = 3, ConcurrencyStamp = Guid.NewGuid().ToString() });

            
        }
    }
}

