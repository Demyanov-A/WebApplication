using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication.Domain.Entities;
using WebApplication.Domain.Entities.Identity;
using WebApplication.Domain.Entities.Order;

namespace WebApplication.DAL.Context
{
    public class WebApplicationDB : IdentityDbContext<User, Role, string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public WebApplicationDB(DbContextOptions<WebApplicationDB> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder db)
        {
            base.OnModelCreating(db);

            //db.Entity<Section>()
            //    .HasMany(section => section.Products)
            //    .WithOne(product => product.Section)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
