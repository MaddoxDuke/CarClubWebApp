using Microsoft.EntityFrameworkCore;
using CarClubWebApp.Models;

namespace CarClubWebApp.Data
{
    public class ApplicationDbContext : DbContext // gives functionality for app. Db stands for Database. hence this is where you will store the stored info
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //Db tables
        public DbSet<CarEvent> carEvents { get; set; } // customary for the secondary term to be plural
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}

