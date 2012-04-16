using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Security;
using BookShopModel;
using BookShopModel.ContainerMock;
using BookShopModel.Model;

namespace BookShopTest
{
    [TestClass]
    public class ModelTests
    {
        Repository repo;
        [TestInitialize]
        public void MyTestInitialize()
        {
            repo = new Repository(new ModelContainerMock());
        }
        [TestMethod]
        public void CanCreateUser()
        {
            MembershipCreateStatus result = repo.CreateUser("TestUser", "TestPassword", "TestEmail");
            Assert.AreEqual(MembershipCreateStatus.Success, result, "Ошибка создания юзера");
            List<User> users = repo.modelContainer.Users.ToList();
            Assert.AreEqual(1, users.Count(), "Неверное кол-во юзеров");
            Assert.AreEqual("TestUser", users.FirstOrDefault().UserName);
        }
        [TestMethod]
        public void CantCreateDuplicateUser()
        {
            repo.CreateUser("TestUser", "TestPassword", "TestEmail");
            MembershipCreateStatus result = repo.CreateUser("TestUser", "TestPassword2", "TestEmail");
            Assert.AreEqual(MembershipCreateStatus.DuplicateUserName, result, "Дублированный юзер создался");
            List<User> users = repo.modelContainer.Users.ToList();
            Assert.AreEqual(1, users.Count(), "Неверное кол-во юзеров");
        }
        [TestMethod]
        public void CantCreateUserWithDuplicateEmail()
        {
            repo.CreateUser("TestUser", "TestPassword", "TestEmail");
            MembershipCreateStatus result = repo.CreateUser("TestUser2", "TestPassword2", "TestEmail");
            Assert.AreEqual(MembershipCreateStatus.DuplicateEmail, result, "Юзер c дублированным EMail создался");
            List<User> users = repo.modelContainer.Users.ToList();
            Assert.AreEqual(1, users.Count(), "Неверное кол-во юзеров");
        }
        [TestMethod]
        public void CanAuthenticateUserAfterCreation()
        {
            repo.CreateUser("TestUser", "TestPassword", "TestEmail");
            string correctUserName;
            bool result = repo.Authenticate("TestUser", "TestPassword", out correctUserName);
            Assert.AreEqual(true, result);
            Assert.AreEqual("TestUser", correctUserName);
        }
        [TestMethod]
        public void CantAuthenticateWrongUser()
        {
            repo.CreateUser("TestUser", "TestPassword", "TestEmail");
            string correctUserName;
            bool result = repo.Authenticate("WrongTestUser", "TestPassword", out correctUserName);
            Assert.AreEqual(false, result);
            result = repo.Authenticate("TestUser", "WrongTestPassword", out correctUserName);
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CanGetUserByName()
        {
            repo.CreateUser("TestUser", "TestPassword", "TestEmail");
            repo.CreateUser("TestUser2", "TestPassword2", "TestEmail2");
            User user = repo.GetUser("TestUser2");
            Assert.IsNotNull(user);
            Assert.AreEqual("TestUser2", user.UserName);
        }
        [TestMethod]
        public void CantGetNotExistUserByName()
        {
            repo.CreateUser("TestUser", "TestPassword", "TestEmail");
            repo.CreateUser("TestUser2", "TestPassword2", "TestEmail");
            User user = repo.GetUser("TestUser3");
            Assert.IsNull(user);
        }
    }
}
