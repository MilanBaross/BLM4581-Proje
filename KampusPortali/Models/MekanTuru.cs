using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KampusPortali.Models
{
    public enum MekanTuru
    {
        [Display(Name = "Akademik")]
        Fakulte,

        [Display(Name = "Kütüphane")]
        Kutuphane,

        [Display(Name = "Yemekhane")]
        Yemekhane,

        [Display(Name = "Kantin")]
        Kantin,

        [Display(Name = "Ring Durağı")]
        RingDuragi,

        [Display(Name = "Yurt")]
        Yurt,

        [Display(Name = "Diğer")]
        Diger
    }

    public static class MekanTuruUzantilari
    {
        public static string GorunenAd(this MekanTuru tur) =>
            typeof(MekanTuru).GetMember(tur.ToString())[0]
                .GetCustomAttribute<DisplayAttribute>()?.Name ?? tur.ToString();

        public static string Ikon(this MekanTuru tur) => tur switch
        {
            MekanTuru.Fakulte => "bi-building",
            MekanTuru.Kutuphane => "bi-book",
            MekanTuru.Yemekhane => "bi-cup-hot",
            MekanTuru.Kantin => "bi-shop",
            MekanTuru.RingDuragi => "bi-bus-front",
            MekanTuru.Yurt => "bi-house-door",
            _ => "bi-geo-alt"
        };
    }
}
