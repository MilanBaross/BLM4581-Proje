using System.ComponentModel.DataAnnotations;

namespace KampusPortali.Models
{
    // Ders programındaki tek bir ders oturumu (gün + saat aralığı + derslik).
    public class Ders : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ders kodu zorunludur.")]
        [StringLength(20)]
        [RegularExpression(@"^[A-ZÇĞİÖŞÜ]{2,4}[0-9]{3,4}$", ErrorMessage = "Ders kodu 'BLM4581' biçiminde olmalıdır.")]
        [Display(Name = "Ders Kodu")]
        public string Kod { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ders adı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ders adı {2}-{1} karakter arasında olmalıdır.")]
        [Display(Name = "Ders Adı")]
        public string DersAdi { get; set; } = string.Empty;

        [Display(Name = "Laboratuvar mı?")]
        public bool LabMi { get; set; }

        [Required(ErrorMessage = "Grup zorunludur.")]
        [StringLength(50)]
        [Display(Name = "Grup")]
        public string Grup { get; set; } = string.Empty;

        [Display(Name = "Gün")]
        public DayOfWeek Gun { get; set; }

        [Required(ErrorMessage = "Başlangıç saati zorunludur.")]
        [Display(Name = "Başlangıç Saati")]
        public TimeOnly BaslangicSaati { get; set; }

        [Required(ErrorMessage = "Bitiş saati zorunludur.")]
        [Display(Name = "Bitiş Saati")]
        public TimeOnly BitisSaati { get; set; }

        [Display(Name = "Derslik")]
        public int? DerslikId { get; set; }
        public Derslik? Derslik { get; set; }

        [Display(Name = "Akademisyen")]
        public int? AkademisyenId { get; set; }
        public Akademisyen? Akademisyen { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BitisSaati <= BaslangicSaati)
            {
                yield return new ValidationResult(
                    "Bitiş saati başlangıç saatinden sonra olmalıdır.",
                    new[] { nameof(BitisSaati) });
            }
        }
    }
}
