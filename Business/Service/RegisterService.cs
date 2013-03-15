using System;
using System.Text;
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
        
        UserDAC userDac = new UserDAC();

        public bool createNewUser(UserDTO user)
        {
            user.ConfirmCode = RandomString(26, true);
            
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
                //userDac.Save(userEntity);
                SendEmail(user);
                
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
