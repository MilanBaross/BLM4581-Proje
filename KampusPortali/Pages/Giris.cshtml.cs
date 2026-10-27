using KampusPortali.Models;
using KampusPortali.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages
{
    public class GirisModel : PageModel
    {
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly UserManager<Kullanici> _userManager;

        public GirisModel(SignInManager<Kullanici> signInManager, UserManager<Kullanici> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public GirisViewModel Girdi { get; set; } = new();

        public string? ReturnUrl { get; set; }

        public IActionResult OnGet(string? returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToPage("/Index");
            }

            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var giris = Girdi.KullaniciAdiVeyaEmail.Trim();
            Kullanici? kullanici = giris.Contains('@')
                ? await _userManager.FindByEmailAsync(giris)
                : await _userManager.Users.FirstOrDefaultAsync(k => k.OgrenciNo == giris);

            if (kullanici == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bilgileri veya şifre hatalı.");
                return Page();
            }

            var sonuc = await _signInManager.PasswordSignInAsync(
                kullanici, Girdi.Sifre, Girdi.BeniHatirla, lockoutOnFailure: true);

            if (sonuc.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            if (sonuc.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Çok fazla hatalı deneme yapıldı. Hesabınız 5 dakika kilitlendi.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bilgileri veya şifre hatalı.");
            }

            return Page();
        }
    }
}
