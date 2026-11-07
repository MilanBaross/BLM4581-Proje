using KampusPortali.Data;
using KampusPortali.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Kampus
{
    public class KayipEsyaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public KayipEsyaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<KayipEsya> Ilanlar { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public bool Bekleyenler { get; set; }

        public async Task OnGetAsync()
        {
            var sorgu = _context.KayipEsyalar
                .Include(e => e.BildirenKullanici)
                .AsQueryable();

            if (Bekleyenler)
            {
                sorgu = sorgu.Where(e => !e.TeslimEdildiMi);
            }

            Ilanlar = await sorgu
                .OrderByDescending(e => e.BulunmaTarihi)
                .ThenByDescending(e => e.Id)
                .ToListAsync();
        }
    }
}
