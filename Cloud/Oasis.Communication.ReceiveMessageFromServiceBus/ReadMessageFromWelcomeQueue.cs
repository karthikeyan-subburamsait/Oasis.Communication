using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Oasis.Communication.ReceiveMessageFromServiceBus
{
    public class ReadMessageFromWelcomeQueue
    {
        [FunctionName("ReadMessageFromWelcomeQueue")]
        public void Run([ServiceBusTrigger("welcomemessage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
