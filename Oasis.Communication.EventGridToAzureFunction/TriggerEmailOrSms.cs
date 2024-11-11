using Azure;
using Azure.Communication.Email;
using Azure.Communication.Sms;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace Oasis.Communication.SendNotification
{
    public static class SendNotification
    {
        [FunctionName("SendNotification")]
        public static void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            try
            {
                log.LogInformation(eventGridEvent.Data.ToString());

                if (eventGridEvent != null && eventGridEvent.Data != null)
                {
                    var notification = JsonConvert.DeserializeObject<Notification>(eventGridEvent.Data.ToString());
                    var communicationServiceUrl = "endpoint=https://oasis-communication-sendemail.unitedstates.communication.azure.com/;accesskey=1nEfNHxosn7koAyRy2AOVsDNBb2hlTA0S1a3HMf8oSpK1afKQJ2nJQQJ99AHACULyCpm3RQcAAAAAZCSk8mm";

                    if (notification.NotificationType == "Email")
                    {
                        _SendEmail(communicationServiceUrl);
                    }
                    else
                    {
                        _SendSMS(communicationServiceUrl);
                    }
                    log.LogInformation("Successfully trigerred " + notification.NotificationType);
                }
            }
            catch (Exception ex)
            {
                log.LogError(string.Format("{0} Stack Trace : {1} Inner Exception : {2} Message : {3}", "Issue in trigerring Email/SMS : ", ex.StackTrace, ex.InnerException, ex.Message));
            }
        }

        private static void _SendEmail(string communicationServiceConnectionString)
        {
            var emailClient = new EmailClient(communicationServiceConnectionString);
            EmailSendOperation emailSendOperation = emailClient.Send(
                WaitUntil.Completed,
                senderAddress: "DoNotReply@908521ae-0edc-4ff6-8184-11322f379aa2.azurecomm.net",
                recipientAddress: "karthikeyan.ece18@gmail.com",
                subject: "Test Email for validating communication service",
                htmlContent: "<html><h1>Hello world via email. Current date and time is " + DateTime.Now.ToString() + "</h1l></html>",
                plainTextContent: "Hello world via email.");
        }

        private static void _SendSMS(string communicationServiceConnectionString)
        {
            var smsClient = new SmsClient(communicationServiceConnectionString);
            smsClient.SendAsync("9566103847", "9566103847", "Sample text sms");
        }
    }
}
