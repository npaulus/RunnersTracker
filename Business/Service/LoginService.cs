using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RunnersTracker.DataAccess;
using RunnersTracker.Business.DTO;
using RunnersTrackerDB;
using RunnersTracker.Common;
using AutoMapper;

namespace RunnersTracker.Business.Service
{
    public class LoginService
    {
        UserDAC userDac = new UserDAC();

        public bool Login(string email, string password)
        {
            UserDTO userDTO = new UserDTO();
            User userEntity = userDac.RetrieveUser(email);

            if (userEntity == null)
            {
                return false;
            }
            else
            {
                Mapper.CreateMap<User, UserDTO>();
                userDTO = Mapper.Map<User, UserDTO>(userEntity);
                byte[] submittedPassword = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes(password), userDTO.Salt);
                return PasswordManagement.ComparePasswords(userDTO.Password, submittedPassword);
            }
        }
    }
}
