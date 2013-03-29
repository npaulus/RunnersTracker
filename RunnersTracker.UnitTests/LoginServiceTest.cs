using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RunnersTracker.Business.Service;
using RunnersTracker.DataAccess;
using RunnersTracker.Business.DTO;
using RunnersTracker.Common;
using System.Text;


namespace RunnersTracker.UnitTests
{
    [TestClass]
    public class LoginServiceTest
    {
        [TestMethod]
        public void LoginUserDoesNotExist()
        {
            //arrange                       
            var mock = new Mock<LoginService>();
            LoginService ls = mock.Object;
            UserDTO user = ls.Login("test@test.com", "pass");
            Assert.IsNull(user);            
            
        }

        public void LoginUserWrongPassword()
        {

        }

        public void LoginUserValidLogin()
        {

        }

    }
}
