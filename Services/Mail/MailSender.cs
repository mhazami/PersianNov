using EASendMail;
using PersianNov.DataStructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.Mail
{
    public class MailSender
    {
        public static string OrginUrl { get; set; }
        public void SendAuthorForgotPasswordEmail(Author author, string email)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();

            oMail.From = "info@persiannovel.com";

            oMail.To = email;

            oMail.Subject = "بازیابی رمز - عبور رمان فارسی";


            StringBuilder message = new StringBuilder();
            message.AppendLine("با سلام");
            message.AppendLine($"این ایمیل صرفا جهت بازیابی رمز عبور {author.FirstName} {author.LastName} ارسال شده است . در صورتی که این ایمیل برای شمما نیست لطفا آن را حذف نمایید");
            message.AppendLine($"جهت بازیابی رمز عبور بر روی این لینک کلیک نمایید {OrginUrl}/Author/ForgotReturnPage/{author.Id}");
            oMail.TextBody = message.ToString();

            SmtpServer oServer = new SmtpServer("webmail.persiannovel.com")
            {

                User = "info@persiannovel.com",
                Password = "^9m$rxK!!vU",
                Port = 587
            };
            oSmtp.SendMail(oServer, oMail);

        }
    }
}
