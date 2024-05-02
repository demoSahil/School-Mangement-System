using SMS_VO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SMS_DL
{
    public class cls_Students_DL
    {

        private readonly string connectionString;

        public cls_Students_DL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<cls_Student_VO> GetStudentsList()
        {
            List<cls_Student_VO> students = new List<cls_Student_VO>();

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT * FROM students";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cls_Student_VO student_VO = new cls_Student_VO()
                            {
                                StudentID = (int)reader["student_id"],
                                FirstName = reader["first_name"] as string,
                                LastName = reader["last_name"] as string,
                                City = reader["city"] as string,
                                Email = reader["email"] as string,
                                Gender = Convert.ToChar(reader["gender"]),
                                PhoneNumber = reader["phone_number"] as string
                            };
                            if (reader["date_of_birth"] != DBNull.Value)
                            {
                                student_VO.DOB = Convert.ToDateTime(reader["date_of_birth"]);
                            }
                            else
                            {
                                student_VO.DOB = null;
                            }


                            students.Add(student_VO);
                        }
                    }
                }
            }

            return students;
        }
        public int? GetLastId()
        {
            int? maxSerialNo = 0;
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT MAX(student_id) AS Max FROM students";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maxSerialNo = Convert.ToInt32(reader["Max"]);
                        }
                    }
                }
            }
            return maxSerialNo;

        }
        public bool InsertStudentDetails(cls_Student_VO student)
        {

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "INSERT INTO students" +
                    "(student_id,first_name,last_name,date_of_birth,gender,city,email,phone_number)" +
                    "VALUES" +
                    "(@id,@firstName,@lastName,@dob,@gender,@city,@email,@phone_number)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("id", GetLastId() + 1);
                    cmd.Parameters.AddWithValue("firstName", student.FirstName);
                    cmd.Parameters.AddWithValue("lastName", student.LastName);
                    if (student.DOB == null)
                    {
                        cmd.Parameters.AddWithValue("dob", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("dob", student.DOB);

                    }

                    cmd.Parameters.AddWithValue("gender", student.Gender);
                    cmd.Parameters.AddWithValue("city", student.City);
                    cmd.Parameters.AddWithValue("email", student.Email);
                    cmd.Parameters.AddWithValue("phone_number", student.PhoneNumber);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteStudentDetails(int? id)
        {

            //first i have to check whether this exist in students teachers table or not


            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "DELETE FROM students WHERE student_id=@ID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("ID", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

        }

        public bool UpdateStudentDetails(cls_Student_VO studentToBeUpdated)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "UPDATE students " +
                    "SET " +
                    "first_name=@name," +
                    "last_name=@lastname," +
                    "city=@city," +
                    "email=@email," +
                    "gender=@gender," +
                    "phone_number=@phoneNumber " +
                    "WHERE student_id=@id";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("name", studentToBeUpdated.FirstName);
                    cmd.Parameters.AddWithValue("lastname", studentToBeUpdated.LastName);
                    cmd.Parameters.AddWithValue("city", studentToBeUpdated.City);
                    cmd.Parameters.AddWithValue("email", studentToBeUpdated.Email);
                    cmd.Parameters.AddWithValue("gender", studentToBeUpdated.Gender);
                    cmd.Parameters.AddWithValue("phoneNumber", studentToBeUpdated.PhoneNumber);
                    cmd.Parameters.AddWithValue("id", studentToBeUpdated.StudentID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

       /* public int? GetTotalStudents()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT COUNT(student_id) AS StudentCount FROM  students";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }

            return null;
        }*/
    }
}
