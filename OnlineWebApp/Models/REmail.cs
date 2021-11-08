using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models
{
    public class REmail
    {
        public void SendConfirmation(string email)
        {
            try
            {
                var myMessage = new SendGridMessage
                {
                    From = new EmailAddress("ndumisomajola2@gmail.com", "Crooked Art Store")
                };

                myMessage.AddTo(email);
                string subject = "Do not Reply!";
                string body = (
                    "Dear Valued Customer" /*+ Name */+ "<br/>"
                    + "<br/>"
                    + "We have recieved your query to return an item back. "
                    + "<br/>"
                     + "<br/>" + "Please kindly return your item to the pick up point for inspection first then we will contact you in 3 to 5 days to tell you whether does your item qualififes to be returned or not."



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