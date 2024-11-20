using System.ComponentModel.DataAnnotations;

namespace CarClubWebApp.Models
{
    public class Address //Notice the model is similar to the database table.
    {
        [Key] 
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    
}
