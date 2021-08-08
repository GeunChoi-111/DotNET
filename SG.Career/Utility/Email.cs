using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SG.Career.Utility
{
    interface IEmail
    {
        void Send();
    }

    public class Email
    {
        public string _host;
        public int _port;
        public string _userId;
        public string _userPassword;
        public MailMessage _mailMassage;

        public Email() 
        {
            this._host = ConfigurationManager.AppSettings["SmtpHost"];
            this._port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            this._userId = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpUserId"]) ? "" : ConfigurationManager.AppSettings["SmtpUserId"];
            this._userPassword = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpUserPassword"]) ? "" : ConfigurationManager.AppSettings["SmtpUserPassword"];
        }


        public Email(string _host, int _port, string _userId, string _userPassword, MailMessage _mailMassage)
        {
            this._host = _host;
            this._port = _port;
            this._userId = _userId;
            this._userPassword = _userPassword;
            this._mailMassage = _mailMassage;
        }

        public void Send()
        {
            try
            {
                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(_host, _port))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(_userId, _userPassword);
                    smtp.Send(_mailMassage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex.InnerException);
            }
            finally
            {
                _mailMassage.Dispose();
            }
        }
    }
}
