using Common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PanelBusinessLogicLayer.BusinessServices.IdentitiesServices;
using PanelPresentationLayer.Infrastructure.JwtUtil;
using PanelPresentationLayer.Infrastructure.RazorUtils;
using PanelViewModel.Authentication;
using System.ComponentModel.DataAnnotations;

namespace PanelPresentationLayer.Pages.Authentication
{
    [BindProperties]
    public class LoginModel : BaseRazor
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _userService = new UserService();
            _configuration = configuration;
        }

        [Required(ErrorMessage = "نام کاربری الزامیست")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "کلمه عبور الزامیست")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public LoginViewModel LoginViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userService.FindByUserNameAsync(UserName);
            if (user == null)
            {
                ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }
            var passwordCompare = Sha256Hasher.IsCompare(user.Password, Password);
            if (!passwordCompare)
            {
                ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }
            var token = JwtTokenBuilder.BuildToken(user, _configuration);
            if (RememberMe)
            {
                HttpContext.Response.Cookies.Append("code-token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.Now.AddDays(30),
                    Secure = true
                });
            }
            else
            {
                HttpContext.Response.Cookies.Append("code-token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true
                });
            }
            return RedirectToPage("/");
        }

        
    }
}
