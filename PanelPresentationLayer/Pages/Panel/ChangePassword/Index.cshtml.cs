using Common.Utils;
using Microsoft.AspNetCore.Mvc;
using PanelBusinessLogicLayer.BusinessServices.IdentitiesServices;
using PanelPresentationLayer.Infrastructure.JwtUtil;
using PanelPresentationLayer.Infrastructure.RazorUtils;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace PanelPresentationLayer.Pages.Panel.ChangePassword
{
    [BindProperties]
    public class IndexModel : BaseRazor
    {
        private readonly UserService _userService;

        public IndexModel()
        {
            _userService = new UserService();
        }

        [Required(ErrorMessage = "کلمه عبور فعلی الزامیست")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "کلمه عبور الزامیست")]
        public string Password { get; set; }
        [Required(ErrorMessage = "تکرار کلمه عبور الزامیست")]
        [Compare("Password", ErrorMessage = "کلمه های عبور یکسان نیستند")]
        public string ConfirmPassword { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userService.FindAsync(User.GetUserId());
            if (user == null)
            {
                ErrorAlert("کاربر یافت نشد");
                return Page();
            }
            if (!Sha256Hasher.IsCompare(user.Password, OldPassword))
            {
                ErrorAlert("کلمه عبور فعلی صحیح نمیاشد");
                return Page();
            }

            var result = await _userService.ChangePasswordAsync(User.GetUserId(), Sha256Hasher.Hash(Password));
            return RedirectAndShowAlert(result, Page());
        }
    }
}
