using SMS_BM;
using SMS_VO.Models;
using SMS_WEBUI.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS_WEBUI.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        /// <summary>
        /// BM layer object of Student class
        /// </summary>
        cls_Student_BM _studentBM;

        /// <summary>
        /// BM layer object of TeacherStudent class
        /// </summary>
        cls_TeacherStudent_BM _teacherStudentBM;

        /// <summary>
        /// Object of Helper class (XML) responsible for handling xml Docs
        /// </summary>
        cls_xml_Helper _xml;
        public StudentController()
        {
            _studentBM = new cls_Student_BM(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            _teacherStudentBM = new cls_TeacherStudent_BM(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            _xml = new cls_xml_Helper();
        }


        public ActionResult Index()
        {
            // Retrieving Students Records
            List<cls_Student_VO> students = _studentBM.ReadAllStudents();

            // Updating XML DOC
            _xml.ConvertToXml(students, Server.MapPath("~/App_Data/students.xml"));


            return View(students);
        }

        [Authorize(Roles = "Admin")]
        //[AJAX] GET /Home/Delete?id={id}
        public ActionResult Delete(int? studentId)
        {
            bool _flag = false;
            bool alreadyMappedWithTeacher = false;
            if (!_teacherStudentBM.CanDeleteStudentDetails(studentId))
            {
                alreadyMappedWithTeacher = true;
            }

            if (!alreadyMappedWithTeacher)
            {
                if (_studentBM.Delete(studentId))
                {
                    // Retrieving Students Records
                    List<cls_Student_VO> students = _studentBM.ReadAllStudents();

                    // Updating XML DOC
                    _xml.ConvertToXml(students, Server.MapPath("~/App_Data/students.xml"));
                    _flag = true;
                } // Success
            }
            return Json(new
            {
                flag = _flag,
                mapped = alreadyMappedWithTeacher
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View(new cls_Student_VO());
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(cls_Student_VO student)
        {
            if (_studentBM.Add(student))
            {
                TempData["_ToastMessage"] = "Student Details Added Successfully";
                return RedirectToAction("Index");
            } // Success

            return View();
        }
        public ActionResult Edit(int? id)
        {
            // Retrieving Students Records
            List<cls_Student_VO> students = _xml.ParseToList(Server.MapPath("~/App_Data/students.xml"));

            var student = students.Where(s => s.StudentID == id).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(cls_Student_VO updatedStudent)
        {
            if (_studentBM.Edit(updatedStudent))
            {
                TempData["_ToastMessage"] = "Student Details Updated Successfully";
                return RedirectToAction("Index");
            } // Success

            return View();
        }

        public ActionResult GetStudentsList()
        {
            List<cls_Student_VO> students= _xml.ParseToList(Server.MapPath("~/App_Data/students.xml"));
            return Json(students, JsonRequestBehavior.AllowGet);
        }
        

    }
}