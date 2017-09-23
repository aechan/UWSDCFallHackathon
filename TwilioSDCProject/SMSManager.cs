using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Nancy.Extensions;
using System.Collections.Specialized;
using Nancy.Helpers;

namespace TwilioSDCProject
{
    public class SMSManager : Nancy.NancyModule
    {
        
        const string accountSid = "AC1265576a3cee850ecfea3fb83b8ec134";
        
        const string authToken = "614194502bc49a7ade804b2f39f0da45";

        public SMSManager()
        {
            TwilioClient.Init(accountSid, authToken);
            Post["/smslistener"] = x => { RecieveSMS(this.Request); return null; };

        }

        public static void RecieveSMS(Nancy.Request req)
        {
            NameValueCollection qscoll = HttpUtility.ParseQueryString(req.Body.AsString());
            string From = qscoll["From"] ?? null;
            string Body = qscoll["Body"] ?? null;
            string Sid = qscoll["AccountSid"] ?? null;
            if(Sid != "AC1265576a3cee850ecfea3fb83b8ec134") { return; }

            Console.WriteLine("From: " + From + " Body: " + Body);
            if(Body == null) { return;  }
            if(From == null) { return;  }


            // format
            // find: 640 elm drive == find request
            // submit: 640 elm drive : hello! == submit message at address
            string[] parsed = Body.Split(':');

            if (parsed[0].Contains("find"))
            {
                
                var l = GeocodingManager.GetMessagesFromAddress(parsed[1]);
                foreach(Message m in l)
                {
                    SendSMS("Message: " + m.Body + " | Left at: "+m.Date.ToShortDateString(),
                        From);
                }

            }
            else if (parsed[0].Contains("submit"))
            {
                string address = parsed[2];
                Message m = new Message(parsed[1]);
                GeocodingManager.AddMessageAtAddress(address, m);
            }
        }

        static void SendSMS(string message, string target)
        {
            var m = MessageResource.Create(
                to: new PhoneNumber(target),
                from: new PhoneNumber("+1 424-377-7497 "),
                body: message);
        }
    }
}
