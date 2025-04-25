using CodeBase.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeBase.EntityFrameworkCore
{
    public class CodebBaseDbContext : DbContext
    {

        public CodebBaseDbContext(DbContextOptions<CodebBaseDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets (tables) here
        // public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<ExampleEntity> ExampleEntities { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentCode).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(200);

                // Add indexes
                entity.HasIndex(e => e.StudentCode).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Phone);
            });
        }
    }
}
