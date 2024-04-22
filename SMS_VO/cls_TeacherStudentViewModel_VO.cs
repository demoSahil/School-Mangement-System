using SMS_VO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_VO
{
    public class cls_TeacherStudentViewModel_VO
    {
        public cls_Teacher_VO teacher { get; set; }
        public List<cls_Student_VO> studentsLinked { get; set; }
    }
}
