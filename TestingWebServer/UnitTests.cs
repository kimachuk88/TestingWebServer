using NUnit.Framework;
using TestingWebServer.Tools;

namespace TestingWebServer
{
    [TestFixture]
    public class UnitTests : TestRunner
    {
        //Admin credentials
        string name = "admin";
        string password = "qwerty";
        string token;

        [Test]
        public void TstPlannedBehaviour()
        {

            //Login as admin
            token = Login(name, password);

            //Verify there is a token by length
            Assert.AreEqual(32, token.Length);

            //Create and verify creation of "lv317" account
            Assert.IsTrue(Create(token, "lv317", "1234"), "Unable to create new user");

            //Logout from admin account and verify that action being done
            Assert.IsTrue(Logout(name, token), "Unable to logout from admin");

            //Login as "lv317" and get it's token
            token = Login("lv317", "1234");

            //Verify if we logged as newly created "lv317"
            string createdName = GetUserName(token);
            Assert.AreEqual("lv317", createdName);

            //Verify that "lv317" user is in the All Users list
            StringAssert.Contains("lv317", GetAllUsers(token));

            //Logout from "lv317" account
            Assert.IsTrue(Logout("lv317", token),"Unable to logout from lv317");

            //Login as admin
            token = Login(name, password);

            //Verify that "lv317" user is in the All Users list
            StringAssert.Contains("akimatc", GetAllUsers(token));

            //Remove "lv317" account and verify it is done
            Assert.IsTrue(RemoveUser(token, "lv317"),"Unable to remove user");

            //Verify that "lv317" user is deleted from the All users list
            StringAssert.DoesNotContain("lv317", GetAllUsers(token));

            //Logout from admin account and verify that action being done
            Assert.IsTrue(Logout(name, token),"Unable to logout from admin");

        }

        [Test]
        public void TstEmptyNamePswrd()
        {

            //Login as admin
            token = Login(name, password);

            //Verify impossibility of creation account without name and password
            Assert.IsFalse(Create(token,"", ""), "New user with empty name and password created");

        }
        
        [Test]
        public void TstExistingUser()
        {

            //Login as admin
            token = Login(name, password);

            //Verify impossibility of creation user with same name as existing
            Assert.IsFalse(Create(token, "akimatc", "1234"), "User with existing name created");

        }

        [Test]
        public void TstAdminName()
        {

            //Login as admin
            token = Login(name, password);

            //Verify impossibility of creation user with admin name 
            Assert.IsFalse(Create(token, "admin", "1234"), "User with admin name created");
        }
    }
}
