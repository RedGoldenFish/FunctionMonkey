using Azure.Storage.Queues;
using FunctionMonkey.Tests.Integration.Common;
using FunctionMonkey.Tests.Integration.Storage.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.Storage
{
    public class QueueFunctionShould : AbstractStorageFunctionTest
    {
        [Fact]
        public async Task RespondsToEnqueuedItem()
        {
            var marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };

            QueueClient queue = new QueueClient(Settings.StorageConnectionString, "testqueue");
            await queue.SendMessageAsync(new BinaryData(marker));

            await marker.Assert();
        }
    }
}
