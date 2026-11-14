using KampusPortali.Data;
using KampusPortali.Models;
using KampusPortali.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KampusPortali.Pages.Yonetim
{
    // Ekleme ve düzenleme için ortak form: ?id= varsa düzenleme, yoksa ekleme.
    public class DuyuruFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Kullanici> _userManager;

        public DuyuruFormModel(ApplicationDbContext context, UserManager<Kullanici> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public DuyuruViewModel Girdi { get; set; } = new();

        public int? Id { get; set; }

        public bool DuzenlemeMi => Id.HasValue;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Id = id;

            if (id == null)
            {
                return Page();
            }

            var duyuru = await _context.Duyurular.FindAsync(id);
            if (duyuru == null)
            {
                return NotFound();
            }

            Girdi = new DuyuruViewModel
            {
                Baslik = duyuru.Baslik,
                Icerik = duyuru.Icerik,
                OnemliMi = duyuru.OnemliMi
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Id = id;

            Duyuru? duyuru = null;
            if (id != null)
            {
                duyuru = await _context.Duyurular.FindAsync(id);
                if (duyuru == null)
                {
                    return NotFound();
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (duyuru == null)
            {
                if (!int.TryParse(_userManager.GetUserId(User), out var yazarId))
                {
                    return Forbid();
                }

                duyuru = new Duyuru
                {
                    YazarId = yazarId,
                    YayinTarihi = DateTime.UtcNow
                };
                _context.Duyurular.Add(duyuru);
            }

            duyuru.Baslik = Girdi.Baslik.Trim();
            duyuru.Icerik = Girdi.Icerik.Trim();
            duyuru.OnemliMi = Girdi.OnemliMi;

            await _context.SaveChangesAsync();

            TempData["Bilgi"] = "Duyuru kaydedildi.";
            return RedirectToPage("/Yonetim/Duyurular");
        }
    }
}
