using KampusPortali.Data;
using KampusPortali.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Kampus
{
    public class KayipEsyaDetayModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Kullanici> _userManager;

        public KayipEsyaDetayModel(ApplicationDbContext context, UserManager<Kullanici> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public KayipEsya Ilan { get; set; } = null!;

        public bool DuzenleyebilirMi { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ilan = await _context.KayipEsyalar
                .Include(e => e.BildirenKullanici)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ilan == null)
            {
                return NotFound();
            }

            Ilan = ilan;
            DuzenleyebilirMi = YetkiliMi(ilan);
            return Page();
        }

        public async Task<IActionResult> OnPostSilAsync(int id)
        {
            var ilan = await _context.KayipEsyalar.FindAsync(id);

            if (ilan == null)
            {
                return NotFound();
            }

            // Buton gizli olsa da istek elle gönderilebilir; yetki burada da kontrol edilir.
            if (!YetkiliMi(ilan))
            {
                return Forbid();
            }

            _context.KayipEsyalar.Remove(ilan);
            await _context.SaveChangesAsync();

            TempData["Bilgi"] = $"\"{ilan.Baslik}\" ilanı silindi.";
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
