using KampusPortali.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KampusPortali.Pages
{
    public class CikisModel : PageModel
    {
        private readonly SignInManager<Kullanici> _signInManager;

        public CikisModel(SignInManager<Kullanici> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult OnGet() => RedirectToPage("/Index");

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            TempData["Bilgi"] = "Başarıyla çıkış yaptınız.";
            return RedirectToPage("/Giris");
        }
    }
}
