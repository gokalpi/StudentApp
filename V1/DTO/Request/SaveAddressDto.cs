using System.ComponentModel.DataAnnotations;

namespace StudentApp.V1.DTO.Request
{
    public class SaveAddressDto
    {
        [Required]
        [MaxLength(255)]
        public string Street { get; set; }

        [Required]
        [MaxLength(255)]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        [MaxLength(255)]
        public string Country { get; set; }
    }
}