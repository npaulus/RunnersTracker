using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RunnersTracker.WebUI.Models;
using System.Net.Mail;
using System.Threading;
using log4net;
using System.ComponentModel;

namespace RunnersTracker.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(HomeController));
        //
        // GET: /Home/        
        public ActionResult Index()
        {
            ViewBag.Title = "Runner's Tracker";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsModel model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                return RedirectToAction("ContactUsComplete", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ContactUsComplete()
        {
            return View("ContactUsComplete");
        }

        private static void SendEmail(ContactUsModel model)
        {
            //use thread pool to prevent the controller from blocking the SendAsync method
            ThreadPool.QueueUserWorkItem(t =>
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient();

                mail.To.Add("nate@natepaulus.com");
                mail.Subject = "Runner's Tracker User Feedback";
                mail.IsBodyHtml = false;
                mail.Body = "From: " + model.FromName + "\n\n" +model.Comment; 

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
