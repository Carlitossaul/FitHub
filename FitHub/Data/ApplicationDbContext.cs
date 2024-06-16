using FitHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<Admin>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admin { get; set; }
    }
}
