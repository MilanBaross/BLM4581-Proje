using KampusPortali.Data;
using KampusPortali.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages
{
    // Duyuruyu giriş yapan herkes okuyabilir; düzenleme ve silme sadece yöneticide görünür.
    [Authorize]
    public class DuyuruDetayModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DuyuruDetayModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Duyuru Duyuru { get; set; } = default!;

        public bool YoneticiMi => User.IsInRole(Roller.Yonetici);

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var duyuru = await _context.Duyurular
                .Include(d => d.Yazar)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (duyuru == null)
            {
                return NotFound();
            }

            Duyuru = duyuru;
            return Page();
        }
    }
}
