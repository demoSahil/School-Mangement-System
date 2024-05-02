using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using MimeKit;
using SMS_VO;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Services.Description;
using Message = Google.Apis.Gmail.v1.Data.Message;

namespace SMS_WEBUI.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult MailIndex()
        {
            return View();
        }

        [HttpPost]
        [ActionName("MailIndex")]
        public ActionResult MainIndex(cls_Mail_VO mail)
        {
            // Load OAuth 2.0 credentials from the JSON file
            string jsonFilePath = Server.MapPath("~/App_Data/credentials.json");
            GoogleCredential credential;

            using (var stream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(GmailService.Scope.GmailSend);
            }

            // Create Gmail service
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SMS",
            });

            try
            {
                // Create email message
                MimeMessage message = CreateMimeMessage(mail);

                // Send email
                SendMessage(service, "me", message);

                ViewBag.Success = true;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        // Helper method to create MimeMessage from MailMessage
        private MimeMessage CreateMimeMessage(cls_Mail_VO mail)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sahil", "azureblazespark@gmail.com"));
            message.To.Add(new MailboxAddress("Sahill", mail.To));
            message.Subject = mail.Subject;
            message.Body = new TextPart("html")
            {
                Text = mail.Body
            };
            return message;
        }

        // Helper method to send email using Gmail API
        private void SendMessage(GmailService service, string userId, MimeMessage message)
        {
            // Convert MimeMessage to raw format
            using (var memoryStream = new MemoryStream())
            {
                message.WriteTo(memoryStream);
                var rawMessage = Convert.ToBase64String(memoryStream.ToArray())
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Replace("=", "");

                // Create Message object
                var gmailMessage = new Message
                {
                    Raw = rawMessage
                };

                // Send email using Gmail API
                service.Users.Messages.Send(gmailMessage, userId).Execute();
            }
        }
    }
}
