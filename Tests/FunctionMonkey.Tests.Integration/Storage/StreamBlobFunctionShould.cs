using Azure.Storage.Blobs;
using FunctionMonkey.Tests.Integration.Common;
using FunctionMonkey.Tests.Integration.Storage.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.Storage
{
    public class StreamBlobFunctionShould : AbstractStorageFunctionTest
    {
        [Fact]
        public async Task RespondToUploadedBlob()
        {
            var marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };

            BlobContainerClient container = new BlobContainerClient(Settings.StorageConnectionString, "streamblobcommands");

            var blobClient = container.GetBlobClient($"{marker.MarkerId}.json");
            await blobClient.UploadAsync(new BinaryData(marker));

            await marker.Assert();
        }
    }
}
