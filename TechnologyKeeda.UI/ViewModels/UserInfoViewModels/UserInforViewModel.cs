using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyKeeda.UI.ViewModels.UserInfoViewModels
{
    public class UserInforViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="aapko username field ko fill karna hain ")]
        [Display(Name ="User Name")]
        public  string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
