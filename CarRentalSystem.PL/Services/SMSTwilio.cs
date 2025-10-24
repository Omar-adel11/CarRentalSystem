using System.Net;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CarRentalSystem.PL.Services
{
    public class SMSTwilio : ISMSTwilio
    {
        private readonly IConfiguration _config;

        public SMSTwilio(IConfiguration config)
        {
            _config = config;
        }
        public MessageResource SendSMS(string toPhoneNumber, string message)
        {
            TwilioClient.Init(_config["Twilio:AccountSID"], _config["Twilio:AuthToken"]);
            var fromPhoneNumber = _config["Twilio:FromPhoneNumber"];
            var msg = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(toPhoneNumber)
            );
            // You may want to return true/false based on msg.Status or similar
            return msg;
        }
    }
}
