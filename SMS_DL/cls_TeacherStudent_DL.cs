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
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read() && count!=1)
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
    }
}
