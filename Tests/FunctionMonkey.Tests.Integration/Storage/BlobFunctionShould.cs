using Azure.Storage.Blobs;
using FunctionMonkey.Tests.Integration.Common;
using FunctionMonkey.Tests.Integration.Storage.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.Storage
{
    public class BlobFunctionShould : AbstractStorageFunctionTest
    {
        [Fact]
        public async Task RespondToUploadedBlob()
        {
            var marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };

            BlobContainerClient container = new BlobContainerClient(Settings.StorageConnectionString, "blobcommands");

            var blob = container.GetBlobClient($"{marker.MarkerId}.json");
            await blob.UploadAsync(new BinaryData(marker));

            await marker.Assert();
        }

        [Fact]
        public async Task OutputToTableBinding()
        {
            var marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };

            BlobContainerClient container = new BlobContainerClient(Settings.StorageConnectionString, "outputbindingcontainer");

            var blob = container.GetBlobClient($"{marker.MarkerId}.json");
            await blob.UploadAsync(new BinaryData(marker));

            await marker.Assert();
        }
    }
}
