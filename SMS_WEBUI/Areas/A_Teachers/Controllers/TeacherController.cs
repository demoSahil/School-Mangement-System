
using SMS_BM;
using SMS_VO;
using SMS_WEBUI.Helper;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace SMS_WEBUI.Areas.A_Teachers.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class TeacherController : Controller
    {
        cls_Teacher_BM _teacherBM;
        cls_xml_Helper _xml;

        public TeacherController()
        {
            _teacherBM = new cls_Teacher_BM(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            _xml = new cls_xml_Helper();
        }
        // GET: Teacher
        public ActionResult Index(bool getCount = false)
        {
            List<cls_Teacher_VO> teachers = _teacherBM.ReadAllTeachers();
            _xml.ConvertToXml_Teachers(teachers, Server.MapPath("~/App_Data/teachers.xml"));

            if (getCount)
            {
                return Json(new { count = teachers.Count }, JsonRequestBehavior.AllowGet);
            }
            return View(teachers);
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int id)
        {
            List<cls_Teacher_VO> teachers = _xml.ParseToList_Teacher(Server.MapPath("~/App_Data/teachers.xml"));

            cls_Teacher_VO teacher = teachers.Where(t => t.TeacherId == id).FirstOrDefault();
            return View(teacher);
        }

        [Authorize(Roles = "Admin")]
        // GET: Teacher/Create
        public ActionResult Create()
        {
            cls_Teacher_VO teacher = new cls_Teacher_VO();
            return View(teacher);
        }

        // POST: Teacher/Create

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(cls_Teacher_VO teacher, string[] selectedStudents)
        {
            string[] studentIds = GetStudentIDSFromString(selectedStudents);
            try
            {
                if (_teacherBM.Add(teacher, studentIds))
                {
                    TempData["_ToastMessage"] = "Teacher Details Added Successfully";
                    return RedirectToAction("Index");
                } // Success

                return View();

            }
            catch
            {
                return View();
            }
        }


        [Authorize(Roles = "Admin")]
        // GET: Teacher/Edit/5
        public ActionResult Edit(int id)
        {
            List<cls_Teacher_VO> teachers = _xml.ParseToList_Teacher(Server.MapPath("~/App_Data/teachers.xml"));
            cls_Teacher_VO teacherToEdit = teachers.Where(t => t.TeacherId == id).FirstOrDefault();
            return View(teacherToEdit);
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(cls_Teacher_VO updatedTeacher, string[] selectedStudents)
        {
            try
            {
                if (_teacherBM.Edit(updatedTeacher, GetStudentIDSFromString(selectedStudents)))
                {
                    TempData["_ToastMessage"] = "Teacher Details Updated Successfully";
                    return RedirectToAction("Index");
                } // Success
                return View();
            }
            catch
            {
                return View();
            }
        }
        // [AJAX]GET: Teacher/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? teacherId)
        {
            bool _flag = false;
            bool alreadyMappedWithStudent = false;

            /*     if (!_teacherBM.CheckingAnyStudentsAssignedToTeacher(teacherId))
                 {
                     return Content("confirmDelete");
                 }*/
            //if yes
            if (_teacherBM.Delete(teacherId))
            {
                // Retrieving Students Records
                List<cls_Teacher_VO> teachers = _teacherBM.ReadAllTeachers();

                // Updating XML DOC
                _xml.ConvertToXml_Teachers(teachers, Server.MapPath("~/App_Data/teachers.xml"));
                _flag = true;
            } // Success

            //if no
            else
            {
                alreadyMappedWithStudent = true;
            }

            return Json(new
            {
                flag = _flag,
                mapped = alreadyMappedWithStudent
            }, JsonRequestBehavior.AllowGet);
        }


        //AJAX GET: Teacher/Search?value
        public ActionResult Search(string value)
        {

            value = value.ToLower();
            List<cls_Teacher_VO> teachers = _xml.ParseToList_Teacher(Server.MapPath("~/App_Data/teachers.xml"));

            List<cls_Teacher_VO> result = teachers.Where(t =>
            t.TeacherId.ToString().Contains(value) ||
            t.TeacherName.ToString().ToLower().Contains(value) ||
            t.Subject.ToLower().Contains(value) ||
            t.ContactNumber.Contains(value)).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult StudentsLinkedWithTeacher(int? teacherId, string teacherName, bool returnJson = false)
        {
            ViewData["TeacherName"] = teacherName;
            if (returnJson)
            {
                return Json(_teacherBM.GetListOfStudentsUnderTeacher(teacherId), JsonRequestBehavior.AllowGet);
            }
            return View("~/Areas/A_Students/Views/Student/Index.cshtml", _teacherBM.GetListOfStudentsUnderTeacher(teacherId));
        }


        public string[] GetStudentIDSFromString(string[] studentList)
        {
            string[] result = new string[studentList.Length];

            for (int i = 0; i < studentList.Length; i++)
            {
                int closeBracket = studentList[i].LastIndexOf(']');
                int openBracket = studentList[i].LastIndexOf('[');

                result[i] = studentList[i].Substring(openBracket + 1, closeBracket - openBracket - 1);
            }
            return result;
        }


    }
}
