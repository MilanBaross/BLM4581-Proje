using KampusPortali.Data;
using KampusPortali.Models;
using KampusPortali.Yardimcilar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Kampus
{
    public class YemekhaneModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public YemekhaneModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public DateOnly? Tarih { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Gorunum { get; set; }

        public bool KantinSekmesi => Gorunum == "kantin";

        public DateOnly Bugun { get; } = DateOnly.FromDateTime(TurkiyeSaati.Simdi());

        public DateOnly SeciliGun { get; set; }

        public DateOnly HaftaBasi { get; set; }

        public List<DateOnly> HaftaGunleri { get; set; } = new();

        public Dictionary<DateOnly, YemekMenusu> HaftalikMenu { get; set; } = new();

        public YemekMenusu? SeciliMenu { get; set; }

        public List<KantinBilgisi> Kantinler { get; set; } = new();

        public List<Mekan> YemekhaneBinalari { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (KantinSekmesi)
            {
                await KantinleriYukleAsync();
                return;
            }

            SeciliGun = Tarih ?? Bugun;
            HaftaBasi = SeciliGun.AddDays(-(((int)SeciliGun.DayOfWeek + 6) % 7));
            HaftaGunleri = Enumerable.Range(0, 5).Select(i => HaftaBasi.AddDays(i)).ToList();

            var haftaSonu = HaftaBasi.AddDays(6);
            HaftalikMenu = await _context.YemekMenuleri
                .AsNoTracking()
                .Where(m => m.Tarih >= HaftaBasi && m.Tarih <= haftaSonu)
                .ToDictionaryAsync(m => m.Tarih);

            SeciliMenu = HaftalikMenu.GetValueOrDefault(SeciliGun);

            YemekhaneBinalari = await _context.Mekanlar
                .AsNoTracking()
                .Where(m => m.Tur == MekanTuru.Yemekhane)
                .OrderBy(m => m.Ad)
                .ToListAsync();
        }

        private async Task KantinleriYukleAsync()
        {
            var mekanlar = await _context.Mekanlar
                .AsNoTracking()
                .Where(m => m.Tur == MekanTuru.Kantin)
                .Include(m => m.Urunler)
                .OrderBy(m => m.Ad)
                .ToListAsync();

            Kantinler = mekanlar
                .Select(m => new KantinBilgisi(
                    m,
                    m.Urunler
                        .OrderBy(u => u.Kategori)
                        .ThenBy(u => u.Ad)
                        .GroupBy(u => u.Kategori)
                        .ToList()))
                .ToList();
        }

        public record KantinBilgisi(Mekan Mekan, List<IGrouping<UrunKategorisi, KantinUrunu>> Kategoriler);
    }
}
