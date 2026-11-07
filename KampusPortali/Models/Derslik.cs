using System.ComponentModel.DataAnnotations;

namespace KampusPortali.Models
{
    public class Derslik
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Derslik adı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Derslik adı {2}-{1} karakter arasında olmalıdır.")]
        [Display(Name = "Derslik Adı")]
        public string Ad { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Blok")]
        public string? Blok { get; set; }

        [StringLength(10)]
        [Display(Name = "Kat")]
        public string? Kat { get; set; }

        [Range(1, 1000, ErrorMessage = "Kapasite {1} ile {2} arasında olmalıdır.")]
        [Display(Name = "Kapasite")]
        public int? Kapasite { get; set; }

        [Display(Name = "Müsait mi?")]
        public bool MusaitMi { get; set; } = true;

        [StringLength(200)]
        [Display(Name = "Not")]
        public string? Not { get; set; }

        [Display(Name = "Bina")]
        public int? MekanId { get; set; }
        public Mekan? Mekan { get; set; }

        public ICollection<Ders> Dersler { get; set; } = new List<Ders>();
        public ICollection<Sinav> Sinavlar { get; set; } = new List<Sinav>();
    }
}
