using Microsoft.SqlServer.Server;
using SMS_DL;
using SMS_VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_BM
{
    public class cls_User_BM
    {
        cls_User_DL _userDL;

        public cls_User_BM(string connectionString)
        {
            _userDL = new cls_User_DL(connectionString);
        }
        
        public bool Add(cls_User_VO user)
        {
            return _userDL.AddUser(user);
        }
        public string[] GetRolesForUser(string userName)
        {
            return _userDL.GetUserRole(userName);
        }

        public bool Authenticate(cls_User_VO user)
        {
            return _userDL.AuthenticateUser(user);  
        }
    }
}
