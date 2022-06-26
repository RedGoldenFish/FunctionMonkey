using Azure.Messaging.ServiceBus;
using Azure.Storage.Queues;
using FunctionMonkey.Tests.Integration.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.ServiceBus
{
    public class QueueFunctionShould : AbstractIntegrationTest
    {
        [Fact]
        public async Task RespondToEnqueuedItem()
        {
            MarkerMessage marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };

            var queueClient = new QueueClient(Settings.ServiceBusConnectionString, "testqueue");
            await queueClient.SendMessageAsync(new BinaryData(marker));

            await marker.Assert();
        }

        [Fact]
        public async Task RespondToEnqueuedItemWithSessionId()
        {
            MarkerMessage marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };

            var serviceBusMessage = new ServiceBusMessage(new BinaryData(marker))
            {
                SessionId = Guid.NewGuid().ToString()
            };

            var client = new ServiceBusClient(Settings.ServiceBusConnectionString);

            var queueClient = client.CreateSender("sessionidtestqueue");
            await queueClient.SendMessageAsync(serviceBusMessage);

            await marker.Assert();
        }

        [Fact]
        public async Task OutputToTableBinding()
        {
            MarkerMessage marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };
            var serviceBusMessage = new ServiceBusMessage(new BinaryData(marker));

            var client = new ServiceBusClient(Settings.ServiceBusConnectionString);

            var queueClient = client.CreateSender("tableoutput");
            await queueClient.SendMessageAsync(serviceBusMessage);

            await marker.Assert();
        }
    }
}
