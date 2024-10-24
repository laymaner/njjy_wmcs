using log4net;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Wish.Model.System;

namespace Wish.ViewModel.Common
{
    internal class EmailHelper
    {
        private static ILog logger = LogManager.GetLogger(typeof(EmailHelper));
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="content">邮件内容</param>
        /// <param name="to">收件人</param>
        public static void SendEmail(string subject, string content, string password, List<SysEmail> toList)
        {
            try
            {
                string emailFromAddress = "wms@tztek.com";
                //string emailFromPassword = "Wms@Tztek23";
                string emailFromPassword = password;
                string emailHostIP = "smtp.qiye.163.com";
                string emailHostPort = "994";
                MimeMessage mes = new MimeMessage();

                //发件人
                mes.From.Add(new MailboxAddress("WMS系统", emailFromAddress));
                //收件人
                foreach (var sysAlertEmail in toList)
                {
                    mes.To.Add(new MailboxAddress(sysAlertEmail.userName, sysAlertEmail.email));
                }

                //标题
                mes.Subject = subject;
                TextPart text = new TextPart(TextFormat.Html)
                {
                    //邮件内容
                    Text = content
                };
                Multipart multipart = new Multipart("mixed");
                multipart.Add(text);
                mes.Body = multipart;
                using (SmtpClient client = new SmtpClient())
                {
                    //邮件服务器
                    client.Connect(emailHostIP, Convert.ToInt32(emailHostPort), true);
                    //帐号、密码
                    client.Authenticate(emailFromAddress, emailFromPassword);
                    client.Send(mes);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                logger.Warn($"邮件发送,失败：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
