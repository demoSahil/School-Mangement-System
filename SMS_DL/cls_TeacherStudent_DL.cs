using SMS_VO;
using SMS_VO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_DL
{
    public class cls_TeacherStudent_DL
    {
        private readonly string connectionString;
        public cls_TeacherStudent_DL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Checks whether any student is mapped to any teacher or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True when student is mapped with atleast one teacher otherwise false</returns>
        public bool CheckStudentMappingWithAnyTeacher(int? id)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT * FROM StudentTeachers WHERE S_ID=@id";

                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read() && count != 1)
                        {
                            count++;
                        }
                        return count != 0;
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether any Teacher is mapped to any student or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True when student is mapped with atleast one teacher otherwise false</returns>
        public bool CheckTeacherMappingWithAnyStudent(int? id)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT * FROM StudentTeachers WHERE T_ID=@id";

                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read() && count != 1)
                        {
                            count++;
                        }
                        return count != 0;
                    }
                }
            }
        }

        /*  public List<cls_TeacherStudentViewModel_VO> GetTeacherStudentList()
          {
              List<cls_TeacherStudentViewModel_VO> teacherStudent = new List<cls_TeacherStudentViewModel_VO>();
              using (SqlConnection cnn = new SqlConnection(connectionString))
              {
                  cnn.Open();
                  string query = @"
                                  SELECT
                                      StudentTeachers.T_ID AS TeacherID,
                                      Teachers.Teacher_Name AS TeacherName,
                                      Teachers.Subject AS Subject,
                                      Teachers.Contact_Number AS Teacher_ContactNumber,
                                      students.first_name AS Student_FirstName,
                                      students.last_name AS Student_LastName,
                                      StudentTeachers.S_ID AS StudentID,
                                      students.date_of_birth AS Student_DOB,
                                      students.gender AS Student_Gender,
                                      students.email AS Student_Email,
                                      students.phone_number AS Student_ContactNumber
                                  FROM
                                      StudentTeachers
                                  LEFT JOIN
                                      Teachers ON StudentTeachers.T_ID = Teachers.Teacher_ID
                                  LEFT JOIN
                                      students ON StudentTeachers.S_ID = students.student_id
                                  ORDER BY
                                      StudentTeachers.T_ID; ";

                  using (SqlCommand cmd = new SqlCommand(query, cnn))
                  {
                      using (SqlDataReader reader = cmd.ExecuteReader())
                      {

                          int previousTeacherID = 0;
                          while (reader.Read())
                          {
                              bool sameTeacher = false;
                              if (Convert.ToInt32(reader["TeacherID"]) == previousTeacherID)
                              {
                                  sameTeacher = true;
                              }

                              if (!sameTeacher)
                              {
                                  // Teacher Details
                                  cls_Teacher_VO teacher = new cls_Teacher_VO();

                                  teacher.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                                  teacher.TeacherName = reader["TeacherName"] as string;
                                  teacher.Subject = reader["Subject"] as string;
                                  teacher.ContactNumber = reader["Teacher_ContactNumber"] as string;

                                  // Now reading students associated with Teacher

                                  List<cls_Student_VO> studentsAssociatedWithTeacher = new List<cls_Student_VO>();

                                  // Setting previousTeacherId
                                  previousTeacherID = teacher.TeacherId;

                                  cls_Student_VO student = new cls_Student_VO();

                                  student.StudentID = Convert.ToInt32(reader["StudentID"]);
                                  student.FirstName = Convert.ToInt32(reader["Fi"]);


                              }







                          }
                      }
                  }
              }
          }*/

        public List<cls_Student_VO> GetStudentsUnderTeacher(int? teacherId)
        {
            List<cls_Student_VO> studentsUnderTeacher = new List<cls_Student_VO>();

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT " +
                    "students.student_id AS ID," +
                    " students.first_name AS Student_FirstName," +
                    " students.last_name AS Student_LastName," +
                    " students.gender AS Student_Gender," +
                    " students.city AS Student_City," +
                    " students.email AS Student_Email," +
                    " students.phone_number AS Student_PhoneNumber," +
                    " students.date_of_birth AS Students_DOB" +
                    " FROM StudentTeachers LEFT JOIN Teachers " +
                    "ON StudentTeachers.T_ID = Teachers.Teacher_ID " +
                    "LEFT JOIN students " +
                    "ON StudentTeachers.S_ID = students.student_id" +
                    " WHERE StudentTeachers.T_ID = @id" +
                    " ORDER BY students.student_id";



                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("id", teacherId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cls_Student_VO student_VO = new cls_Student_VO()
                            {
                                StudentID = (int)reader["ID"],
                                FirstName = reader["Student_FirstName"] as string,
                                LastName = reader["Student_LastName"] as string,
                                City = reader["Student_City"] as string,
                                Email = reader["Student_Email"] as string,
                                Gender = Convert.ToChar(reader["Student_Gender"]),
                                PhoneNumber = reader["Student_PhoneNumber"] as string
                            };
                            if (reader["Students_DOB"] != DBNull.Value)
                            {
                                student_VO.DOB = Convert.ToDateTime(reader["Students_DOB"]);
                            }
                            else
                            {
                                student_VO.DOB = null;
                            }

                            studentsUnderTeacher.Add(student_VO);

                        }

                        return studentsUnderTeacher;
                    }

                }
            }
        }

        public bool MapStudentWithTeacher(string[] studentIds, int? teacherID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();

                // Formatting Query
                string partialQuery1 = "INSERT INTO StudentTeachers (S_ID,T_ID) VALUES ";
                string partialQuery2 = "";

                for (int i = 0; i < studentIds.Length; i++)
                {
                    if (i == 0)
                    {
                        partialQuery2 += $"(@StdID{i},@TeacherID)";

                    }

                    else
                    {
                        partialQuery2 += $",(@StdID{i},@TeacherID)";
                    }
                }

                string query = partialQuery1 + partialQuery2;
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    // Adding paramters To Prevent SQL Injection Attacks
                    for (int i = 0; i < studentIds.Length; i++)
                    {
                        cmd.Parameters.AddWithValue($"StdID{i}", studentIds[i]);
                    }
                    cmd.Parameters.AddWithValue("TeacherID", teacherID);

                    return cmd.ExecuteNonQuery() > 0;
                }

            }
        }

        public bool DeleteMapping(int? teacherID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "DELETE FROM StudentTeachers WHERE T_ID=@id";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("id", teacherID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateMapping(string[] studentIds, int? teacherID)
        {
            if(DeleteMapping(teacherID) && MapStudentWithTeacher(studentIds, teacherID))
            {
                return true;
            }
            return false;
        }
    }
}

