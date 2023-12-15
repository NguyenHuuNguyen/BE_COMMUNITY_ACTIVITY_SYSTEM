using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Data
{
    public class DataContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<CommunityActivity> CommunityActivities { get; set; }
        public DbSet<CommunityActivityType> CommunityActivityTypes { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Major)
                .WithMany(m => m.Classes)
                .HasForeignKey(c => c.MajorId);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.HeadTeacher)
                .WithMany()
                .HasForeignKey(c => c.HeadTeacherId);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.ClassPresident)
                .WithMany()
                .HasForeignKey(c => c.ClassPresidentId);

            modelBuilder.Entity<CommunityActivity>()
                .HasOne(ca => ca.User)
                .WithMany(u => u.CommunityActivities)
                .HasForeignKey(ca => ca.UserId);

            modelBuilder.Entity<CommunityActivity>()
                .HasOne(ca => ca.CommunityActivityType)
                .WithMany(cat => cat.CommunityActivities)
                .HasForeignKey(ca => ca.ActivityTpeId);

            modelBuilder.Entity<Major>()
                .HasOne(m => m.MajorHead)
                .WithMany()
                .HasForeignKey(m => m.MajorHeadId);

            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.Role)
                .WithMany(r => r.RoleUsers)
                .HasForeignKey(ru => ru.RoleId);

            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.RoleUsers)
                .HasForeignKey(ru => ru.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Class)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.ClassId);

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();
        }
    }
}
