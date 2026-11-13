using KampusPortali.Data;
using KampusPortali.Models;
using KampusPortali.Yardimcilar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Kampus
{
    public class DerslikBulModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Kullanici> _userManager;

        public DerslikBulModel(ApplicationDbContext context, UserManager<Kullanici> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public string? Gorunum { get; set; }

        public List<DersKatalogu> Katalog { get; set; } = new();

        public List<DersKatalogu> SeciliDersler { get; set; } = new();

        public List<Ders> Dersler { get; set; } = new();

        public List<Sinav> Sinavlar { get; set; } = new();

        public List<BosDerslik> BosDerslikler { get; set; } = new();

        public List<DayOfWeek> Gunler { get; set; } = new();

        public Ders? SuAnkiDers { get; set; }

        public Ders? SiradakiDers { get; set; }

        public DateTime Simdi { get; } = TurkiyeSaati.Simdi();

        public DayOfWeek Bugun => Simdi.DayOfWeek;

        public TimeOnly SuAnkiSaat => TimeOnly.FromDateTime(Simdi);

        public TimeOnly CizelgeBaslangic { get; set; } = new(8, 0);

        public TimeOnly CizelgeBitis { get; set; } = new(18, 0);

        public bool BosDersliklerSekmesi => Gorunum == "bos";

        public string BolumAdi => "Bilgisayar Müh.";

        public async Task OnGetAsync()
        {
            if (BosDersliklerSekmesi)
            {
                await BosDerslikleriBulAsync();
                return;
            }

            Katalog = (await _context.Dersler
                .Select(d => new { d.Kod, d.DersAdi, d.Grup })
                .ToListAsync())
                .GroupBy(d => d.Kod)
                .Select(g => new DersKatalogu(g.Key, g.First().DersAdi, g.First().Grup))
                .OrderBy(d => d.Kod)
                .ToList();

            var kullaniciId = KullaniciId();
            var seciliKodlar = await _context.KullaniciDersleri
                .Where(k => k.KullaniciId == kullaniciId)
                .Select(k => k.DersKodu)
                .ToListAsync();

            SeciliDersler = Katalog.Where(d => seciliKodlar.Contains(d.Kod)).ToList();

            if (SeciliDersler.Count > 0)
            {
                await ProgramiYukleAsync(seciliKodlar);
            }
        }

        public async Task<IActionResult> OnPostKaydetAsync(List<string> kodlar)
        {
            var kullaniciId = KullaniciId();
            var gecerliKodlar = await _context.Dersler
                .Where(d => kodlar.Contains(d.Kod))
                .Select(d => d.Kod)
                .Distinct()
                .ToListAsync();

            var mevcutlar = await _context.KullaniciDersleri
                .Where(k => k.KullaniciId == kullaniciId)
                .ToListAsync();

            _context.KullaniciDersleri.RemoveRange(mevcutlar.Where(m => !gecerliKodlar.Contains(m.DersKodu)));

            foreach (var kod in gecerliKodlar.Where(k => mevcutlar.All(m => m.DersKodu != k)))
            {
                _context.KullaniciDersleri.Add(new KullaniciDersi { KullaniciId = kullaniciId, DersKodu = kod });
            }

            await _context.SaveChangesAsync();
            TempData["Bilgi"] = "Ders seçimin kaydedildi.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCikarAsync(string kod)
        {
            var kullaniciId = KullaniciId();
            var kayit = await _context.KullaniciDersleri
                .FirstOrDefaultAsync(k => k.KullaniciId == kullaniciId && k.DersKodu == kod);

            if (kayit != null)
            {
                _context.KullaniciDersleri.Remove(kayit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        private async Task ProgramiYukleAsync(List<string> kodlar)
        {
            Dersler = (await _context.Dersler
                .Include(d => d.Derslik).ThenInclude(dl => dl!.Mekan)
                .Where(d => kodlar.Contains(d.Kod))
                .ToListAsync())
                .OrderBy(d => GunSirasi(d.Gun))
                .ThenBy(d => d.BaslangicSaati)
                .ToList();

            Gunler = new List<DayOfWeek>
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday
            };
            foreach (var gun in Dersler.Select(d => d.Gun).Distinct())
            {
                if (!Gunler.Contains(gun))
                {
                    Gunler.Add(gun);
                }
            }
            Gunler = Gunler.OrderBy(GunSirasi).ToList();

            if (Dersler.Count > 0)
            {
                CizelgeBaslangic = new TimeOnly(Dersler.Min(d => d.BaslangicSaati).Hour, 0);
                var enGec = Dersler.Max(d => d.BitisSaati);
                CizelgeBitis = new TimeOnly(Math.Min(enGec.Minute > 0 ? enGec.Hour + 1 : enGec.Hour, 23), 0);
            }

            SuAnkiDers = Dersler.FirstOrDefault(d =>
                d.Gun == Bugun && d.BaslangicSaati <= SuAnkiSaat && SuAnkiSaat < d.BitisSaati);

            for (var gunFarki = 0; gunFarki < 7 && SiradakiDers == null; gunFarki++)
            {
                var gun = (DayOfWeek)(((int)Bugun + gunFarki) % 7);
                SiradakiDers = Dersler
                    .Where(d => d.Gun == gun && (gunFarki > 0 || d.BaslangicSaati > SuAnkiSaat))
                    .OrderBy(d => d.BaslangicSaati)
                    .FirstOrDefault();
            }

            Sinavlar = await _context.Sinavlar
                .Include(s => s.Derslik).ThenInclude(dl => dl!.Mekan)
                .Where(s => s.Tarih >= DateOnly.FromDateTime(Simdi) && kodlar.Contains(s.DersKodu))
                .OrderBy(s => s.Tarih)
                .ThenBy(s => s.BaslangicSaati)
                .ToListAsync();
        }

        private async Task BosDerslikleriBulAsync()
        {
            var derslikler = await _context.Derslikler
                .Include(d => d.Mekan)
                .Where(d => d.MekanId != null)
                .ToListAsync();

            var bugunkuDersler = await _context.Dersler
                .Where(d => d.Gun == Bugun && d.DerslikId != null)
                .ToListAsync();

            foreach (var derslik in derslikler)
            {
                var dersleri = bugunkuDersler.Where(d => d.DerslikId == derslik.Id).ToList();

                if (dersleri.Any(d => d.BaslangicSaati <= SuAnkiSaat && SuAnkiSaat < d.BitisSaati))
                {
                    continue;
                }

                var sonrakiDers = dersleri
                    .Where(d => d.BaslangicSaati > SuAnkiSaat)
                    .OrderBy(d => d.BaslangicSaati)
                    .FirstOrDefault();

                BosDerslikler.Add(new BosDerslik(derslik, sonrakiDers?.BaslangicSaati));
            }

            BosDerslikler = BosDerslikler
                .OrderBy(b => b.BosKadar.HasValue)
                .ThenByDescending(b => b.BosKadar)
                .ThenBy(b => b.Derslik.Ad)
                .ToList();
        }

        public double DikeyKonum(TimeOnly saat)
        {
            var toplam = (CizelgeBitis - CizelgeBaslangic).TotalMinutes;
            return Math.Round((saat - CizelgeBaslangic).TotalMinutes / toplam * 100, 3);
        }

        public static int GunSirasi(DayOfWeek gun) => ((int)gun + 6) % 7;

        private int KullaniciId() => int.Parse(_userManager.GetUserId(User)!);

        public record DersKatalogu(string Kod, string DersAdi, string Grup);

        public record BosDerslik(Derslik Derslik, TimeOnly? BosKadar);
    }
}
