using System.Data;
using System.Data.Common;
using KampusPortali.Data;
using KampusPortali.Models;
using KampusPortali.Yardimcilar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Kampus
{
    public class KutuphaneModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Kullanici> _userManager;

        public KutuphaneModel(ApplicationDbContext context, UserManager<Kullanici> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public DateOnly? Tarih { get; set; }

        [BindProperty(SupportsGet = true)]
        public TimeOnly? Baslangic { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Sure { get; set; }

        public TimeOnly? Bitis => Baslangic?.AddMinutes(Sure ?? 0);

        public List<KutuphaneMasasi> Masalar { get; set; } = new();

        public HashSet<int> DoluMasalar { get; set; } = new();

        public List<KatDolulugu> Katlar { get; set; } = new();

        public int ToplamMasa { get; set; }

        public int SimdiDoluMasa { get; set; }

        public int AraliktaDoluMasa => AralikHatasi == null ? DoluMasalar.Count : SimdiDoluMasa;

        public int DolulukYuzdesi => ToplamMasa == 0 ? 0 : (int)Math.Round(100.0 * AraliktaDoluMasa / ToplamMasa);

        public bool KutuphaneAcik { get; set; }

        public string? AralikHatasi { get; set; }

        public MasaRezervasyonu? CakisanRezervasyonum { get; set; }

        public List<DateOnly> Gunler { get; set; } = new();

        public List<TimeOnly> BaslangicSecenekleri { get; set; } = new();

        public List<int> SureSecenekleri { get; set; } = new();

        public TimeSpan GunlukKullanilan { get; set; }

        public TimeSpan GunlukKalan => KutuphaneKurallari.GunlukEnFazlaSure - GunlukKullanilan;

        public DateTime Simdi { get; } = TurkiyeSaati.Simdi();

        public DateOnly Bugun => DateOnly.FromDateTime(Simdi);

        public async Task OnGetAsync()
        {
            VarsayilanlariAyarla();
            await YukleAsync();
        }

        public async Task<IActionResult> OnPostRezerveEtAsync(int masaId)
        {
            VarsayilanlariAyarla();

            var hata = AraligiDenetle();
            if (hata != null)
            {
                ModelState.AddModelError(string.Empty, hata);
                await YukleAsync();
                return Page();
            }

            var masa = await _context.KutuphaneMasalari.FindAsync(masaId);
            if (masa == null)
            {
                ModelState.AddModelError(string.Empty, "Lütfen bir masa seçin.");
                await YukleAsync();
                return Page();
            }

            var kullaniciId = KullaniciId();
            var (baslangicUtc, bitisUtc) = AraligiUtcAl();

            var rezervasyon = new MasaRezervasyonu
            {
                MasaId = masa.Id,
                KullaniciId = kullaniciId,
                Baslangic = baslangicUtc,
                Bitis = bitisUtc
            };

            var basarili = false;
            try
            {
                await using (var islem = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
                {
                    var masaDolu = await _context.MasaRezervasyonlari.AnyAsync(r =>
                        r.MasaId == masa.Id && !r.IptalEdildiMi &&
                        r.Baslangic < bitisUtc && r.Bitis > baslangicUtc);

                    if (masaDolu)
                    {
                        ModelState.AddModelError(string.Empty, $"{masa.MasaNo} numaralı masa bu saat aralığında dolu. Başka bir masa seçin.");
                    }

                    var kullaniciMesgul = await _context.MasaRezervasyonlari.AnyAsync(r =>
                        r.KullaniciId == kullaniciId && !r.IptalEdildiMi &&
                        r.Baslangic < bitisUtc && r.Bitis > baslangicUtc);

                    if (kullaniciMesgul)
                    {
                        ModelState.AddModelError(string.Empty, "Bu saat aralığında zaten bir rezervasyonun var. Aynı anda birden fazla masa ayrılamaz.");
                    }

                    var gunlukKullanilan = await GunlukKullanilanSureAsync(kullaniciId);
                    if (gunlukKullanilan + (bitisUtc - baslangicUtc) > KutuphaneKurallari.GunlukEnFazlaSure)
                    {
                        var kalan = KutuphaneKurallari.GunlukEnFazlaSure - gunlukKullanilan;
                        ModelState.AddModelError(string.Empty,
                            $"Bir günde en fazla {SureMetni(KutuphaneKurallari.GunlukEnFazlaSure)} rezervasyon yapılabilir. Bu gün için kalan hakkın: {SureMetni(kalan)}.");
                    }

                    if (ModelState.IsValid)
                    {
                        _context.MasaRezervasyonlari.Add(rezervasyon);
                        await _context.SaveChangesAsync();
                        await islem.CommitAsync();
                        basarili = true;
                    }
                }
            }
            catch (Exception ex) when (ex is DbUpdateException or DbException)
            {
                _context.ChangeTracker.Clear();
                ModelState.AddModelError(string.Empty, "Bu masa az önce başka biri tarafından ayrıldı. Lütfen tekrar deneyin.");
            }

            if (!basarili)
            {
                await YukleAsync();
                return Page();
            }

            TempData["Bilgi"] = $"{masa.MasaNo} numaralı masa {Tarih:dd.MM.yyyy} {Baslangic:HH:mm}-{Bitis:HH:mm} için ayrıldı.";
            return RedirectToPage("/Kampus/Rezervasyonlarim");
        }

        private void VarsayilanlariAyarla()
        {
            var suAnkiAdim = KutuphaneKurallari.AdimaYuvarla(TimeOnly.FromDateTime(Simdi));
            var sonBaslangic = KutuphaneKurallari.Kapanis.AddMinutes(-KutuphaneKurallari.AdimDakika);

            DateOnly ilkGun;
            TimeOnly ilkSaat;
            if (suAnkiAdim >= sonBaslangic)
            {
                ilkGun = Bugun.AddDays(1);
                ilkSaat = KutuphaneKurallari.Acilis;
            }
            else
            {
                ilkGun = Bugun;
                ilkSaat = suAnkiAdim < KutuphaneKurallari.Acilis ? KutuphaneKurallari.Acilis : suAnkiAdim;
            }

            Tarih ??= ilkGun;
            Baslangic ??= Tarih == ilkGun ? ilkSaat : KutuphaneKurallari.Acilis;

            if (Sure == null)
            {
                var kapanisaKalan = (int)(KutuphaneKurallari.Kapanis - Baslangic.Value).TotalMinutes;
                Sure = Math.Clamp(kapanisaKalan, KutuphaneKurallari.AdimDakika, 60);
            }

            for (var gun = ilkGun; gun <= Bugun.AddDays(KutuphaneKurallari.EnFazlaGunIleri); gun = gun.AddDays(1))
            {
                Gunler.Add(gun);
            }

            for (var saat = KutuphaneKurallari.Acilis; saat < KutuphaneKurallari.Kapanis; saat = saat.AddMinutes(KutuphaneKurallari.AdimDakika))
            {
                BaslangicSecenekleri.Add(saat);
            }

            for (var dakika = KutuphaneKurallari.AdimDakika; dakika <= KutuphaneKurallari.TekSeferdeEnFazlaSure.TotalMinutes; dakika += KutuphaneKurallari.AdimDakika)
            {
                SureSecenekleri.Add(dakika);
            }
        }

        private string? AraligiDenetle()
        {
            var tarih = Tarih!.Value;
            var baslangic = Baslangic!.Value;
            var sure = Sure!.Value;

            if (tarih < Bugun || tarih > Bugun.AddDays(KutuphaneKurallari.EnFazlaGunIleri))
            {
                return $"Rezervasyon bugünden itibaren en fazla {KutuphaneKurallari.EnFazlaGunIleri} gün sonrası için yapılabilir.";
            }

            if (!KutuphaneKurallari.AdimaUygunMu(baslangic) || sure % KutuphaneKurallari.AdimDakika != 0)
            {
                return $"Başlangıç saati ve süre {KutuphaneKurallari.AdimDakika} dakikalık aralıklarla seçilmelidir.";
            }

            if (sure <= 0 || sure > KutuphaneKurallari.TekSeferdeEnFazlaSure.TotalMinutes)
            {
                return $"Bir rezervasyon en az {KutuphaneKurallari.AdimDakika} dakika, en fazla {SureMetni(KutuphaneKurallari.TekSeferdeEnFazlaSure)} olabilir.";
            }

            if (baslangic < KutuphaneKurallari.Acilis ||
                baslangic.ToTimeSpan() + TimeSpan.FromMinutes(sure) > KutuphaneKurallari.Kapanis.ToTimeSpan())
            {
                return $"Kütüphane {KutuphaneKurallari.Acilis:HH:mm}-{KutuphaneKurallari.Kapanis:HH:mm} arasında açıktır; seçilen süre kapanış saatini aşıyor.";
            }

            if (tarih == Bugun && baslangic < KutuphaneKurallari.AdimaYuvarla(TimeOnly.FromDateTime(Simdi)))
            {
                return "Geçmiş bir saat için rezervasyon yapılamaz.";
            }

            return null;
        }

        private (DateTime BaslangicUtc, DateTime BitisUtc) AraligiUtcAl() =>
            (TurkiyeSaati.UtcyeCevir(Tarih!.Value, Baslangic!.Value),
             TurkiyeSaati.UtcyeCevir(Tarih!.Value, Baslangic!.Value).AddMinutes(Sure!.Value));

        private async Task YukleAsync()
        {
            Masalar = await _context.KutuphaneMasalari
                .OrderBy(m => m.Kat)
                .ThenBy(m => m.MasaNo)
                .ToListAsync();

            ToplamMasa = Masalar.Count;

            var simdiUtc = DateTime.UtcNow;
            var simdiDolu = await _context.MasaRezervasyonlari
                .Where(r => !r.IptalEdildiMi && r.Baslangic <= simdiUtc && r.Bitis > simdiUtc)
                .Select(r => r.MasaId)
                .Distinct()
                .ToListAsync();

            SimdiDoluMasa = simdiDolu.Count;

            var suAn = TimeOnly.FromDateTime(Simdi);
            KutuphaneAcik = suAn >= KutuphaneKurallari.Acilis && suAn < KutuphaneKurallari.Kapanis;

            AralikHatasi = AraligiDenetle();
            if (AralikHatasi != null)
            {
                KatlariHesapla(simdiDolu.ToHashSet());
                return;
            }

            var (baslangicUtc, bitisUtc) = AraligiUtcAl();

            DoluMasalar = (await _context.MasaRezervasyonlari
                .Where(r => !r.IptalEdildiMi && r.Baslangic < bitisUtc && r.Bitis > baslangicUtc)
                .Select(r => r.MasaId)
                .ToListAsync())
                .ToHashSet();

            KatlariHesapla(DoluMasalar);

            var kullaniciId = KullaniciId();
            GunlukKullanilan = await GunlukKullanilanSureAsync(kullaniciId);
            CakisanRezervasyonum = await _context.MasaRezervasyonlari
                .Include(r => r.Masa)
                .FirstOrDefaultAsync(r => r.KullaniciId == kullaniciId && !r.IptalEdildiMi &&
                    r.Baslangic < bitisUtc && r.Bitis > baslangicUtc);
        }

        private async Task<TimeSpan> GunlukKullanilanSureAsync(int kullaniciId)
        {
            var gunBaslangici = TurkiyeSaati.UtcyeCevir(Tarih!.Value, TimeOnly.MinValue);
            var gunBitisi = TurkiyeSaati.UtcyeCevir(Tarih!.Value.AddDays(1), TimeOnly.MinValue);

            var rezervasyonlar = await _context.MasaRezervasyonlari
                .Where(r => r.KullaniciId == kullaniciId && !r.IptalEdildiMi &&
                    r.Baslangic >= gunBaslangici && r.Baslangic < gunBitisi)
                .Select(r => new { r.Baslangic, r.Bitis })
                .ToListAsync();

            return TimeSpan.FromTicks(rezervasyonlar.Sum(r => (r.Bitis - r.Baslangic).Ticks));
        }

        public static string SureMetni(TimeSpan sure)
        {
            if (sure <= TimeSpan.Zero) return "0 dakika";
            var saat = (int)sure.TotalHours;
            var dakika = sure.Minutes;
            if (saat == 0) return $"{dakika} dakika";
            return dakika == 0 ? $"{saat} saat" : $"{saat} saat {dakika} dakika";
        }

        private void KatlariHesapla(HashSet<int> doluMasalar)
        {
            Katlar = Masalar
                .GroupBy(m => m.Kat)
                .Select(g => new KatDolulugu(g.Key, g.Count(m => doluMasalar.Contains(m.Id)), g.Count()))
                .ToList();
        }

        private int KullaniciId() => int.Parse(_userManager.GetUserId(User)!);

        public record KatDolulugu(int Kat, int Dolu, int Toplam)
        {
            public int Yuzde => Toplam == 0 ? 0 : (int)Math.Round(100.0 * Dolu / Toplam);
        }
    }
}
