using System.ComponentModel.DataAnnotations;

namespace KampusPortali.Models
{
    public class Mekan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mekan adı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Mekan adı {2}-{1} karakter arasında olmalıdır.")]
        [Display(Name = "Mekan Adı")]
        public string Ad { get; set; } = string.Empty;

        [Display(Name = "Tür")]
        public MekanTuru Tur { get; set; }

        [Range(-90, 90, ErrorMessage = "Enlem {1} ile {2} arasında olmalıdır.")]
        [Display(Name = "Enlem")]
        public double Enlem { get; set; }

        [Range(-180, 180, ErrorMessage = "Boylam {1} ile {2} arasında olmalıdır.")]
        [Display(Name = "Boylam")]
        public double Boylam { get; set; }

        [StringLength(500, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }
    }
}
