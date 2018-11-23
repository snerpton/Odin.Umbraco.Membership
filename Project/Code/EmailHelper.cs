using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace MicrositeAmey.Membership.Code
{
    public class EmailHelper
    {
        //private const string SendGridUsername   = "sendGridUsername";
        //private const string SendGridPassword   = "sendGridPassword";
        private const string EmailFromAddress   = "no-reply-struse@amey.email";

        public void SendResetPasswordEmail(string memberEmail, string resetGUID)
        {
            //Send a reset email to member
            // Create the email object first, then add the properties.
            var myMessage = new MailMessage(EmailFromAddress, memberEmail);

            //Subject
            myMessage.Subject = "Reset your password";

            myMessage.IsBodyHtml = true;

            //Reset link
            string baseURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, string.Empty);
            var resetURL = baseURL + Constants.UrlResetPassword + "?resetGUID=" + resetGUID;

            //HTML Message
            string body = string.Format(
                                "<h3>Reset Your Password</h3>" +
                                "<p>You have requested to reset your password<br/>" +
                                "If you have not requested to reset your password, simply ignore this email and delete it</p>" +
                                "<p><a href='{0}'>Reset your password</a></p>",
                                resetURL);

            myMessage.Body = body;

            //PlainText Message
            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // Add the alternate body to the message.

            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            myMessage.AlternateViews.Add(alternate);

            //// Create an SMTP transport for sending email.
            //var transportSMTP = new SmtpClient();

            //// Send the email.
            //transportSMTP.Send(myMessage);

            //ToDo: used this as above 2 lines caused
            //
            //'Service not available, closing transmission channel.
            //The server response was: Timeout waiting for data from client.'
            //
            using (var transportSMTP = new SmtpClient(){EnableSsl = true})
            {
                transportSMTP.Send(myMessage);
            }
        }

        public void SendVerifyEmail(string memberEmail, string verifyGUID)
        {
            //Send a reset email to member
            // Create the email object first, then add the properties.
            var myMessage = new MailMessage(EmailFromAddress, memberEmail);

            //Subject
            myMessage.Subject = "Verify Your Email";

            myMessage.IsBodyHtml = true;

            //Verify link
            string baseURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, string.Empty);
            var verifyURL = baseURL + Constants.UrlVerifyEmail + "?verifyGUID=" + verifyGUID;

            //HTML Message
            string body = string.Format(
                                "<h3>Verify Your Email</h3>" +
                                "<p>Click here to verify your email address and activate your account today</p>" +
                                "<p><a href='{0}'>Verify your email & activate your account</a></p>",
                                verifyURL);

            myMessage.Body = body;

            //PlainText Message
            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // Add the alternate body to the message.

            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            myMessage.AlternateViews.Add(alternate);

            // Create an SMTP transport for sending email.
            var transportSMTP = new SmtpClient();

            // Send the email.
            transportSMTP.Send(myMessage);
        }
    }
}