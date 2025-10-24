using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Twilio.Rest.Api.V2010.Account;

namespace CarRentalSystem.PL.Services
{
    public interface ISMSTwilio
    {
        MessageResource SendSMS(string toPhoneNumber, string message);
    }
}
