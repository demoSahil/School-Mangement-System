using SMS_DL;
using SMS_VO.Models;
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

        public List<cls_Student_VO> StudentsLinkedWithTeacher(int? id)
        {
            return _dl.GetStudentsUnderTeacher(id);
        }

        public bool MapStudents(string[] studentIds, int? teacherID)
        {
            return _dl.MapStudentWithTeacher(studentIds, teacherID);
        }

        public bool UpdateStudentsMapping(string[] studentIds, int? teacherID)
        {
            return _dl.UpdateMapping(studentIds, teacherID);
        }

        public bool DeleteStudentsMapping(int? teacherID)
        {
            return _dl.DeleteMapping(teacherID);
        }
    }
}
