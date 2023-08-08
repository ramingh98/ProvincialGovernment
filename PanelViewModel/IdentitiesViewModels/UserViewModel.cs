using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelViewModel.IdentitiesViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "نام کاربری الزامیست")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "کلمه عبور الزامیست")]
        public string Password { get; set; }
    }
}
