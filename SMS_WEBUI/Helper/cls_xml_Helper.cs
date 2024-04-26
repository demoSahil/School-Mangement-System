using SMS_VO;
using SMS_VO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SMS_WEBUI.Helper
{
    public class cls_xml_Helper
    {
        public void ConvertToXml(List<cls_Student_VO> students, string filePath)
        {
            XElement rootElement = new XElement("Students");

            foreach (var student in students)
            {
                XElement element = new XElement("Student", new
                    XElement("Id", student.StudentID),
                   new XElement("FirstName", student.FirstName),
                   new XElement("LastName", student.LastName),
                   new XElement("DOB", student.DOB),
                   new XElement("City", student.City),
                   new XElement("Gender", student.Gender),
                   new XElement("Email", student.Email),
                   new XElement("PhoneNumber", student.PhoneNumber)
                    );

                rootElement.Add(element);
            }

            XDocument xml = new XDocument();
            xml.Add(rootElement);
            string text = xml.ToString();
            File.WriteAllText(filePath, text);
        }

        public List<cls_Student_VO> ParseToList(string filePath)
        {
            List<cls_Student_VO> _students = new List<cls_Student_VO>();

            XDocument xml = XDocument.Load(filePath);

            foreach (XElement element in xml.Root.Elements("Student"))
            {
                cls_Student_VO student = new cls_Student_VO()
                {
                    StudentID = Convert.ToInt32(element.Element("Id").Value),
                    FirstName = element.Element("FirstName").Value,
                    LastName = element.Element("LastName").Value,
                    City = element.Element("City").Value,
                    Email = element.Element("Email").Value,
                    Gender = Convert.ToChar(element.Element("Gender").Value),
                    PhoneNumber = element.Element("PhoneNumber").Value
                };


                // Handling Null date
                if (element.Element("DOB").Value == "")
                {
                    student.DOB = null;
                }
                else
                {
                    student.DOB = Convert.ToDateTime(element.Element("DOB").Value);
                }
                _students.Add(student);
            }

            return _students;
        }

        public void ConvertToXml_Teachers(List<cls_Teacher_VO> teachers, string filePath)
        {
            XElement rootElement = new XElement("Teachers");

            foreach (var teacher in teachers)
            {
                XElement element = new XElement("Teacher", new
                    XElement("Id", teacher.TeacherId),
                   new XElement("Name", teacher.TeacherName),
                   new XElement("Subject", teacher.Subject),
                   new XElement("Contact", teacher.ContactNumber)

                    );

                XElement students = new XElement("StudentAssociated");

                foreach (int studentId in teacher.studentsIDUnderTeacher)
                {
                    XElement studentsIDAssociated = new XElement("StudentId", studentId);

                    students.Add(studentsIDAssociated);
                }
                element.Add(students);
                rootElement.Add(element);
            }

            XDocument xml = new XDocument();
            xml.Add(rootElement);
            string text = xml.ToString();
            File.WriteAllText(filePath, text);
        }

        public List<cls_Teacher_VO> ParseToList_Teacher(string filePath)
        {
            List<cls_Teacher_VO> _teachers = new List<cls_Teacher_VO>();
            XDocument xml = XDocument.Load(filePath);

            foreach (XElement teacherElement in xml.Root.Elements("Teacher"))
            {
                cls_Teacher_VO teacher = new cls_Teacher_VO()
                {
                    TeacherId = Convert.ToInt32(teacherElement.Element("Id").Value),
                    TeacherName = teacherElement.Element("Name").Value,
                    Subject = teacherElement.Element("Subject").Value,
                    ContactNumber = teacherElement.Element("Contact").Value,
                };

                // Get all <StudentId> elements within the current <Teacher> element
                IEnumerable<XElement> studentElements = teacherElement.Element("StudentAssociated").Elements("StudentId");

                foreach (XElement studentElement in studentElements)
                {
                    teacher.studentsIDUnderTeacher.Add(Convert.ToInt32(studentElement.Value));
                }

                _teachers.Add(teacher);
            }
            return _teachers;
        }
    }
}