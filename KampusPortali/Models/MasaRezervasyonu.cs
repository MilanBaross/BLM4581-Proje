using System.ComponentModel.DataAnnotations;
using KampusPortali.Yardimcilar;

namespace KampusPortali.Models
{
    public class MasaRezervasyonu : IValidatableObject
    {
        public int Id { get; set; }

        [Display(Name = "Masa")]
        public int MasaId { get; set; }
        public KutuphaneMasasi? Masa { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

        [Display(Name = "Başlangıç")]
        public DateTime Baslangic { get; set; }

        [Display(Name = "Bitiş")]
        public DateTime Bitis { get; set; }

        [Display(Name = "İptal Edildi mi?")]
        public bool IptalEdildiMi { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Bitis <= Baslangic)
            {
                yield return new ValidationResult("Bitiş zamanı başlangıçtan sonra olmalıdır.", new[] { nameof(Bitis) });
            }
            else if (Bitis - Baslangic > KutuphaneKurallari.TekSeferdeEnFazlaSure)
            {
                yield return new ValidationResult(
                    $"Bir rezervasyon en fazla {KutuphaneKurallari.TekSeferdeEnFazlaSure.TotalHours} saat olabilir.", new[] { nameof(Bitis) });
            }
        }
    }
}
