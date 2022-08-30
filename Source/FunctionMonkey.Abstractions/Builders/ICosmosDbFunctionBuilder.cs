using FunctionMonkey.Commanding.Cosmos.Abstractions;

namespace FunctionMonkey.Abstractions.Builders
{
    public interface ICosmosDbFunctionBuilder
    {
        ///  <summary>
        ///  Associate a function with the specified collection and database
        ///  </summary>
        ///  <typeparam name="TCommand"></typeparam>
        ///  <typeparam name="TDocument">The type of document exchanged</typeparam>
        ///  <param name="containerName">The Cosmos collection name</param>
        ///  <param name="databaseName">The Cosmos database name</param>
        ///  <param name="leaseContainerName">The cosmos collection to use for leases - defaults to leases</param>
        ///  <param name="leaseDatabaseName">The cosmos database to use for leases - defaults to the database name</param>
        ///  <param name="createLeaseCollectionIfNotExists">Creates the lease collection if it doesn't exist, defaults to false</param>
        ///  <param name="startFromBeginning">Start from the beginning of the change feed, defaults to false</param>
        /// <param name="leaseContainerPrefix">When set, it adds a prefix to the leases created in the Lease collection for this Function</param>
        /// <param name="maxItemsPerInvocation">When set, it customizes the maximum amount of items received per Function call.</param>
        /// <param name="feedPollDelay">When set, it defines, in milliseconds, the delay in between polling a partition for new changes on the feed, after all current changes are drained. Default is 5000 (5 seconds).</param>
        /// <param name="leaseAcquireInterval">When set, it defines, in milliseconds, the interval to kick off a task to compute if partitions are distributed evenly among known host instances. Default is 13000 (13 seconds).</param>
        /// <param name="leaseExpirationInterval">When set, it defines, in milliseconds, the interval for which the lease is taken on a lease representing a partition. If the lease is not renewed within this interval, it will cause it to expire and ownership of the partition will move to another instance. Default is 60000 (60 seconds)</param>
        /// <param name="leaseRenewInterval">When set, it defines, in milliseconds, the renew interval for all leases for partitions currently held by an instance. Default is 17000 (17 seconds).</param>
        /// <param name="leasesContainerThroughput">Defines the amount of Request Units to assign when the leases collection is created. This setting is only used When createLeaseCollectionIfNotExists is set to true. This parameter is automatically set when the binding is created using the portal.</param>
        /// <param name="trackRemainingWork">If true (default value) this will create a timer function that will output a remaining work estimate to the log - the metric name will be of the form {functionName}RemainingWork </param>
        /// <param name="remainingWorkCronExpression">The frequency that the monitor timer runs - defaults to once per every 5 seconds</param>
        /// <returns></returns>
        ICosmosDbFunctionOptionBuilder<TCommand> ChangeFeedFunction<TCommand, TDocument>(string containerName,
            string databaseName,
            string leaseContainerName = "leases",
            string leaseDatabaseName = null,
            bool createLeaseCollectionIfNotExists = false,
            bool startFromBeginning = false,
            string leaseContainerPrefix = null,
            int? maxItemsPerInvocation = null,
            int? feedPollDelay = null,
            int? leaseAcquireInterval = null,
            int? leaseExpirationInterval = null,
            int? leaseRenewInterval = null,
            int? leasesContainerThroughput = null,
            bool trackRemainingWork = false,
            string remainingWorkCronExpression = "*/5 * * * * *"
        )
            where TDocument : IDocument;

        ///  <summary>
        ///  Associate a function with the specified collection and database
        ///  </summary>
        ///  <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TCosmosDbErrorHandler">A type that provides error handling for document processing</typeparam>
        ///  <typeparam name="TDocument">The type of document managed</typeparam>
        ///  <param name="containerName">The Cosmos collection name</param>
        ///  <param name="databaseName">The Cosmos database name</param>
        ///  <param name="leaseContainerName">The cosmos collection to use for leases - defaults to leases</param>
        ///  <param name="leaseDatabaseName">The cosmos database to use for leases - defaults to the database name</param>
        ///  <param name="createLeaseCollectionIfNotExists">Creates the lease collection if it doesn't exist, defaults to false</param>
        ///  <param name="startFromBeginning">Start from the beginning of the change feed, defaults to false</param>
        /// <param name="leaseContainerPrefix">When set, it adds a prefix to the leases created in the Lease collection for this Function</param>
        /// <param name="maxItemsPerInvocation">When set, it customizes the maximum amount of items received per Function call.</param>
        /// <param name="feedPollDelay">When set, it defines, in milliseconds, the delay in between polling a partition for new changes on the feed, after all current changes are drained. Default is 5000 (5 seconds).</param>
        /// <param name="leaseAcquireInterval">When set, it defines, in milliseconds, the interval to kick off a task to compute if partitions are distributed evenly among known host instances. Default is 13000 (13 seconds).</param>
        /// <param name="leaseExpirationInterval">When set, it defines, in milliseconds, the interval for which the lease is taken on a lease representing a partition. If the lease is not renewed within this interval, it will cause it to expire and ownership of the partition will move to another instance. Default is 60000 (60 seconds)</param>
        /// <param name="leaseRenewInterval">When set, it defines, in milliseconds, the renew interval for all leases for partitions currently held by an instance. Default is 17000 (17 seconds).</param>
        /// <param name="leasesContainerThroughput">Defines the amount of Request Units to assign when the leases collection is created. This setting is only used When createLeaseCollectionIfNotExists is set to true. This parameter is automatically set when the binding is created using the portal.</param>
        /// <param name="trackRemainingWork">If true (default value) this will create a timer function that will output a remaining work estimate to the log - the metric name will be of the form {functionName}RemainingWork </param>
        /// <param name="remainingWorkCronExpression">The frequency that the monitor timer runs - defaults to once per every 5 seconds</param>
        /// <returns></returns>
        ICosmosDbFunctionOptionBuilder<TCommand> ChangeFeedFunction<TCommand, TCosmosDbErrorHandler, TDocument>(string containerName,
            string databaseName,
            string leaseContainerName = "leases",
            string leaseDatabaseName = null,
            bool createLeaseCollectionIfNotExists = false,
            bool startFromBeginning = false,
            string leaseContainerPrefix = null,
            int? maxItemsPerInvocation = null,
            int? feedPollDelay = null,
            int? leaseAcquireInterval = null,
            int? leaseExpirationInterval = null,
            int? leaseRenewInterval = null,
            int? leasesContainerThroughput = null,
            bool trackRemainingWork = false,
            string remainingWorkCronExpression = "*/5 * * * * *"
        )
            where TCosmosDbErrorHandler : ICosmosDbErrorHandler<TDocument>
                where TDocument : IDocument;
    }
}
