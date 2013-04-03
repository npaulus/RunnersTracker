using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunnersTracker.Common;
using System.Text;
using RunnersTracker.DataAccess;
using Moq;
using System.Linq.Expressions;
using RunnersTrackerDB;
using System.Collections.Generic;
using RunnersTracker.Business.Service.Impl;
using RunnersTracker.Business.DTO;

namespace RunnersTracker.UnitTests
{
    [TestClass]
    public class RegisterServiceTest
    {
        
        [TestMethod]
        public void CreateUser_UserDoesExist_ReturnsFalse()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass },                
            });

            RegisterService registerService = new RegisterService(mock.Object);

            UserDTO testUser = new UserDTO();
            testUser.Email = "test4@test.com";

            bool result = registerService.createNewUser(testUser);
            
            Assert.IsFalse(result);        
        }

        [TestMethod]
        public void CreateUser_UserDoesNotExist_ReturnsTrue()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>());

            RegisterService registerService = new RegisterService(mock.Object);

            UserDTO testUser = new UserDTO();
            testUser.Email = "test4@test.com";
            testUser.Password = pass;

            bool result = registerService.createNewUser(testUser);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ConfirmUser_ValidCode_ReturnsTrue()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass, ConfirmCode = "code", AccountConfirmed = false },                
            });

            RegisterService registerService = new RegisterService(mock.Object);

            bool result = registerService.ConfirmUser("code");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ConfirmUser_InvalidCode_ReturnsFalse()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>());

            RegisterService registerService = new RegisterService(mock.Object);

            bool result = registerService.ConfirmUser("code");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ResendConfirmationLink_InvalidEmail_ReturnsFalse()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>());

            RegisterService registerService = new RegisterService(mock.Object);

            bool result = registerService.ResendConfirmationLink("test@test.com");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ResendConfirmationLink_ValidEmail_ReturnsTrue()
        {
            byte[] salt = PasswordManagement.GenerateSalt();
            byte[] pass = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes("Password"), salt);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(
                new List<User>
            {
                new User { UserId = 4, FirstName = "Test4", LastName = "LastName", Email = "test4@test.com", Salt = salt, Password = pass, ConfirmCode = "code", AccountConfirmed = false },                
            });

            RegisterService registerService = new RegisterService(mock.Object);

            bool result = registerService.ResendConfirmationLink("test4@test.com");

            Assert.IsTrue(result);
        }
    }
}
