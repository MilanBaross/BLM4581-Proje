using KampusPortali.Data;
using KampusPortali.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Kampus
{
    public class RezervasyonlarimModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Kullanici> _userManager;

        public RezervasyonlarimModel(ApplicationDbContext context, UserManager<Kullanici> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<MasaRezervasyonu> Yaklasanlar { get; set; } = new();

        public List<MasaRezervasyonu> Gecmisler { get; set; } = new();

        public DateTime SimdiUtc { get; } = DateTime.UtcNow;

        public async Task OnGetAsync()
        {
            var kullaniciId = KullaniciId();

            Yaklasanlar = await _context.MasaRezervasyonlari
                .Include(r => r.Masa)
                .Where(r => r.KullaniciId == kullaniciId && !r.IptalEdildiMi && r.Bitis > SimdiUtc)
                .OrderBy(r => r.Baslangic)
                .ToListAsync();

            Gecmisler = await _context.MasaRezervasyonlari
                .Include(r => r.Masa)
                .Where(r => r.KullaniciId == kullaniciId && (r.IptalEdildiMi || r.Bitis <= SimdiUtc))
                .OrderByDescending(r => r.Baslangic)
                .Take(20)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostIptalAsync(int id)
        {
            var rezervasyon = await KendiRezervasyonunuBulAsync(id);
            if (rezervasyon == null || rezervasyon.Baslangic <= SimdiUtc)
            {
                TempData["Bilgi"] = "Bu rezervasyon iptal edilemez.";
                return RedirectToPage();
            }

            rezervasyon.IptalEdildiMi = true;
            await _context.SaveChangesAsync();

            TempData["Bilgi"] = $"{rezervasyon.Masa?.MasaNo} numaralı masa rezervasyonu iptal edildi.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostBitirAsync(int id)
        {
            var rezervasyon = await KendiRezervasyonunuBulAsync(id);
            if (rezervasyon == null || rezervasyon.Baslangic > SimdiUtc || rezervasyon.Bitis <= SimdiUtc)
            {
                TempData["Bilgi"] = "Sadece devam eden bir rezervasyon bitirilebilir.";
                return RedirectToPage();
            }

            rezervasyon.Bitis = SimdiUtc;
            await _context.SaveChangesAsync();

            TempData["Bilgi"] = $"{rezervasyon.Masa?.MasaNo} numaralı masa boşaltıldı.";
            return RedirectToPage();
        }

        private async Task<MasaRezervasyonu?> KendiRezervasyonunuBulAsync(int id)
        {
            var kullaniciId = KullaniciId();
            return await _context.MasaRezervasyonlari
                .Include(r => r.Masa)
                .FirstOrDefaultAsync(r => r.Id == id && r.KullaniciId == kullaniciId && !r.IptalEdildiMi);
        }

        private int KullaniciId() => int.Parse(_userManager.GetUserId(User)!);
    }
}
