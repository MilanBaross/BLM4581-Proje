using KampusPortali.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Yonetim
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Kullanici> _userManager;

        public IndexModel(UserManager<Kullanici> userManager)
        {
            _userManager = userManager;
        }

        public List<KullaniciSatiri> Kullanicilar { get; set; } = new();

        public async Task OnGetAsync()
        {
            var kullanicilar = await _userManager.Users
                .OrderBy(k => k.KayitTarihi)
                .ToListAsync();

            foreach (var k in kullanicilar)
            {
                var roller = await _userManager.GetRolesAsync(k);
                Kullanicilar.Add(new KullaniciSatiri(
                    $"{k.Ad} {k.Soyad}", k.Email, k.OgrenciNo, string.Join(", ", roller), k.KayitTarihi));
            }
        }

        public record KullaniciSatiri(string AdSoyad, string? Email, string? OgrenciNo, string Roller, DateTime KayitTarihi);
    }
}
