using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System.Text;

public class Program
{
    private const string BlobStorageConnectionString = "emailcommunicationstorag";
    private const string BlobContainerName = "emailcommunicationcontainer";
    private const string EventHubConnectionString = "Endpoint=sb://oasis-communication-eventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=1UenkaIu9+T3Z262iARkBg955bFUuxnul+AEhG3Z9/c=";
    private const string EventHubName = "emailcommunicationeventhub";

    public static async Task Main()
    {
        var storageClient = new BlobContainerClient(BlobStorageConnectionString, BlobContainerName);
        var processor = new Azure.Messaging.EventHubs.EventProcessorClient(storageClient, EventHubConsumerClient.DefaultConsumerGroupName, EventHubConnectionString, EventHubName);

        processor.ProcessEventAsync += ProcessEventHandler;
        processor.ProcessErrorAsync += ProcessErrorHandler;

        await processor.StartProcessingAsync();

        Console.WriteLine("Waiting for events...");

        await Task.Delay(TimeSpan.FromMinutes(1));
        await processor.StopProcessingAsync();
    }

    static Task ProcessEventHandler(ProcessEventArgs eventArgs)
    {
        Console.WriteLine($"Received event: {Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray())}");
        return Task.CompletedTask;
    }

    static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
    {
        Console.WriteLine($"Error on partition {eventArgs.PartitionId}: {eventArgs.Exception.Message}");
        return Task.CompletedTask;
    }
}