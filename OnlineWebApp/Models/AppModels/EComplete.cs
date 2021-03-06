using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models.AppModels
{
    public class EComplete
    {
        public void SendConfirmation(string Email)
        {
            try
            {
                var myMessage = new SendGridMessage
                {
                    From = new EmailAddress("ndumisomajola2@gmail.com", "Crooked Art Store")
                };

                myMessage.AddTo(Email);
                string subject = "Do not Reply!";
                string body = (
                    "Dear Valued Customer" /*+ Name */+ "<br/>"
                    + "<br/>"
                    + "You have successfully collected your order thank you. "
                    + "<br/>"
           
                    + "<br/>" + "We shall contact you with further details  :" +

                    "<br/>" +
                    "<br/>" +
                    "<br/>" +

                    "Sincerely Yours, " +
                    "<br/>" +
                    "Crooked Arts Management");

                myMessage.Subject = subject;
                myMessage.HtmlContent = body;

                var transportWeb = new SendGrid.SendGridClient("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");

                transportWeb.SendEmailAsync(myMessage);
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }

        }
    }
}