using KampusPortali.Data;
using KampusPortali.Models;
using KampusPortali.ViewModels;
using KampusPortali.Yardimcilar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Yonetim
{
    public class SinavFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SinavFormModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SinavViewModel Girdi { get; set; } = new();

        public int? Id { get; set; }

        public bool DuzenlemeMi => Id.HasValue;

        public List<SelectListItem> DersSecenekleri { get; set; } = new();

        public List<SelectListItem> TurSecenekleri { get; set; } = new();

        public List<Derslik> Derslikler { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Id = id;

            if (id != null)
            {
                var grup = await SinavGrubunuBulAsync(id.Value);
                if (grup.Count == 0)
                {
                    return NotFound();
                }

                var sinav = grup[0];
                Girdi = new SinavViewModel
                {
                    DersKodu = sinav.DersKodu,
                    Tur = sinav.Tur,
                    Tarih = sinav.Tarih,
                    BaslangicSaati = sinav.BaslangicSaati,
                    BitisSaati = sinav.BitisSaati,
                    DerslikIdleri = grup.Where(s => s.DerslikId != null).Select(s => s.DerslikId!.Value).ToList()
                };
            }
            else
            {
                Girdi.Tarih = DateOnly.FromDateTime(TurkiyeSaati.Simdi());
            }

            await SecenekleriYukleAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Id = id;

            var eskiGrup = new List<Sinav>();
            if (id != null)
            {
                eskiGrup = await SinavGrubunuBulAsync(id.Value);
                if (eskiGrup.Count == 0)
                {
                    return NotFound();
                }
            }

            if (!string.IsNullOrEmpty(Girdi.DersKodu) &&
                !await _context.Dersler.AnyAsync(d => d.Kod == Girdi.DersKodu))
            {
                ModelState.AddModelError("Girdi.DersKodu", "Ders programında böyle bir ders yok.");
            }

            var seciliDerslikler = Girdi.DerslikIdleri.Distinct().ToList();
            var bulunanDerslikSayisi = await _context.Derslikler.CountAsync(d => seciliDerslikler.Contains(d.Id));
            if (bulunanDerslikSayisi != seciliDerslikler.Count)
            {
                ModelState.AddModelError("Girdi.DerslikIdleri", "Seçilen dersliklerden biri bulunamadı.");
            }

            if (!ModelState.IsValid)
            {
                await SecenekleriYukleAsync();
                return Page();
            }

            _context.Sinavlar.RemoveRange(eskiGrup);

            foreach (var derslikId in seciliDerslikler)
            {
                _context.Sinavlar.Add(new Sinav
                {
                    DersKodu = Girdi.DersKodu,
                    Tur = Girdi.Tur!.Value,
                    Tarih = Girdi.Tarih!.Value,
                    BaslangicSaati = Girdi.BaslangicSaati!.Value,
                    BitisSaati = Girdi.BitisSaati!.Value,
                    DerslikId = derslikId
                });
            }

            await _context.SaveChangesAsync();

            TempData["Bilgi"] = $"{Girdi.DersKodu} {Girdi.Tur!.Value.GorunenAd()} sınavı kaydedildi.";
            return RedirectToPage("/Yonetim/Sinavlar");
        }

        private async Task<List<Sinav>> SinavGrubunuBulAsync(int id)
        {
            var sinav = await _context.Sinavlar.FindAsync(id);
            if (sinav == null)
            {
                return new List<Sinav>();
            }

            return await _context.Sinavlar
                .Where(s => s.DersKodu == sinav.DersKodu && s.Tur == sinav.Tur && s.Tarih == sinav.Tarih &&
                    s.BaslangicSaati == sinav.BaslangicSaati && s.BitisSaati == sinav.BitisSaati)
                .ToListAsync();
        }

        private async Task SecenekleriYukleAsync()
        {
            DersSecenekleri = (await _context.Dersler
                .Select(d => new { d.Kod, d.DersAdi })
                .ToListAsync())
                .GroupBy(d => d.Kod)
                .OrderBy(g => g.Key)
                .Select(g => new SelectListItem(
                    g.First().DersAdi == g.Key ? g.Key : $"{g.Key} · {g.First().DersAdi}", g.Key))
                .ToList();

            Derslikler = await _context.Derslikler
                .Include(d => d.Mekan)
                .OrderBy(d => d.Mekan == null)
                .ThenBy(d => d.Mekan!.Ad)
                .ThenBy(d => d.Ad)
                .ToListAsync();

            TurSecenekleri = Enum.GetValues<SinavTuru>()
                .Select(t => new SelectListItem(t.GorunenAd(), t.ToString()))
                .ToList();
        }
    }
}
