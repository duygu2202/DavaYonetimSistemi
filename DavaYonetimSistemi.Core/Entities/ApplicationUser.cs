using Microsoft.AspNetCore.Identity;

namespace DavaYonetimDB.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
} 