using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.Abstractions.Builders.Model;
using FunctionMonkey.Abstractions.Extensions;
using FunctionMonkey.Commanding.Cosmos.Abstractions;
using FunctionMonkey.Model;
using System.Collections.Generic;

namespace FunctionMonkey.Builders
{
    internal class CosmosDbFunctionBuilder : ICosmosDbFunctionBuilder
    {
        private readonly ConnectionStringSettingNames _connectionStringSettingNames;
        private readonly string _connectionStringName;
        private readonly string _leaseConnectionStringName;
        private readonly List<AbstractFunctionDefinition> _functionDefinitions;

        public CosmosDbFunctionBuilder(
            ConnectionStringSettingNames connectionStringSettingNames,
            string connectionStringName,
            string leaseConnectionName,
            List<AbstractFunctionDefinition> functionDefinitions)
        {
            _connectionStringSettingNames = connectionStringSettingNames;
            _connectionStringName = connectionStringName;
            _functionDefinitions = functionDefinitions;
            _leaseConnectionStringName = leaseConnectionName;
        }

        public ICosmosDbFunctionOptionBuilder<TCommand> ChangeFeedFunction<TCommand, TDocument>(
            string containerName,
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
        where TDocument : IDocument
        {
            CosmosDbFunctionDefinition definition = new CosmosDbFunctionDefinition(typeof(TCommand))
            {
                ConnectionStringName = _connectionStringName,
                ContainerName = containerName,
                DatabaseName = databaseName,
                DocumentType = typeof(TDocument),
                DocumentTypeName = typeof(TDocument).EvaluateType(),
                LeaseConnectionStringName = _leaseConnectionStringName,
                LeaseContainerName = leaseContainerName,
                LeaseDatabaseName = leaseDatabaseName ?? databaseName,
                CreateLeaseCollectionIfNotExists = createLeaseCollectionIfNotExists,
                StartFromBeginning = startFromBeginning,
                LeaseContainerPrefix = leaseContainerPrefix,
                MaxItemsPerInvocation = maxItemsPerInvocation,
                FeedPollDelay = feedPollDelay,
                LeaseAcquireInterval = leaseAcquireInterval,
                LeaseExpirationInterval = leaseExpirationInterval,
                LeaseRenewInterval = leaseRenewInterval,
                LeasesContainerThroughput = leasesContainerThroughput,
                TrackRemainingWork = trackRemainingWork,
                RemainingWorkCronExpression = remainingWorkCronExpression
            };
            _functionDefinitions.Add(definition);
            return new CosmosDbFunctionOptionBuilder<TCommand>(_connectionStringSettingNames, this, definition);
        }

        public ICosmosDbFunctionOptionBuilder<TCommand> ChangeFeedFunction<TCommand, TCosmosDbErrorHandler, TDocument>(
            string containerName,
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
            ) where TCosmosDbErrorHandler : ICosmosDbErrorHandler<TDocument>
        where TDocument : IDocument
        {
            CosmosDbFunctionDefinition definition = new CosmosDbFunctionDefinition(typeof(TCommand))
            {
                ConnectionStringName = _connectionStringName,
                ContainerName = containerName,
                DatabaseName = databaseName,
                DocumentType = typeof(TDocument),
                DocumentTypeName = typeof(TDocument).EvaluateType(),
                LeaseConnectionStringName = _leaseConnectionStringName,
                LeaseContainerName = leaseContainerName,
                LeaseDatabaseName = leaseDatabaseName ?? databaseName,
                CreateLeaseCollectionIfNotExists = createLeaseCollectionIfNotExists,
                StartFromBeginning = startFromBeginning,
                LeaseContainerPrefix = leaseContainerPrefix,
                MaxItemsPerInvocation = maxItemsPerInvocation,
                FeedPollDelay = feedPollDelay,
                LeaseAcquireInterval = leaseAcquireInterval,
                LeaseExpirationInterval = leaseExpirationInterval,
                LeaseRenewInterval = leaseRenewInterval,
                LeasesContainerThroughput = leasesContainerThroughput,
                ErrorHandlerType = typeof(TCosmosDbErrorHandler),
                ErrorHandlerTypeName = typeof(TCosmosDbErrorHandler).EvaluateType(),
                TrackRemainingWork = trackRemainingWork,
                RemainingWorkCronExpression = remainingWorkCronExpression
            };
            _functionDefinitions.Add(definition);
            return new CosmosDbFunctionOptionBuilder<TCommand>(_connectionStringSettingNames, this, definition);
        }
    }
}
