using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RunnersTracker.DataAccess;
using RunnersTracker.Business.DTO;
using RunnersTrackerDB;
using RunnersTracker.Common;
using AutoMapper;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Threading;
using System.Net.Mail;
using System.ComponentModel;

namespace RunnersTracker.Business.Service
{
    public class LoginService
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IUnitOfWork unitOfWork = new UnitOfWork();
        
        public UserDTO Login(string email, string password)
        {
            UserDTO userDTO = new UserDTO();
            
            var users = unitOfWork.UserRepository.Get(u => u.Email.Equals(email));
            User userEntity = users.First();

            if (userEntity == null || !userEntity.AccountConfirmed)
            {
                return null;
            }
            else
            {
                
                Mapper.CreateMap<User, UserDTO>();
                userDTO = Mapper.Map<User, UserDTO>(userEntity);
                byte[] submittedPassword = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes(password), userDTO.Salt);
                if (PasswordManagement.ComparePasswords(userDTO.Password, submittedPassword))
                {
                    return userDTO;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool ResetPassword(string email)
        {
            UserDTO userDto = new UserDTO();
            var users = unitOfWork.UserRepository.Get(u => u.Email.Equals(email));
            User userEntity = users.First();

            if (userEntity != null)
            {
                Mapper.CreateMap<User, UserDTO>();
                UserDTO user = Mapper.Map<User, UserDTO>(userEntity);
                                
                user.PassResetCode = PasswordManagement.RandomString(26, true);
                user.PassResetExpire = DateTime.Now.AddMinutes(10);

                Mapper.CreateMap<UserDTO, User>();
                User userUpdated = Mapper.Map<UserDTO, User>(user);
                unitOfWork.UserRepository.Update(userUpdated, userEntity);
                unitOfWork.Save();
                SendEmail(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool NewPassword(string password, string code)
        {

            var users = unitOfWork.UserRepository.Get(u => u.PassResetCode.Equals(code));
            User userEntity = users.First();
            if (userEntity != null)
            {
                Mapper.CreateMap<User, UserDTO>();
                UserDTO user = Mapper.Map<User, UserDTO>(userEntity);
                if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(user.PassResetExpire)) < 0)
                {
                    user.Salt = PasswordManagement.GenerateSalt();
                    user.Password = PasswordManagement.GenerateSaltedPassword(Encoding.UTF8.GetBytes(password), user.Salt);
                    user.PassResetCode = null;
                    user.PassResetExpire = null;
                    Mapper.CreateMap<UserDTO, User>();
                    User userUpdated = Mapper.Map<UserDTO, User>(user);
                    unitOfWork.UserRepository.Update(userUpdated, userEntity);
                    unitOfWork.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }            
        }
        
        private static void SendEmail(UserDTO user)
        {
            //use thread pool to prevent the controller from blocking the SendAsync method
            ThreadPool.QueueUserWorkItem(t =>
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient();

                mail.To.Add(user.Email);
                mail.Subject = "Runner's Tracker Password Reset";
                mail.IsBodyHtml = true;
                mail.Body = createResetPasswordEmail(user);

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

        private static string createResetPasswordEmail(UserDTO user)
        {

            using (var sw = new MemoryStream())
            {
                using (var xw = XmlWriter.Create(sw))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("password");
                    xw.WriteStartElement("firstName");
                    xw.WriteString(user.FirstName);
                    xw.WriteEndElement();

                    xw.WriteStartElement("resetLink");
                    xw.WriteString("http://localhost/login/newpassword/" + user.PassResetCode);
                    xw.WriteEndElement();

                    xw.WriteEndElement();
                    xw.WriteEndDocument();

                }
                sw.Position = 0; //set to beginning of stream before reading
                XPathDocument xpath = new XPathDocument(sw);

                XslCompiledTransform myXslTrans = new XslCompiledTransform();
                logger.Info(AppDomain.CurrentDomain.BaseDirectory);
                myXslTrans.Load(AppDomain.CurrentDomain.BaseDirectory + "..\\Business\\MailTemplates\\resetPassTemplate.xslt");
                using (var ms = new MemoryStream())
                {
                    myXslTrans.Transform(xpath, null, ms);
                    ms.Position = 0; //set tp beginning of stream before reading
                    StreamReader reader = new StreamReader(ms);
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
