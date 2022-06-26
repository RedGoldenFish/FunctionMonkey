using Azure;
using Azure.Data.Tables;
using System;
using System.Threading.Tasks;

namespace FunctionMonkey.Tests.Integration.Common.Model
{
    public class MarkerTableEntity : ITableEntity
    {
        public static Task<MarkerTableEntity> Success(Guid markerId)
        {
            return Task.FromResult(new MarkerTableEntity
            {
                PartitionKey = markerId.ToString(),
                RowKey = string.Empty
            });
        }

        /// <inheritdoc />
        public string PartitionKey { get; set; }

        /// <inheritdoc />
        public string RowKey { get; set; }

        /// <inheritdoc />
        public DateTimeOffset? Timestamp { get; set; }

        /// <inheritdoc />
        public ETag ETag { get; set; }
    }
}
