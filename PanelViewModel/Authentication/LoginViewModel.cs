
using System.ComponentModel.DataAnnotations;

namespace PanelViewModel.Authentication
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری الزامیست")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "کلمه عبور الزامیست")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
