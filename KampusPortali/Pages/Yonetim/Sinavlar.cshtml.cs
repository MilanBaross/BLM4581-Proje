using KampusPortali.Data;
using KampusPortali.Models;
using KampusPortali.Yardimcilar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Yonetim
{
    public class SinavlarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SinavlarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SinavGrubu> Yaklasanlar { get; set; } = new();

        public List<SinavGrubu> Gecmisler { get; set; } = new();

        public Dictionary<string, string> DersAdlari { get; set; } = new();

        public async Task OnGetAsync()
        {
            var bugun = DateOnly.FromDateTime(TurkiyeSaati.Simdi());

            var sinavlar = await _context.Sinavlar
                .Include(s => s.Derslik).ThenInclude(d => d!.Mekan)
                .ToListAsync();

            var gruplar = sinavlar
                .GroupBy(s => new { s.DersKodu, s.Tur, s.Tarih, s.BaslangicSaati, s.BitisSaati })
                .Select(g => new SinavGrubu(
                    g.OrderBy(s => s.Id).First(),
                    g.Where(s => s.Derslik != null).Select(s => s.Derslik!).OrderBy(d => d.Ad).ToList()))
                .ToList();

            Yaklasanlar = gruplar
                .Where(g => g.Sinav.Tarih >= bugun)
                .OrderBy(g => g.Sinav.Tarih).ThenBy(g => g.Sinav.BaslangicSaati)
                .ToList();

            Gecmisler = gruplar
                .Where(g => g.Sinav.Tarih < bugun)
                .OrderByDescending(g => g.Sinav.Tarih).ThenBy(g => g.Sinav.BaslangicSaati)
                .ToList();

            DersAdlari = (await _context.Dersler
                .Select(d => new { d.Kod, d.DersAdi })
                .ToListAsync())
                .GroupBy(d => d.Kod)
                .ToDictionary(g => g.Key, g => g.First().DersAdi);
        }

        public async Task<IActionResult> OnPostSilAsync(int id)
        {
            var sinav = await _context.Sinavlar.FindAsync(id);
            if (sinav == null)
            {
                return NotFound();
            }

            var grup = await _context.Sinavlar
                .Where(s => s.DersKodu == sinav.DersKodu && s.Tur == sinav.Tur && s.Tarih == sinav.Tarih &&
                    s.BaslangicSaati == sinav.BaslangicSaati && s.BitisSaati == sinav.BitisSaati)
                .ToListAsync();

            _context.Sinavlar.RemoveRange(grup);
            await _context.SaveChangesAsync();

            TempData["Bilgi"] = $"{sinav.DersKodu} {sinav.Tur.GorunenAd()} sınavı silindi.";
            return RedirectToPage();
        }

        public record SinavGrubu(Sinav Sinav, List<Derslik> Derslikler);
    }
}
