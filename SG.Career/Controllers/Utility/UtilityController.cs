using SG.Career.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SG.Career.Controllers.Utility
{
    public class UtilityController : Controller
    {
        // GET: Utility
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendEmail()
        {
            int rs = 0;

            string _host = "smtp.office365.com";
            int _port = 587; //기본 smtp port : 25 , office365 587
            string _userId = "";
            string _userPassword = "";
            string fromEmail = "sanggeun.choi@marineworks.co.kr";
            string fromName = "상근";
            string to = "suhyang923@naver.com";

            string cc = "";
            string subject = "수향이 안녕?";
            string body = "코딩으로 메일보내긔!";
            bool IsHtml = false;

            MailMessage message = SetMailMessage(fromEmail, fromName, to, cc, subject, body, IsHtml);
            Email email = new Email(_host, _port, _userId, _userPassword, message);
            email.Send();

            return Json(new { RS = rs });
        }


        public MailMessage SetMailMessage(string fromEmail, string fromName, string to, string cc, string subject, string body, bool IsHtml)
        {
            MailMessage message = new MailMessage();

            MailAddress address = new MailAddress(fromEmail, fromName);

            message.From = address;
            message.To.Add(to);
            if (!string.IsNullOrEmpty(cc))
            {
                message.CC.Add(cc);
            }
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = IsHtml;

            return message;
        }
    }
}