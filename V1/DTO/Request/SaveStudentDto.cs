using System.ComponentModel.DataAnnotations;

namespace StudentApp.V1.DTO.Request
{
    public class SaveStudentDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string BloodGroup { get; set; }

        public SaveAddressDto Address { get; set; }
    }
}