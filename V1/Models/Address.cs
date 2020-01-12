using System.ComponentModel.DataAnnotations;

namespace StudentApp.V1.Models
{
    public class Address
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
    }
}