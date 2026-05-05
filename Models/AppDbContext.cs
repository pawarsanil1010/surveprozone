using Microsoft.EntityFrameworkCore;

namespace SurveProzone.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactForm> ContactForms { get; set; }

        public DbSet<AdminUser> AdminUsers { get; set; }

        public DbSet<SiteContent> SiteContents { get; set; }
    }
}