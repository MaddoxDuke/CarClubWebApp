using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarClubWebApp.Models
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<CarEvent> carEvents { get; set; }
        
    }
}
