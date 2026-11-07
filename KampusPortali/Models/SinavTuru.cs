using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KampusPortali.Models
{
    public enum SinavTuru
    {
        [Display(Name = "Vize")]
        Vize,

        [Display(Name = "Final")]
        Final,

        [Display(Name = "Bütünleme")]
        Butunleme,

        [Display(Name = "Mazeret")]
        Mazeret
    }

    public static class SinavTuruUzantilari
    {
        public static string GorunenAd(this SinavTuru tur) =>
            typeof(SinavTuru).GetMember(tur.ToString())[0]
                .GetCustomAttribute<DisplayAttribute>()?.Name ?? tur.ToString();
    }
}
