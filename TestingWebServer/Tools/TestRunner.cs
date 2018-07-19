using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWebServer.Tools
{
    class TestRunner
    {
        ServiceReference.CommonClient common = new ServiceReference.CommonClient();
        string token;

        public string Login(string name, string password)
        {
            token = common.login(name, password);
            return token;
        }

        public bool Create(string token, string name, string password)
        {
            return common.createUser(token, name, password, true);

        }

        public bool Logout(string name, string token)
        {
            return common.logout(name, token);
        }

        public string GetUserName(string token)
        {
            return common.getUserName(token);
        }

        public String GetAllUsers(string token)
        {
            return common.getAllUsers(token);
        }

        public bool RemoveUser(string token, string username)
        {
            return common.removeUser(token, username);
        }
    }
}
