using System.ComponentModel.DataAnnotations;

namespace KampusPortali.ViewModels
{
    public class GirisViewModel
    {
        [Required(ErrorMessage = "Öğrenci numarası veya e-posta zorunludur.")]
        [StringLength(100, ErrorMessage = "En fazla {1} karakter girilebilir.")]
        [Display(Name = "Öğrenci No veya E-posta")]
        public string KullaniciAdiVeyaEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; } = string.Empty;

        [Display(Name = "Beni Hatırla")]
        public bool BeniHatirla { get; set; }
    }
}
