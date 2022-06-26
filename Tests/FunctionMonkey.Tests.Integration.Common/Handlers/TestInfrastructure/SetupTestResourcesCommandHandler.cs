using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Tests.Integration.Common.Commands.TestInfrastructure;
using System;
using System.Threading.Tasks;

namespace FunctionMonkey.Tests.Integration.Common.Handlers.TestInfrastructure
{
    internal class SetupTestResourcesCommandHandler : ICommandHandler<SetupTestResourcesCommand>
    {
        public async Task ExecuteAsync(SetupTestResourcesCommand command)
        {
            // Storage
            TableClient markerTable = new TableClient(Environment.GetEnvironmentVariable("storageConnectionString"), Constants.Storage.Table.Markers);
            await markerTable.CreateIfNotExistsAsync();


            QueueClient testQueue = new QueueClient(Environment.GetEnvironmentVariable("storageConnectionString"), Constants.Storage.Queue.TestQueue);
            await testQueue.CreateIfNotExistsAsync();
            QueueClient markerQueue = new QueueClient(Environment.GetEnvironmentVariable("storageConnectionString"), Constants.Storage.Queue.MarkerQueue);
            await markerQueue.CreateIfNotExistsAsync();

            BlobContainerClient blobCommandsContainer = new BlobContainerClient(Environment.GetEnvironmentVariable("storageConnectionString"), Constants.Storage.Blob.BlobCommandContainer);
            await blobCommandsContainer.CreateIfNotExistsAsync();
            BlobContainerClient streamBlobCommandsContainer = new BlobContainerClient(Environment.GetEnvironmentVariable("storageConnectionString"), Constants.Storage.Blob.StreamBlobCommandContainer);
            await streamBlobCommandsContainer.CreateIfNotExistsAsync();
            BlobContainerClient outputBlobContainer = new BlobContainerClient(Environment.GetEnvironmentVariable("storageConnectionString"), Constants.Storage.Blob.OutputBlobContainer);
            await outputBlobContainer.CreateIfNotExistsAsync();
            BlobContainerClient outputBindingContainer =
                new BlobContainerClient(Environment.GetEnvironmentVariable("storageConnectionString"), Constants.Storage.Blob.BlobOutputCommandContainer);
            await outputBindingContainer.CreateIfNotExistsAsync();

            // Cosmos and Service Bus
            // Created through provisioning
        }
    }
}
