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
                string query = "INSERT INTO Users(UserName,UserType,Password) VALUES (@name,@type,@password)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("name", user.UserName);
                    cmd.Parameters.AddWithValue("type", user.UserType);
                    cmd.Parameters.AddWithValue("password", Hash(user.Password));
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
                            byte[] storedPassword = reader["Password"] as byte[];
                            string userType = reader["UserType"] as string;
                            if (enteredPassword.SequenceEqual(storedPassword))
                            {
                                if (userType == user.UserType)
                                {
                                    return true;
                                }
                                user.ErrorMessage = "Invalid User Type";
                                return false;
                            }

                            else
                            {
                                user.ErrorMessage = "Invalid Password";
                                return false;
                            }

                        }
                    }
                }

                user.ErrorMessage = "Invalid Username";
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
