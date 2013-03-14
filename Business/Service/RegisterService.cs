using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RunnersTracker.Common;
using RunnersTracker.Business.DTO;
using AutoMapper;
using RunnersTracker.DataAccess;
using RunnersTrackerDB;
using System.Net.Mail;
using log4net;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Net.Configuration;
using System.Configuration;

namespace RunnersTracker.Business.Service
{
    public class RegisterService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(RegisterService));
        
        UserDAC userDac = new UserDAC();

        public bool createNewUser(UserDTO user)
        {
            user.ConfirmCode = RandomString(26, true);
            logger.Info("Testing the logger");
            if (userDac.getUser(user.Email)) //check if user already exists
            {
                return false;
            }
            else
            {
                createPassword(user);
                Mapper.CreateMap<UserDTO, User>();
                User userEntity = Mapper.Map<UserDTO, User>(user);
                userEntity.DistanceType = "Miles";
                userDac.Save(userEntity);
                SendEmail(user);
                logger.Info("send email here if this works");
            }

            return true;
        }



        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private UserDTO createPassword(UserDTO user)
        {
            user.Salt = PasswordManagement.GenerateSalt();
            byte[] pass = user.Password;
            user.Password = PasswordManagement.GenerateSaltedPassword(pass, user.Salt);

            return user;
        }

        private static void SendEmail(UserDTO user)
        {
            //use thread pool to prevent the controller from blocking the SendAsync method
            ThreadPool.QueueUserWorkItem(t =>
            {
                MailMessage mail = new MailMessage();
                
                SmtpClient SmtpServer = new SmtpClient();

                mail.To.Add(user.Email);
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GoDaddy";

                SmtpServer.SendCompleted += delegate(object sender, AsyncCompletedEventArgs e)
                {
                    if (e.Error != null)
                    {
                        System.Diagnostics.Trace.TraceError(e.Error.ToString());
                        logger.Info(e.Error.ToString());
                    }
                    MailMessage userMessage = e.UserState as MailMessage;
                    if (userMessage != null)
                    {
                        userMessage.Dispose();
                    }
                };
                SmtpServer.SendAsync(mail, mail);
            });
        }
    }
}
