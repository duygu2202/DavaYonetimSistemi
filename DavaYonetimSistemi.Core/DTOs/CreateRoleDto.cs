using System.ComponentModel.DataAnnotations;

namespace DavaYonetimDB.Core.DTOs
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Rol adı zorunludur")]
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
    }
} 