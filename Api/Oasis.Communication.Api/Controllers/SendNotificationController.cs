using Azure;
using Azure.Messaging.EventGrid;
using Microsoft.AspNetCore.Mvc;

namespace Oasis.Communication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendNotificationController : ControllerBase
    {
        private readonly string topicAccessKey = "8KHlBlwbCUwVsLgbADLCrDm4efIbsmc5aAZEGH2BYMU=";
        private readonly string topicEndPointUrl = "https://oasis-communication-sendnotification-topic.eastus-1.eventgrid.azure.net/api/events";

        public SendNotificationController() { }

        [Route("SendNotification")]
        [HttpPost]
        public async void SendNotification([FromBody] string notificationType)
        {
            var builder = new EventGridSasBuilder(new Uri(topicEndPointUrl), DateTimeOffset.Now.AddHours(1));
            var keyCredential = new AzureKeyCredential(topicAccessKey);
            string sasToken = builder.GenerateSas(keyCredential);
            AzureSasCredential azureSasCredential = new AzureSasCredential(sasToken);
            EventGridPublisherClient client = new EventGridPublisherClient(new Uri(topicEndPointUrl), azureSasCredential);
            await client.SendEventAsync(new EventGridEvent("Subject: Test Event Grid", "Event Type: Trigger Email or SMS", "Data Version: 1.0", new { Id = "1", Name = "Karthikeyan", NotificationType = notificationType }));
        }
    }
}
