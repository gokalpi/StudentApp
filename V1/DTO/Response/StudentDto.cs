namespace StudentApp.V1.DTO.Response
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public AddressDto Address { get; set; }
    }
}