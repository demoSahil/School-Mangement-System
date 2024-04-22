using SMS_DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_BM
{
    public class cls_TeacherStudent_BM
    {
        private readonly cls_TeacherStudent_DL _dl;

        public cls_TeacherStudent_BM(string connectionString)
        {
            _dl = new cls_TeacherStudent_DL(connectionString);
        }

        public bool CanDeleteStudentDetails(int? id)
        {
            if (!_dl.CheckStudentMappingWithAnyTeacher(id))
            {
                return true;
            }
            return false;
        }

        public bool CanDeleteTeacherDetails(int? id)
        {
            if (!_dl.CheckTeacherMappingWithAnyStudent(id))
            {
                return true;
            }
            return false;
        }
    }
}
