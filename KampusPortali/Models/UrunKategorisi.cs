using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KampusPortali.Models
{
    public enum UrunKategorisi
    {
        [Display(Name = "Sıcak İçecekler")]
        SicakIcecek,

        [Display(Name = "Soğuk İçecekler")]
        SogukIcecek,

        [Display(Name = "Yiyecekler")]
        Yiyecek,

        [Display(Name = "Atıştırmalıklar")]
        Atistirmalik,

        [Display(Name = "Tatlılar")]
        Tatli
    }

    public static class UrunKategorisiUzantilari
    {
        public static string GorunenAd(this UrunKategorisi kategori) =>
            typeof(UrunKategorisi).GetMember(kategori.ToString())[0]
                .GetCustomAttribute<DisplayAttribute>()?.Name ?? kategori.ToString();

        public static string Ikon(this UrunKategorisi kategori) => kategori switch
        {
            UrunKategorisi.SicakIcecek => "bi-cup-hot",
            UrunKategorisi.SogukIcecek => "bi-cup-straw",
            UrunKategorisi.Yiyecek => "bi-egg-fried",
            UrunKategorisi.Atistirmalik => "bi-basket",
            UrunKategorisi.Tatli => "bi-cake2",
            _ => "bi-tag"
        };
    }
}
