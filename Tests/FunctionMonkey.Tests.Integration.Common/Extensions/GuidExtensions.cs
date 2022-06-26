using Azure.Data.Tables;
using FunctionMonkey.Tests.Integration.Common.Model;
using System;
using System.Threading.Tasks;

namespace FunctionMonkey.Tests.Integration.Common.Extensions
{
    internal static class GuidExtensions
    {
        public static async Task RecordMarker(this Guid markerId)
        {
            TableClient tableClient = new TableClient(Environment.GetEnvironmentVariable("storageConnectionString"),
                Constants.Storage.Table.Markers);

            await tableClient.UpsertEntityAsync(new MarkerTableEntity
            {
                PartitionKey = markerId.ToString(),
                RowKey = string.Empty
            });
        }
    }
}
