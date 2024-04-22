using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_VO
{
    public class cls_User_VO
    {
        [Key]
        [ScaffoldColumn(false)]
        [Display(Name ="User ID")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "User is Required")]
        [Display(Name ="User Type")]
        public string UserType { get; set; }

    }
}
