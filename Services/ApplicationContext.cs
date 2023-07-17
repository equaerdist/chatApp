using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
namespace WebApplication5.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set;}= null!;
        public DbSet<Group> Groups { get; set;} = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<UsersGroup> UsersGroups { get; set; } = null!;
        public DbSet<UserSettingsForGroup> UserSettingsForGroup {get; set;} = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<User>()
           .HasMany(u => u.UserGroups)
           .WithOne(ug => ug.User)
           .HasForeignKey(ug => ug.UserId);

           modelBuilder.Entity<Group>()
           .HasMany(g => g.UsersGroup)
           .WithOne(ug => ug.Group)
           .HasForeignKey(ug => ug.GroupId);

           modelBuilder.Entity<Group>()
           .HasMany(g => g.UserSettings)
           .WithOne(us => us.Group)
           .HasForeignKey(us => us.GroupId);
           
           modelBuilder.Entity<User>()
           .HasMany(u => u.GroupSettings)
           .WithOne(us => us.User)
           .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<Group>()
            .HasMany(g => g.Messages)
            .WithOne(m => m.CreatingGroup)
            .HasForeignKey(m => m.GroupId);
            modelBuilder.Entity<User>()
            .HasMany(u => u.Messages)
            .WithOne(m => m.Creator)
            .HasForeignKey(m => m.UserId);
        }
    }
}