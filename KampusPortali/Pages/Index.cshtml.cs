using KampusPortali.Data;
using KampusPortali.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Kullanici> _userManager;

    public IndexModel(ApplicationDbContext context, UserManager<Kullanici> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<Duyuru> Duyurular { get; set; } = new();

    public string Ad { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return RedirectToPage("/Giris");
        }

        var kullanici = await _userManager.GetUserAsync(User);
        Ad = kullanici?.Ad ?? string.Empty;

        Duyurular = await _context.Duyurular
            .Include(d => d.Yazar)
            .OrderByDescending(d => d.OnemliMi)
            .ThenByDescending(d => d.YayinTarihi)
            .Take(10)
            .ToListAsync();

        return Page();
    }
}
