using System;
using AppDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProfile>()
                .Property(u => u.Name)
                .HasMaxLength(50);

            builder.Entity<Lesson>()
                .Property(u => u.Name)
                .HasMaxLength(50);

            builder.Entity<Group>()
                .Property(u => u.Name)
                .HasMaxLength(50);
        }
    }
}
