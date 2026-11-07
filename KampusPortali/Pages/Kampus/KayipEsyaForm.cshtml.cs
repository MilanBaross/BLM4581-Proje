using KampusPortali.Data;
using KampusPortali.Models;
using KampusPortali.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KampusPortali.Pages.Kampus
{
    // Ekleme ve düzenleme için ortak form: id varsa düzenleme, yoksa ekleme.
    public class KayipEsyaFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Kullanici> _userManager;

        public KayipEsyaFormModel(ApplicationDbContext context, UserManager<Kullanici> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public KayipEsyaViewModel Girdi { get; set; } = new();

        public int? Id { get; set; }

        public bool DuzenlemeMi => Id.HasValue;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Id = id;

            if (id == null)
            {
                return Page();
            }

            var ilan = await _context.KayipEsyalar.FindAsync(id);
            if (ilan == null)
            {
                return NotFound();
            }

            if (!YetkiliMi(ilan))
            {
                return Forbid();
            }

            Girdi = new KayipEsyaViewModel
            {
                Baslik = ilan.Baslik,
                Aciklama = ilan.Aciklama,
                BulunanYer = ilan.BulunanYer,
                BulunmaTarihi = ilan.BulunmaTarihi.ToLocalTime().Date,
                TeslimEdildiMi = ilan.TeslimEdildiMi
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Id = id;

            KayipEsya? ilan = null;
            if (id != null)
            {
                ilan = await _context.KayipEsyalar.FindAsync(id);
                if (ilan == null)
                {
                    return NotFound();
                }

                // Form gizli olsa da istek elle gönderilebilir; yetki burada da kontrol edilir.
                if (!YetkiliMi(ilan))
                {
                    return Forbid();
                }
            }

            if (Girdi.BulunmaTarihi.Date > DateTime.Today)
            {
                ModelState.AddModelError($"{nameof(Girdi)}.{nameof(Girdi.BulunmaTarihi)}", "Bulunma tarihi ileri bir tarih olamaz.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ilan == null)
            {
                if (!int.TryParse(_userManager.GetUserId(User), out var kullaniciId))
                {
                    return Forbid();
                }

                ilan = new KayipEsya { BildirenKullaniciId = kullaniciId };
                _context.KayipEsyalar.Add(ilan);
            }

            ilan.Baslik = Girdi.Baslik.Trim();
            ilan.Aciklama = Girdi.Aciklama.Trim();
            ilan.BulunanYer = Girdi.BulunanYer.Trim();
            // PostgreSQL timestamptz sadece UTC kabul eder.
            ilan.BulunmaTarihi = DateTime.SpecifyKind(Girdi.BulunmaTarihi.Date, DateTimeKind.Utc);
            ilan.TeslimEdildiMi = Girdi.TeslimEdildiMi;

            await _context.SaveChangesAsync();

            TempData["Bilgi"] = DuzenlemeMi
                ? $"\"{ilan.Baslik}\" ilanı güncellendi."
                : $"\"{ilan.Baslik}\" ilanı eklendi.";
            return RedirectToPage("/Kampus/KayipEsya");
        }

        // İlanı sadece ekleyen kişi ya da Yönetici değiştirebilir.
        private bool YetkiliMi(KayipEsya ilan)
        {
            var sahibiMi = int.TryParse(_userManager.GetUserId(User), out var kullaniciId)
                && ilan.BildirenKullaniciId == kullaniciId;

            return sahibiMi || User.IsInRole(Roller.Yonetici);
        }
    }
}
