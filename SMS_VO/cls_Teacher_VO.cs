using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_VO
{
    public class cls_Teacher_VO
    {
        [Key]
        public int TeacherId {  get; set; }

        [Required(ErrorMessage ="Teacher name is an required field")]
        public string TeacherName { get; set; }
        
        [Required(ErrorMessage = "Subject is an required field")]
        public string Subject {  get; set; }

        [DataType(DataType.PhoneNumber)]
        public string ContactNumber {  get; set; }

        [ScaffoldColumn(false)]
        public string ErrorMessage {  get; set; }
    }
}
