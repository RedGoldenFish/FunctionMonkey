
using Azure;
using Azure.Data.Tables;
using System;

internal class MarkerTableEntity : ITableEntity
{
    public int? Value { get; set; }

    /// <inheritdoc />
    public string PartitionKey { get; set; }

    /// <inheritdoc />
    public string RowKey { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? Timestamp { get; set; }

    /// <inheritdoc />
    public ETag ETag { get; set; }
}