using FunctionMonkey.Tests.Integration.Common;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.Cosmos
{
    public class ChangeFeedFunctionShould : AbstractIntegrationTest
    {
        [Fact]
        public async Task RespondToNewItem()
        {
            string cosmosConnectionString = Settings.CosmosConnectionString;
            string[] cosmosConnectionStringParts = cosmosConnectionString.Split(';');
            string cosmosEndpoint = cosmosConnectionStringParts[0].Substring("AccountEndpoint=".Length);
            string cosmosAuthKey = cosmosConnectionStringParts[1].Substring("AccountKey=".Length).TrimEnd(';');
            using (var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosAuthKey))
            {
                var container = cosmosClient.GetContainer("testdatabase", "testcollection");

                MarkerMessage marker = new MarkerMessage
                {
                    MarkerId = Guid.NewGuid()
                };
                await container.CreateItemAsync(marker);

                await marker.Assert();
            }
        }

        [Fact]
        public async Task OutputToTableBinding()
        {
            string cosmosConnectionString = Settings.CosmosConnectionString;
            string[] cosmosConnectionStringParts = cosmosConnectionString.Split(';');
            string cosmosEndpoint = cosmosConnectionStringParts[0].Substring("AccountEndpoint=".Length);
            string cosmosAuthKey = cosmosConnectionStringParts[1].Substring("AccountKey=".Length).TrimEnd(';');
            using (var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosAuthKey))
            {
                var container = cosmosClient.GetContainer("testdatabase", "outputtablecollection");

                MarkerMessage marker = new MarkerMessage
                {
                    MarkerId = Guid.NewGuid()
                };
                await container.CreateItemAsync(marker);

                await marker.Assert();
            }
        }
    }
}
