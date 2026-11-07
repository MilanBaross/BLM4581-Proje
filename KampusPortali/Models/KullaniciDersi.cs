using System.ComponentModel.DataAnnotations;

namespace KampusPortali.Models
{
    public class KullaniciDersi
    {
        public int Id { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-ZÇĞİÖŞÜ]{2,4}[0-9]{3,4}$")]
        [Display(Name = "Ders Kodu")]
        public string DersKodu { get; set; } = string.Empty;
    }
}
