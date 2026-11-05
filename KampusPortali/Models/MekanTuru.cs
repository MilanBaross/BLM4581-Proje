using System.ComponentModel.DataAnnotations;

namespace KampusPortali.Models
{
    public enum MekanTuru
    {
        [Display(Name = "Fakülte")]
        Fakulte,

        [Display(Name = "Kütüphane")]
        Kutuphane,

        [Display(Name = "Yemekhane")]
        Yemekhane,

        [Display(Name = "Kantin")]
        Kantin,

        [Display(Name = "Ring Durağı")]
        RingDuragi,

        [Display(Name = "Diğer")]
        Diger
    }
}
