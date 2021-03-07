using System;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<ItemName> ItemNames { get; set; } = null!;
        public DbSet<Job> Jobs { get; set; } = null!;
        public DbSet<JobName> JobNames { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<JobItem> JobItems { get; set; } = null!;
        public DbSet<Rally> Rallies { get; set; } = null!;
        
        public DbSet<JobPerformed> JobsPerformed { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            /*foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .Where(e => !e.IsOwned())
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;*/
            
                      
            modelBuilder.Entity<Job>().HasMany(job => job.JobItems).WithOne(item => item.Job)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Rally>().HasMany(item => item.JobsPerformed).WithOne(item => item.Rally)
                .OnDelete(DeleteBehavior.Restrict);
            

        }
        

    }
}