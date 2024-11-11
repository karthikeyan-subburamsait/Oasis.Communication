using Azure.Identity;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text;

public class Program
{
    private const string EventHubNamespace = "Oasis-Communication-EventHub.servicebus.windows.net";
    private const string HubName = "emailcommunicationeventhub";

    public static async Task Main()
    {
        var producerClient = new EventHubProducerClient(EventHubNamespace, HubName);//, new DefaultAzureCredential());

        using var eventBatch = await producerClient.CreateBatchAsync();
        for (int i = 1; i <= 3; i++)
        {
            if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event {i}"))))
            {
                Console.WriteLine($"Event {i} is too large for the batch and cannot be sent.");
            }
        }

        try
        {
            await producerClient.SendAsync(eventBatch);
            Console.WriteLine("A batch of 3 events has been published.");
        }
        finally
        {
            await producerClient.DisposeAsync();
        }
    }
}