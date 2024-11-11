using Azure;
using Azure.Messaging.EventGrid;
using Microsoft.AspNetCore.Mvc;

namespace Oasis.Communication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureEventGridController : ControllerBase
    {
        private readonly string topicAccessKey = "zKXxGomBD1UuGe0kw2RRVvBKFjoJnSWugAZEGMLP8qk=";
        private readonly string topicEndPointUrl = "https://oasis-communication-triggeremailorsms-topic.southindia-1.eventgrid.azure.net/api/events";

        public AzureEventGridController() { }

        [Route("PostEventToAzureEventGrid")]
        [HttpPost]
        public async void PostEventToAzureEventGrid([FromBody] string notificationType)
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
