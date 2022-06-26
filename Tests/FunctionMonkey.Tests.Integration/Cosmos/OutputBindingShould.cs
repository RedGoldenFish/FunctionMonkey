using Flurl;
using Flurl.Http;
using FunctionMonkey.Tests.Integration.Common;
using FunctionMonkey.Tests.Integration.Http;
using Microsoft.Azure.Cosmos;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.Cosmos
{
    public class OutputBindingShould : AbstractHttpFunctionTest
    {
        private const string Database = "testdatabase";

        private const string Collection = "testcollection";

        [Fact]
        public async Task WriteToDatabaseWhenResponseIsSingular()
        {
            Guid markerId = Guid.NewGuid();

            var response = await Settings.Host
                .AppendPathSegment("outputBindings")
                .AppendPathSegment("toCosmos")
                .SetQueryParam("markerId", markerId)
                .GetAsync();

            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
            await WaitForMarkerInDatabase(markerId);
        }

        [Fact]
        public async Task WriteToDatabaseWhenResponseIsCollection()
        {
            Guid markerId = Guid.NewGuid();

            var response = await Settings.Host
                .AppendPathSegment("outputBindings")
                .AppendPathSegment("collectionToCosmos")
                .SetQueryParam("markerId", markerId)
                .GetAsync();

            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
            await WaitForMarkerInDatabase(markerId);
        }

        private async Task WaitForMarkerInDatabase(Guid markerId)
        {
            const int delayIncrement = 750;
            const int maximumDelay = delayIncrement * 20;

            string cosmosConnectionString = Settings.CosmosConnectionString;
            string[] cosmosConnectionStringParts = cosmosConnectionString.Split(';');
            string cosmosEndpoint = cosmosConnectionStringParts[0].Substring("AccountEndpoint=".Length);
            string cosmosAuthKey = cosmosConnectionStringParts[1].Substring("AccountKey=".Length).TrimEnd(';');
            CosmosMarker marker = null;
            using (var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosAuthKey))
            {
                int totalDelay = 0;
                do
                {
                    await Task.Delay(delayIncrement);
                    totalDelay += delayIncrement;

                    try
                    {
                        var container = cosmosClient.GetContainer(Database, Collection);

                        marker = await container.ReadItemAsync<CosmosMarker>(markerId.ToString(), new PartitionKey(markerId.ToString()));
                    }
                    catch (CosmosException ex)
                    {
                        if (ex.StatusCode != HttpStatusCode.NotFound)
                        {
                            throw;
                        }
                    }
                } while (totalDelay < maximumDelay && marker == null);
            }

            Assert.NotNull(marker);
        }
    }
}
