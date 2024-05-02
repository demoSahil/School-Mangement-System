using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SMS_VO.Models
{

    
    public class cls_Student_VO
    {
        [Key]
        [Display(Name ="Student ID")]
        
        public int StudentID { get; set; }

        [Required(ErrorMessage = "First name is an required field")]
        [MaxLength(50, ErrorMessage = "Cannot exceed more than 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime? DOB { get; set; }

        [Display(Name ="Student class")]
        [Required]
        public Enum Class { get; set; }

        public string City { get; set; }

        [Required(ErrorMessage = "Gender must be specified")]
        public char Gender { get; set; }

        [EmailAddress]
        [Required(ErrorMessage ="Email address is Required")]
        public string Email { get; set; }

        [Display(Name = "Contact number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [ScaffoldColumn(false)]
        public string ErrorMessage {  get; set; }
      
    }
}
