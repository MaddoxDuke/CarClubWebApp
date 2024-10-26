using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Data
{
    public class ApplicationDbContext : DbContext // gives functionality for app. Db stands for Database. hence this is where you will store the stored info
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Race> Races { get; set; } // customary for the secondary term to be plural
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}

