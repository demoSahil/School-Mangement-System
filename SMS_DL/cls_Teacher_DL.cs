using SMS_VO;
using SMS_VO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_DL
{
    public class cls_Teacher_DL
    {
        private readonly string connectionString;

        public cls_Teacher_DL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<cls_Teacher_VO> GetAllTeachersList()
        {
            List<cls_Teacher_VO> teachers = new List<cls_Teacher_VO>();

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT * FROM Teachers";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cls_Teacher_VO teacher = new cls_Teacher_VO()
                            {
                                TeacherId = (int)reader["Teacher_ID"],
                                TeacherName = reader["Teacher_Name"] as string,
                                Subject = reader["Subject"] as string,
                                ContactNumber = reader["Contact_Number"] as string,
                            };

                            teachers.Add(teacher);
                        }
                    }
                }
            }

            return teachers;
        }

        public bool InsertTeacherDetails(cls_Teacher_VO teacher)
        {

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "INSERT INTO Teachers" +
                    "(Teacher_Name,Subject,Contact_Number)" +
                    "VALUES" +
                    "(@name,@subject,@number)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("name", teacher.TeacherName);
                    cmd.Parameters.AddWithValue("subject", teacher.Subject);
                    cmd.Parameters.AddWithValue("number", teacher.ContactNumber);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteTeacherDetails(int? id)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "DELETE FROM Teachers WHERE Teacher_ID=@ID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("ID", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

        }

        public bool UpdateTeacherDetails(cls_Teacher_VO teacherToBeUpdated)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "UPDATE Teachers " +
                    "SET " +
                    "Teacher_Name=@name," +
                    "Subject=@subject," +
                    "Contact_Number=@number " +
                    "WHERE Teacher_ID=@id";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("name", teacherToBeUpdated.TeacherName);
                    cmd.Parameters.AddWithValue("subject", teacherToBeUpdated.Subject);
                    cmd.Parameters.AddWithValue("number", teacherToBeUpdated.ContactNumber);
                    cmd.Parameters.AddWithValue("id", teacherToBeUpdated.TeacherId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


    }
}
