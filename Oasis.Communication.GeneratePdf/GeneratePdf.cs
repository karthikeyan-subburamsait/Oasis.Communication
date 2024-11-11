using Azure;
using Azure.Messaging.EventGrid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Oasis.Communication.Business;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Oasis.Communication.GeneratePdf
{
    public static class GeneratePdf
    {
        [FunctionName("GeneratePdf")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            PdfGenerationData data = JsonConvert.DeserializeObject<PdfGenerationData>(requestBody);
            name = name ?? data?.DeviceName;

            //Upload file in local to Blob storage
            bool isBlobExistsInContainer = false;
            _UploadFileToBlobStorage(data, out isBlobExistsInContainer);

            if (isBlobExistsInContainer)
                _SendNotification();

                return new OkObjectResult(name);
        }

        private static void _UploadFileToBlobStorage(PdfGenerationData pdfGenerationData, out bool isBlobExistsInContainer)
        {
            isBlobExistsInContainer = false;

            string data = string.Format("Title : {0}, Device Name : {1}, Device Value : {2}", pdfGenerationData.Title, pdfGenerationData.DeviceName, pdfGenerationData.DeviceValue);
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=oasiscommunication;AccountKey=EzyUOvrHDLUkJ6AoOfepBih38IRvfQBxjR6m6YS8AC8tRZsQrv+tpq4kYc0vlDsrsNYRqe394Ypa+AStwM73vg==;EndpointSuffix=core.windows.net");
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("oasis-communication-pdfblob");
            cloudBlobContainer.CreateIfNotExistsAsync().Wait();
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(pdfGenerationData.FileName);
            //Upload Text
            cloudBlockBlob.Properties.ContentType = "text/plain";
            cloudBlockBlob.UploadTextAsync(data).GetAwaiter();
            isBlobExistsInContainer = cloudBlockBlob.ExistsAsync().Result;
        }

        private static async void _SendNotification()
        {
            string topicAccessKey = "8KHlBlwbCUwVsLgbADLCrDm4efIbsmc5aAZEGH2BYMU=";
            string topicEndPointUrl = "https://oasis-communication-sendnotification-topic.eastus-1.eventgrid.azure.net/api/events";

            var builder = new EventGridSasBuilder(new Uri(topicEndPointUrl), DateTimeOffset.Now.AddHours(1));
            var keyCredential = new AzureKeyCredential(topicAccessKey);
            string sasToken = builder.GenerateSas(keyCredential);
            AzureSasCredential azureSasCredential = new AzureSasCredential(sasToken);
            EventGridPublisherClient client = new EventGridPublisherClient(new Uri(topicEndPointUrl), azureSasCredential);
            await client.SendEventAsync(new EventGridEvent("Subject: Test Event Grid", "Event Type: Trigger Email or SMS", "Data Version: 1.0", new { Id = "1", Name = "Karthikeyan", NotificationType = "Email" }));
        }
    }
}
