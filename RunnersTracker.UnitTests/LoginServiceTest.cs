using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RunnersTracker.DataAccess;
using RunnersTracker.Business.DTO;
using RunnersTracker.Business.Service.Impl;
using RunnersTracker.Common;
using System.Text;
using System.Linq.Expressions;
using RunnersTrackerDB;
using System.Collections.Generic;


namespace RunnersTracker.UnitTests
{
    [TestClass]
    public class LoginServiceTest
    {
                
        [TestMethod]
        public void Login_UserDoesNotExist_ReturnsNull()
        {
            TimeSpan ts = new TimeSpan(1, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass, AccountConfirmed = true, PassResetCode = "test1", PassResetExpire = new Nullable<DateTime>(DateTime.Now.Add(ts)) },                
            });

            LoginService ls = new LoginService(mock.Object);

            UserDTO user = ls.Login("nate@test.com", "test123");

            Assert.IsNull(user);

        }

        [TestMethod]
        public void Login_UserWrongPassword_ReturnsNull()
        {
            TimeSpan ts = new TimeSpan(1, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass, AccountConfirmed = true, PassResetCode = "test1", PassResetExpire = new Nullable<DateTime>(DateTime.Now.Add(ts)) },                
            });

            LoginService ls = new LoginService(mock.Object);

            UserDTO user = ls.Login("test4@test.com", "test123");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void Login_UserNotConfirmed_ReturnsNull()
        {
            TimeSpan ts = new TimeSpan(1, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass, AccountConfirmed = false, PassResetCode = "test1", PassResetExpire = new Nullable<DateTime>(DateTime.Now.Add(ts)) },                
            });

            LoginService ls = new LoginService(mock.Object);

            UserDTO user = ls.Login("test4@test.com", "Password");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void Login_UserValidLogin_ReturnsUserDTO()
        {
            TimeSpan ts = new TimeSpan(1, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass, AccountConfirmed = true, PassResetCode = "test1", PassResetExpire = new Nullable<DateTime>(DateTime.Now.Add(ts)) },                
            });

            LoginService ls = new LoginService(mock.Object);

            UserDTO user = ls.Login("test4@test.com", "Password");

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void ResetPassword_ValidUserResetsPassword_ReturnsTrue()
        {
            TimeSpan ts = new TimeSpan(1, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass, AccountConfirmed = true, PassResetCode = "test1", PassResetExpire = new Nullable<DateTime>(DateTime.Now.Add(ts)) },                
            });

            LoginService ls = new LoginService(mock.Object);

            bool passwordReset = ls.ResetPassword("test4@test.com");

            Assert.IsTrue(passwordReset);
        }

        [TestMethod]
        public void ResetPassword_InvalidUserResetsPassword_ReturnsFalse()
        {
            TimeSpan ts = new TimeSpan(1, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(new List<User>());

            LoginService ls = new LoginService(mock.Object);

            bool passwordReset = ls.ResetPassword("tes@test.com");

            Assert.IsFalse(passwordReset);
        }

        [TestMethod]
        public void NewPassword_ValidResetCode_ReturnsTrue()
        {
            TimeSpan ts = new TimeSpan(1, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);
            
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass, AccountConfirmed = true, PassResetCode = "test1", PassResetExpire = new Nullable<DateTime>(DateTime.Now.Add(ts)) },                
            });

            LoginService ls = new LoginService(mock.Object);

            bool newPassword = ls.NewPassword("newpass", "test1");

            Assert.IsTrue(newPassword);
        }

        [TestMethod]
        public void NewPassword_InValidResetCode_ReturnsFalse()
        {
            TimeSpan ts = new TimeSpan(1, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(new List<User>());

            LoginService ls = new LoginService(mock.Object);

            bool newPassword = ls.NewPassword("newpass", "test");

            Assert.IsFalse(newPassword);
        }

        [TestMethod]
        public void NewPassword_ExpiredResetCode_ReturnsFalse()
        {
           
            TimeSpan ts = new TimeSpan(0, 5, 0);
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);
            List<User> users = new List<User>
            {
                new User { UserId = 5, FirstName = "Test5", LastName = "LastName", Email = "test5@test.com", Salt = salt, Password = pass, AccountConfirmed = true, PassResetCode = "test2", PassResetExpire = DateTime.Now.Subtract(ts) }
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(users);

            LoginService ls = new LoginService(mock.Object);
            
            bool newPassword = ls.NewPassword("newpass", "test2");

            Assert.IsFalse(newPassword);
        }
    }
}
