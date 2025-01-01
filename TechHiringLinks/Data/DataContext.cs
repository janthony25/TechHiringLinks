using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechHiringLinks.Models;

namespace TechHiringLinks.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Link> Links { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public DbSet<LinkApplicationStatus> LinkApplicationStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Many-to-Many Relationship ( Link - ApplicationStatus )
            builder.Entity<LinkApplicationStatus>()
                .HasKey(ls => new { ls.LinkId, ls.ApplicationStatusId });

            builder.Entity<LinkApplicationStatus>()
                .HasOne(ls => ls.Link)
                .WithMany(link => link.LinkApplicationStatus)
                .HasForeignKey(ls => ls.LinkId);

            builder.Entity<LinkApplicationStatus>()
                .HasOne(ls => ls.ApplicationStatus)
                .WithMany(appStatus => appStatus.LinkApplicationStatus)
                .HasForeignKey(ls => ls.ApplicationStatusId);

            base.OnModelCreating(builder);
        }
    }
}
