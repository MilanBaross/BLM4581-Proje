using System.Text.Json;
using KampusPortali.Data;
using KampusPortali.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages.Kampus
{
    public class HaritaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HaritaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Mekan> Mekanlar { get; set; } = new();

        public List<MekanTuru> Turler { get; set; } = new();

        public string MekanlarJson { get; set; } = "[]";

        public async Task OnGetAsync()
        {
            Mekanlar = await _context.Mekanlar
                .Include(m => m.Derslikler)
                .OrderBy(m => m.Ad)
                .ToListAsync();

            Turler = Mekanlar
                .Select(m => m.Tur)
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            MekanlarJson = JsonSerializer.Serialize(Mekanlar.Select(m => new
            {
                id = m.Id,
                ad = m.Ad,
                tur = m.Tur.ToString(),
                turAdi = m.Tur.GorunenAd(),
                ikon = m.Tur.Ikon(),
                enlem = m.Enlem,
                boylam = m.Boylam,
                aciklama = m.Aciklama,
                derslikler = m.Derslikler.Select(d => d.Ad).OrderBy(a => a).ToList(),
                geometri = m.Geometri == null ? (JsonElement?)null : JsonDocument.Parse(m.Geometri).RootElement.Clone()
            }));
        }
    }
}
