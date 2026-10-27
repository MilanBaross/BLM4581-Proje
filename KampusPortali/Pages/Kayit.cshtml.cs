using KampusPortali.Models;
using KampusPortali.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusPortali.Pages
{
    public class KayitModel : PageModel
    {
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly UserManager<Kullanici> _userManager;

        public KayitModel(SignInManager<Kullanici> signInManager, UserManager<Kullanici> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public KayitViewModel Girdi { get; set; } = new();

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

            var ogrenciNo = string.IsNullOrWhiteSpace(Girdi.OgrenciNo) ? null : Girdi.OgrenciNo.Trim();

            if (ogrenciNo != null && await _userManager.Users.AnyAsync(k => k.OgrenciNo == ogrenciNo))
            {
                ModelState.AddModelError($"{nameof(Girdi)}.{nameof(Girdi.OgrenciNo)}", "Bu öğrenci numarası zaten kayıtlı.");
                return Page();
            }

            var kullanici = new Kullanici
            {
                UserName = Girdi.Email.Trim(),
                Email = Girdi.Email.Trim(),
                Ad = Girdi.Ad.Trim(),
                Soyad = Girdi.Soyad.Trim(),
                OgrenciNo = ogrenciNo
            };

            var sonuc = await _userManager.CreateAsync(kullanici, Girdi.Sifre);

            if (!sonuc.Succeeded)
            {
                foreach (var hata in sonuc.Errors)
                {
                    ModelState.AddModelError(string.Empty, hata.Description);
                }
                return Page();
            }

            await _userManager.AddToRoleAsync(kullanici, Roller.Kullanici);
            await _signInManager.SignInAsync(kullanici, isPersistent: false);

            TempData["Bilgi"] = $"Hoş geldin {kullanici.Ad}! Kaydın başarıyla oluşturuldu.";
            return LocalRedirect(returnUrl);
        }
    }
}
