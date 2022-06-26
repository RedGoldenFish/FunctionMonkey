using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using FunctionMonkey.Tests.Integration.Common;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.EventHub
{
    public class EventHubFunctionShould : AbstractIntegrationTest
    {
        [Fact]
        public async Task RespondToEnqueuedItem()
        {
            MarkerMessage marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };
            string json = JsonConvert.SerializeObject(marker);

            EventHubProducerClient client =
                new EventHubProducerClient(Settings.EventHubConnectionString, "maintesthub");

            await client.SendAsync(new[] { new EventData(Encoding.UTF8.GetBytes(json)) });

            await marker.Assert();
        }
    }
}