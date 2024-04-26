using SMS_DL;
using SMS_VO;
using SMS_VO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_BM
{
    public class cls_Teacher_BM
    {
        private cls_Teacher_DL _dl;
        private cls_TeacherStudent_BM _TeacherStudentBM;

        public cls_Teacher_BM(string connectionString)
        {
            _dl = new cls_Teacher_DL(connectionString);
            _TeacherStudentBM = new cls_TeacherStudent_BM(connectionString);
        }
        private bool IsValid(cls_Teacher_VO teacher)
        {
            if (teacher.TeacherName.Length > 50)
            {
                teacher.ErrorMessage = "First name length exceed 50 characters!";
                return false;
            }

            else if (string.IsNullOrEmpty(teacher.Subject))
            {
                teacher.ErrorMessage = "Subject cannot be empty";
                return false;
            }
            else if (string.IsNullOrEmpty(teacher.ContactNumber))
            {
                teacher.ErrorMessage = "Contact number cannot be empty";
                return false;
            }
            else if (string.IsNullOrEmpty(teacher.TeacherName))
            {
                teacher.ErrorMessage = "Teacher Name cannot be empty";
                return false;
            }

            return true;

        }
        public bool Add(cls_Teacher_VO teacher, string[] studentIds)
        {
            if (IsValid(teacher))
            {
                int? newTeacherID = _dl.InsertTeacherDetails(teacher);
                if (newTeacherID != null && newTeacherID > 0)
                {
                    if (_TeacherStudentBM.MapStudents(studentIds, newTeacherID))
                    {
                        return true;
                    }
                } // Success
            }

            return false;
        }

        public bool Edit(cls_Teacher_VO teacher, string[] studentIDs)
        {
            if (IsValid(teacher))
            {
                if (_dl.UpdateTeacherDetails(teacher))
                {
                    if (_TeacherStudentBM.UpdateStudentsMapping(studentIDs, teacher.TeacherId))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Delete the Teacher from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns> True when Teacher get Deleted Successfully otherwise false</returns>
        public bool Delete(int? id)
        {
            //Proceed with delete and return true
            if (_TeacherStudentBM.DeleteStudentsMapping(id) && _dl.DeleteTeacherDetails(id))
            {
                return true;
            }

            return false;
        }

        public List<cls_Teacher_VO> ReadAllTeachers()
        {
            return _dl.GetAllTeachersList();
        }

        public List<cls_Student_VO> GetListOfStudentsUnderTeacher(int? id)
        {
            return _TeacherStudentBM.StudentsLinkedWithTeacher(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> false if there are any students Assigned</returns>
        public bool CheckingAnyStudentsAssignedToTeacher(int? id)
        {
            return _TeacherStudentBM.CanDeleteTeacherDetails(id);
        }


    }
}
