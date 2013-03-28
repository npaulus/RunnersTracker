using System;
using System.Text;
using System.Linq;
using RunnersTracker.Common;
using RunnersTracker.Business.DTO;
using AutoMapper;
using RunnersTracker.DataAccess;
using RunnersTrackerDB;
using System.Net.Mail;
using log4net;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace RunnersTracker.Business.Service
{
    public class RegisterService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(RegisterService));
        private UnitOfWork unitOfWork = new UnitOfWork();

        public bool createNewUser(UserDTO user)
        {
            user.ConfirmCode = PasswordManagement.RandomString(26, true);
            
            var users = unitOfWork.UserRepository.Get(u => u.Email.Equals(user.Email));
            
            if (users.Count() > 0) //check if user already exists
            {
                return false;
            }
            else
            {
                createPassword(user);
                Mapper.CreateMap<UserDTO, User>();
                User userEntity = Mapper.Map<UserDTO, User>(user);
                userEntity.DistanceType = "Miles";
                unitOfWork.UserRepository.Insert(userEntity);
                unitOfWork.Save();                
                SendEmail(user);                
            }
            return true;
        }

        public bool ConfirmUser(string code)
        {
            var users = unitOfWork.UserRepository.Get(u => u.ConfirmCode.Equals(code));            

            if (users.Count() == 1)
            {
                User user = users.First();
                Mapper.CreateMap<User, UserDTO>();
                UserDTO userDto = Mapper.Map<User, UserDTO>(user);
                userDto.ConfirmCode = "";
                userDto.AccountConfirmed = true;

                Mapper.CreateMap<UserDTO, User>();
                User userEntity = Mapper.Map<UserDTO, User>(userDto);
                
                unitOfWork.UserRepository.Update(userEntity, user);
                unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }            
        }

        public bool ResendConfirmationLink(string email)
        {
            logger.Info("Inside ResendConfirmationLink method");
            bool result = false;
            var users = unitOfWork.UserRepository.Get(u => u.Email.Equals(email));
            
            if (users.Count() == 1)
            {
                User userEntity = users.First();
                Mapper.CreateMap<User, UserDTO>();
                UserDTO userDto = Mapper.Map<User, UserDTO>(userEntity);
                logger.Info("User email: " + userDto.Email);
                logger.Info("User code: " + userDto.ConfirmCode);
                SendEmail(userDto);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
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
                mail.Subject = "Welcome to Runner's Tracker!";
                mail.IsBodyHtml = true;
                mail.Body = createWelcomeEmail(user);

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

        private static string createWelcomeEmail(UserDTO user)
        {
            
            using (var sw = new MemoryStream())
            {
                using (var xw = XmlWriter.Create(sw))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("welcomeEmail");
                    xw.WriteStartElement("firstName");
                    xw.WriteString(user.FirstName);
                    xw.WriteEndElement();

                    xw.WriteStartElement("confirmLink");
                    xw.WriteString("http://localhost/register/confirm/" + user.ConfirmCode);
                    xw.WriteEndElement();

                    xw.WriteEndElement();
                    xw.WriteEndDocument();

                }
                sw.Position = 0; //set to beginning of stream before reading
                XPathDocument xpath = new XPathDocument(sw);               

                XslCompiledTransform myXslTrans = new XslCompiledTransform();
                logger.Info(AppDomain.CurrentDomain.BaseDirectory);
                myXslTrans.Load(AppDomain.CurrentDomain.BaseDirectory+"..\\Business\\MailTemplates\\emailTemplate.xslt");
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
