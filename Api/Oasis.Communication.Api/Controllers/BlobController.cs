using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Oasis.Communication.Business;
using System;
using System.Text;

namespace Oasis.Communication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly string blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=oasiscommunication;AccountKey=EzyUOvrHDLUkJ6AoOfepBih38IRvfQBxjR6m6YS8AC8tRZsQrv+tpq4kYc0vlDsrsNYRqe394Ypa+AStwM73vg==;EndpointSuffix=core.windows.net";
        private readonly string blobContainerName = "oasis-communication-pdfblob";

        public BlobController() { }

        [HttpPost]
        [Route("UploadFileToBlob/{fileName}")]
        public async void UploadFileToBlob(string fileName)
        {
            string data = string.Format("Title : {0}, Device Name : {1}, Device Value : {2}", "Order Detail", "Iphone 13 midnight blue 128GB", 55000.00m);
            fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobConnectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainerName);
            cloudBlobContainer.CreateIfNotExistsAsync().Wait();
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);

            //Upload Text
            cloudBlockBlob.Properties.ContentType = "text/plain";
            await cloudBlockBlob.UploadTextAsync(data);
        }

        [HttpGet]
        [Route("DownloadFileFromBlob/{fileName}")]
        public async void DownloadFileFromBlob(string fileName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobConnectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainerName);
            CloudBlob cloudBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            Stream blobStream = cloudBlob.OpenReadAsync().Result;
            File(blobStream, contentType: "application/octet-stream", fileName);
        }

        [HttpDelete]
        [Route("DeleteFileFromBlob/{fileName}")]
        public async void DeleteFileFromBlob(string fileName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobConnectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainerName);
            CloudBlob cloudBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            if (cloudBlob != null)
            {
                await cloudBlob.DeleteAsync();
            }
        }
    }
}
