using System.ComponentModel.DataAnnotations;
using KampusPortali.Models;

namespace KampusPortali.ViewModels
{
    public class SinavViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Ders seçiniz.")]
        [StringLength(20)]
        [RegularExpression(@"^[A-ZÇĞİÖŞÜ]{2,4}[0-9]{3,4}$", ErrorMessage = "Ders kodu 'BLM4581' biçiminde olmalıdır.")]
        [Display(Name = "Ders")]
        public string DersKodu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sınav türü seçiniz.")]
        [Display(Name = "Sınav Türü")]
        public SinavTuru? Tur { get; set; }

        [Required(ErrorMessage = "Tarih zorunludur.")]
        [Display(Name = "Tarih")]
        public DateOnly? Tarih { get; set; }

        [Required(ErrorMessage = "Başlangıç saati zorunludur.")]
        [Display(Name = "Başlangıç Saati")]
        public TimeOnly? BaslangicSaati { get; set; }

        [Required(ErrorMessage = "Bitiş saati zorunludur.")]
        [Display(Name = "Bitiş Saati")]
        public TimeOnly? BitisSaati { get; set; }

        [Display(Name = "Derslikler")]
        public List<int> DerslikIdleri { get; set; } = new();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DerslikIdleri.Count == 0)
            {
                yield return new ValidationResult("En az bir derslik seçiniz.", new[] { nameof(DerslikIdleri) });
            }

            if (BaslangicSaati.HasValue && BitisSaati.HasValue && BitisSaati <= BaslangicSaati)
            {
                yield return new ValidationResult(
                    "Bitiş saati başlangıç saatinden sonra olmalıdır.",
                    new[] { nameof(BitisSaati) });
            }
        }
    }
}
