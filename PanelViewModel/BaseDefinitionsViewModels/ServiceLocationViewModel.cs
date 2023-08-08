using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelViewModel.BaseDefinitionsViewModels
{
    public class ServiceLocationViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "عنوان الزامیست")]
        [MaxLength(60, ErrorMessage = "عنوان نمیتواند بیشتر از 60 کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "عنوان نمیتواند کمتر از 3 کاراکتر باشد")]
        public string Title { get; set; }
        public DateTime RegDate { get; set; }
    }
}
