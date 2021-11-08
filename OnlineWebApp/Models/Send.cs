using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineWebApp.Models
{
    public class Send
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
                    + "We would like to tell you that your package is ready for collection"
                    + "<br/>" +
                    // + "<br/>" + "Colour of the Shirts   :" + Colour_Name
                    //  + "<br/>" + "Type of the Shirts   :" + ShirtName
                    //   + "<br/>" + "Size of the Shirts   :" + size
                    //   + "<br/>" + "Number of the Shirts   :" + quantity


                    //+ "<br/>" + "We shall contact you with further details  :" +

                    "<br/>" +
                    "<br/>" +
                    "<br/>" +

                    "Sincerely Yours, " +
                    "<br/>" +
                    "Izinto Cleaning Management");

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