using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace Oasis.Communication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureServiceBusController : ControllerBase
    {
        private readonly ILogger<AzureServiceBusController> _logger;
        //private IQueueClient _queueClient;
        private string messageFromAzureServiceBusQueue;

        public AzureServiceBusController(ILogger<AzureServiceBusController> logger)//, IQueueClient queueClient)
        {
            _logger = logger;
            //this._queueClient = queueClient;
        }

        [HttpPost(Name = "PostMessageToAzureServiceBusQueue")]
        public void PostMessageToAzureServiceBusQueue()
        {
            const string serviceBusConnectionString = "Endpoint=sb://oasis-communication.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LPLnFNdAMsPjtRqda8uI5wHCij47eS3O6+ASbEnnkeM=";
            const string queueName = "welcomemessage";
            var _queueClient = new QueueClient(serviceBusConnectionString, queueName);
            var message = new Message(Encoding.UTF8.GetBytes("Hello, Karthik!"));
            _queueClient.SendAsync(message).GetAwaiter().GetResult();
            Console.WriteLine("Message sent to the queue successfully.");
            _queueClient.CloseAsync();
        }

        [HttpGet(Name = "GetMessageFromAzureServiceBusQueue")]
        public void GetMessageFromAzureServiceBusQueue()
        {
            const string serviceBusConnectionString = "Endpoint=sb://oasis-communication.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LPLnFNdAMsPjtRqda8uI5wHCij47eS3O6+ASbEnnkeM=";
            const string queueName = "welcomemessage";
            var _queueClient = new QueueClient(serviceBusConnectionString, queueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _queueClient.RegisterMessageHandler((Message message, CancellationToken cancellationToken) => (
                Task.FromResult(Encoding.UTF8.GetString(message.Body))
            ), messageHandlerOptions);
        }

        //private Task ProcessMessagesAsync(Message message, CancellationToken cancellationToken)
        //{
        //    Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
        //    this.messageFromAzureServiceBusQueue = Encoding.UTF8.GetString(message.Body);
        //    return Task.FromResult(this.messageFromAzureServiceBusQueue);
        //}

        //private Task GetMessageFromAzureServiceBusQueueBody(Message message, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(Encoding.UTF8.GetString(message.Body));
        //}

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
    }
}
