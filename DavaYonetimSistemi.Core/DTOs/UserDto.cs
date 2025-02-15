namespace DavaYonetimDB.Core.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 