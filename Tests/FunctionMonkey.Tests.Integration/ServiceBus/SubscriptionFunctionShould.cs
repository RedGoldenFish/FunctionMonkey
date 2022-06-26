using Azure.Messaging.ServiceBus;
using FunctionMonkey.Tests.Integration.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.ServiceBus
{
    public class SubscriptionFunctionShould : AbstractIntegrationTest
    {
        [Fact]
        public async Task RespondToEnqueuedItem()
        {
            MarkerMessage marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };

            var client = new ServiceBusClient(Settings.ServiceBusConnectionString);

            var topicClient = client.CreateSender("testtopic");
            await topicClient.SendMessageAsync(new ServiceBusMessage(new BinaryData(marker)));

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

            var topicClient = client.CreateSender("sessionidtesttopic");
            await topicClient.SendMessageAsync(serviceBusMessage);

            await marker.Assert();
        }
    }
}
