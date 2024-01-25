using System.ComponentModel.DataAnnotations;

namespace Core.Entites.Identity
{
    public class Address
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }
       
        public string State { get; set; }

        [Required]
        public string AppUserId { get; set; }
    }
}