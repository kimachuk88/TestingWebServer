using System;
using NUnit.Framework;
using TestingWebServer.Tools;

namespace TestingWebServer
{
    [TestFixture]
    public class FirstVarTests
    {
        TestRunner testrunner = new TestRunner();
        
        //Admin credentials
        string name = "admin";
        string password = "qwerty";
        string token;

        [Test]
        public void LoginAndLogout()
        {
            //Login as admin
            token = testrunner.Login(name, password);
            
            //Verify there is a token by length
            Assert.AreEqual(32, token.Length);
            
            //Logout from admin account and verify that action being done
            Assert.IsTrue(testrunner.Logout(name, token));
        }

        [Test]
        public void CreateUser()
        {
            //Login as admin
            token = testrunner.Login(name, password);

            //Create and verify creation of "lv317" account
            Assert.IsTrue(testrunner.Create(token, "lv317", "1234"));

            //Logout from admin account and verify that action being done
            Assert.IsTrue(testrunner.Logout(name, token));

            //Login with "lv317" and get it's token
            token = testrunner.Login("lv317", "1234");

            //Verify if we logged as newly created "lv317"
            string createdName = testrunner.GetUserName(token);
            Assert.AreEqual("lv317", createdName);
            
            //Finish test and Logout from "lv317" account
            Assert.IsTrue(testrunner.Logout("lv317", token));
        }

        [Test]
        public void VievAllUsers()
        {
            //Login as admin
            token = testrunner.Login(name, password);

            //string allUsers = testrunner.GetAllUsers(token);
            //Assert.Contains("akimatc", testrunner.GetAllUsers(token));

            //Finish test and logout from admin
            testrunner.Logout(name, token);
        }

        [Test]
        public void RemoveUser()
        {
            //Login again as admin and get it's token
            token = testrunner.Login(name, password);

            //Remove "lv317" account and verify it is done
            Assert.IsTrue(testrunner.RemoveUser(token, "lv317"));
        }
    }

}
