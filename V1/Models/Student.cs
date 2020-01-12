using System.ComponentModel.DataAnnotations;

namespace StudentApp.V1.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string BloodGroup { get; set; }

        public Address Address { get; set; }
    }
}