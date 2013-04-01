using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RunnersTracker.Business.Service;
using RunnersTracker.DataAccess;
using RunnersTracker.Business.DTO;
using RunnersTracker.Business.Service.Impl;
using RunnersTracker.Common;
using System.Text;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using RunnersTrackerDB;
using System.Linq;
using System.Collections.Generic;


namespace RunnersTracker.UnitTests
{
    [TestClass]
    public class LoginServiceTest
    {
        [TestMethod]
        public void LoginUserDoesNotExist()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);
            IUnitOfWork uowTest = new UnitOfWorkTest(pass, salt);

            LoginService ls = new LoginService(uowTest);

            UserDTO user = ls.Login("nate@test.com", "test123");

            Assert.IsNull(user);

        }

        [TestMethod]
        public void LoginUserWrongPassword()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);
            IUnitOfWork uowTest = new UnitOfWorkTest(pass, salt);

            LoginService ls = new LoginService(uowTest);

            UserDTO user = ls.Login("test@test.com", "test123");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void LoginUserNotConfirmed()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);
            IUnitOfWork uowTest = new UnitOfWorkTest(pass, salt);

            LoginService ls = new LoginService(uowTest);

            UserDTO user = ls.Login("test2@test.com", "Password");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void LoginUserValidLogin()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);
            IUnitOfWork uowTest = new UnitOfWorkTest(pass, salt);

            LoginService ls = new LoginService(uowTest);

            UserDTO user = ls.Login("test@test.com", "Password");

            Assert.IsNotNull(user);
        }
    }
}
