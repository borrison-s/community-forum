using ForumApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Thread = ForumApp.Domain.Models.Thread;

namespace ForumApp.DataAccess
{
    public class ForumAppDbContext : DbContext
    {
        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options) : base(options)
        {

        }

        // DbSets for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }


        // OnModelCreating to configure relationships and other settings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Posts relationship (restrict cascading delete on Posts)
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)  // A post belongs to one user
                .WithMany(u => u.Posts)  // A user can have many posts
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete for posts when a user is deleted

            // Configure Thread-Posts relationship (cascade delete for posts when a thread is deleted)
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Thread)  // A post belongs to one thread
                .WithMany(t => t.Posts)  // A thread can have many posts
                .HasForeignKey(p => p.ThreadId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascading delete for posts when a thread is deleted

            // Configure Thread-Category relationship (cascade delete for threads when a category is deleted)
            modelBuilder.Entity<Thread>()
                .HasOne(t => t.Category)  // A thread belongs to one category
                .WithMany(c => c.Threads)  // A category can have many threads
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascading delete for threads when a category is deleted


            // Property-specific configurations
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(100)  // Set max length for username
                .IsRequired();  // Make username required

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255)  // Set max length for email
                .IsRequired();  // Make email required

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(150);  // Set max length for Name (optional)

            modelBuilder.Entity<User>()
                .Property(x => x.Password) //column
                .IsRequired();

            modelBuilder.Entity<Thread>()
                .Property(t => t.Title)
                .HasMaxLength(200)  // Set max length for thread title
                .IsRequired();  // Make title required

            modelBuilder.Entity<Thread>()
                .Property(t => t.Description)
                .HasMaxLength(500);  // Set max length for description

            modelBuilder.Entity<Post>()
                .Property(p => p.Content)
                .HasMaxLength(2000)  // Set max length for post content
                .IsRequired();  // Make content required

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(100)  // Set max length for category name
                .IsRequired();  // Make category name required

            // Ensure unique UserName index
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();


        }

    }
}
