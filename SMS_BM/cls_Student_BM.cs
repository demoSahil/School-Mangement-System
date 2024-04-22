using SMS_DL;
using SMS_VO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace SMS_BM
{
    public class cls_Student_BM
    {

        private cls_Students_DL _dl;
        private cls_TeacherStudent_BM _TeacherStudentBM;

        public cls_Student_BM(string connectionString)
        {
            _dl = new cls_Students_DL(connectionString);
            _TeacherStudentBM = new cls_TeacherStudent_BM(connectionString);
        }
        private bool IsValid(cls_Student_VO student)
        {
            if (student.FirstName.Length > 50)
            {
                student.ErrorMessage = "First name length exceed 50 characters!";
                return false;
            }

            else if (student.LastName.Length > 25)
            {
                student.ErrorMessage = "Last name length exceed 25 characters!";
                return false;
            }

            return true;

        }
        public bool Add(cls_Student_VO student)
        {
            if (IsValid(student))
            {
                if (_dl.InsertStudentDetails(student))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Edit(cls_Student_VO student)
        {
            if (IsValid(student))
            {
                if (_dl.UpdateStudentDetails(student)) return true;
            }

            return false;
        }

        /// <summary>
        /// Delete the students from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns> True when Student get Deleted Successfully otherwise false</returns>
        public bool Delete(int? id)
        {
            // Check whether students assigned to any teacher
            if (_TeacherStudentBM.CanDeleteStudentDetails(id))
            {
                //Proceed with delete and return true
                if (_dl.DeleteStudentDetails(id))
                {
                    return true;
                }
            }

            return false;
        }

        public List<cls_Student_VO> ReadAllStudents()
        {
            return _dl.GetStudentsList();
        }
    }
}
