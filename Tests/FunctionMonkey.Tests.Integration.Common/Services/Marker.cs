using Azure.Data.Tables;
using FunctionMonkey.Tests.Integration.Common.Model;
using System;
using System.Threading.Tasks;

namespace FunctionMonkey.Tests.Integration.Common.Services
{
    public class Marker : IMarker
    {
        public async Task RecordMarker(Guid markerId)
        {
            TableClient table = new TableClient(Environment.GetEnvironmentVariable("storageConnectionString"), Constants.Storage.Table.Markers);

            await table.UpsertEntityAsync(new MarkerTableEntity
            {
                PartitionKey = markerId.ToString(),
                RowKey = string.Empty
            });
        }
    }
}