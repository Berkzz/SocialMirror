using Microsoft.EntityFrameworkCore;
using SocialRofl.Models.Database;

namespace SocialRofl.Data
{
    public class DataContext : DbContext
    {
        IConfiguration _config;

        public DataContext(IConfiguration config)
        {
            _config = config;
        }

        public DataContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .HasIndex(b => b.Hash)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasOne(p => p.MainPhoto)
                .WithMany();
            modelBuilder.Entity<User>()
                .HasMany(p => p.Following)
                .WithMany(c => c.SubscribedTo)
                .UsingEntity<Follower>(
                    x => x.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId),
                    x => x.HasOne(p => p.SubscribedTo).WithMany().HasForeignKey(p => p.SubscribedToId))
                .ToTable("Followers");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("Default"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}
