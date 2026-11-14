using KampusPortali.Data;
using KampusPortali.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Yonetim
{
    public class DuyurularModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DuyurularModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Duyuru> Duyurular { get; set; } = new();

        public async Task OnGetAsync()
        {
            Duyurular = await _context.Duyurular
                .Include(d => d.Yazar)
                .OrderByDescending(d => d.YayinTarihi)
                .ThenByDescending(d => d.Id)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostSilAsync(int id)
        {
            var duyuru = await _context.Duyurular.FindAsync(id);
            if (duyuru == null)
            {
                return NotFound();
            }

            _context.Duyurular.Remove(duyuru);
            await _context.SaveChangesAsync();

            TempData["Bilgi"] = $"\"{duyuru.Baslik}\" duyurusu silindi.";
            return RedirectToPage();
        }
    }
}
