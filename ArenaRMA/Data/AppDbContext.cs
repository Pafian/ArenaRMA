using ArenaRMA.Models;
using Microsoft.EntityFrameworkCore;

namespace ArenaRMA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Email> Emails { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Dropdown> Dropdowns { get; set; }
        public DbSet<DropdownOption> DropdownOptions { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Warranty> Warranties { get; set; }

        public DbSet<Customer> Customers { get; set; }





    }
}
