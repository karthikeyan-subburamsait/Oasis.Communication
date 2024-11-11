using Azure.Communication.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Oasis.Communication.Email
{
    public static class SendEmail
    {
        [FunctionName("SendEmail")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Sending email azure function triggered.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var communicationServiceUrl = "endpoint=https://oasis-communication-email.india.communication.azure.com/;accesskey=AMIcjHnxIj21xqZdy84C5UUdyMJOazVXKAdCn83Yl0nj02xJF3fhJQQJ99AGACULyCpm3RQcAAAAAZCSAHuD";
            var emailClient = new EmailClient(communicationServiceUrl);

            EmailSendOperation emailSendOperation = emailClient.SendAsync(
                Azure.WaitUntil.Completed, "karthikeyan.subburamsait@likewize.com", "karthikeyan.subburamsait@likewize.com", "Welcome Email", "<html><h1>Hi User, Welcome Back</h1></html>").Result;

            return new OkObjectResult(new { Result = "Success" });
        }
    }
}
