using SMS_VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMS_DL
{
    public class cls_User_DL
    {
        private readonly string connectionString;
        public cls_User_DL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddUser(cls_User_VO user)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "INSERT INTO Users(UserName,UserType,Password) VALUES (@name,@type,HASHBYTES(SHA2_256,@password)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("name", user.UserName);
                    cmd.Parameters.AddWithValue("type", user.UserType);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool AuthenticateUser(cls_User_VO user)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT * FROM Users WHERE Username=@name";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("name", user.UserName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        byte[] enteredPassword = Hash(user.Password);
                        while (reader.Read())
                        {
                            byte[] storedPpassword = reader["Password"] as byte[];
                            if (enteredPassword.SequenceEqual(storedPpassword))
                            {
                                return true;
                            }
                            return false;

                        }
                    }
                }

                return false;
            }
        }

        public string[] GetUserRole(string userName)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT UserType FROM Users WHERE Username=@name";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("name", userName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<String> roles = new List<String>();
                        while (reader.Read())
                        {
                            roles.Add(reader["UserType"] as string);

                        }
                        return roles.ToArray();
                    }
                }
            }

        }

        public byte[] Hash(string password)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                return sHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
