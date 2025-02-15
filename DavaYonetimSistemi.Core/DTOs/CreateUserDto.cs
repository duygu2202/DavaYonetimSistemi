using System.ComponentModel.DataAnnotations;

namespace DavaYonetimDB.Core.DTOs
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ad zorunludur")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        public List<string> SelectedRoles { get; set; } = new List<string>();
    }
} 