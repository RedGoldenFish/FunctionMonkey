using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.Abstractions.Builders.Model;
using FunctionMonkey.Commanding.Cosmos.Abstractions;
using FunctionMonkey.Model;
using System;

namespace FunctionMonkey.Builders
{
    public class CosmosDbFunctionOptionBuilder<TCommandOuter> : ICosmosDbFunctionOptionBuilder<TCommandOuter>
    {
        private readonly ConnectionStringSettingNames _connectionStringSettingNames;
        private readonly ICosmosDbFunctionBuilder _underlyingBuilder;
        private readonly CosmosDbFunctionDefinition _functionDefinition;

        public CosmosDbFunctionOptionBuilder(
            ConnectionStringSettingNames connectionStringSettingNames,
            ICosmosDbFunctionBuilder underlyingBuilder,
            CosmosDbFunctionDefinition functionDefinition)
        {
            _connectionStringSettingNames = connectionStringSettingNames;
            _underlyingBuilder = underlyingBuilder;
            _functionDefinition = functionDefinition;
        }

        public ICosmosDbFunctionOptionBuilder<TCommand> ChangeFeedFunction<TCommand, TDocument>(string containerName, string databaseName,
            string leaseContainerName = "leases", string leaseDatabaseName = null,
            bool createLeaseCollectionIfNotExists = false, bool startFromBeginning = false,
            string leaseContainerPrefix = null, int? maxItemsPerInvocation = null, int? feedPollDelay = null,
            int? leaseAcquireInterval = null, int? leaseExpirationInterval = null, int? leaseRenewInterval = null,
            int? leasesContainerThroughput = null,
            bool trackRemainingWork = true,
            string remainingWorkCronExpression = "*/1 * * * * *")
        where TDocument : IDocument
        {
            return _underlyingBuilder.ChangeFeedFunction<TCommand, TDocument>(containerName, databaseName,
                leaseContainerName, leaseDatabaseName,
                createLeaseCollectionIfNotExists, startFromBeginning,
                leaseContainerPrefix, maxItemsPerInvocation, feedPollDelay,
                leaseAcquireInterval, leaseExpirationInterval, leaseRenewInterval,
                leasesContainerThroughput, trackRemainingWork, remainingWorkCronExpression);
        }

        public ICosmosDbFunctionOptionBuilder<TCommand> ChangeFeedFunction<TCommand, TCosmosDbErrorHandler, TDocument>(string containerName, string databaseName,
            string leaseContainerName = "leases", string leaseDatabaseName = null,
            bool createLeaseCollectionIfNotExists = false, bool startFromBeginning = false,
            string leaseContainerPrefix = null, int? maxItemsPerInvocation = null, int? feedPollDelay = null,
            int? leaseAcquireInterval = null, int? leaseExpirationInterval = null, int? leaseRenewInterval = null,
            int? leasesContainerThroughput = null,
            bool trackRemainingWork = true,
            string remainingWorkCronExpression = "*/1 * * * * *") where TCosmosDbErrorHandler : ICosmosDbErrorHandler<TDocument> where TDocument : IDocument
        {
            return _underlyingBuilder.ChangeFeedFunction<TCommand, TCosmosDbErrorHandler, TDocument>(containerName, databaseName,
                leaseContainerName, leaseDatabaseName,
                createLeaseCollectionIfNotExists, startFromBeginning,
                leaseContainerPrefix, maxItemsPerInvocation, feedPollDelay,
                leaseAcquireInterval, leaseExpirationInterval, leaseRenewInterval,
                leasesContainerThroughput, trackRemainingWork, remainingWorkCronExpression);
        }

        public ICosmosDbFunctionOptionBuilder<TCommandOuter> Options(Action<IFunctionOptionsBuilder> options)
        {
            FunctionOptionsBuilder builder = new FunctionOptionsBuilder(_functionDefinition);
            options(builder);
            return this;
        }

        public IOutputBindingBuilder<ICosmosDbFunctionOptionBuilder<TCommandOuter>> OutputTo =>
            new OutputBindingBuilder<ICosmosDbFunctionOptionBuilder<TCommandOuter>>(_connectionStringSettingNames, this, _functionDefinition, _pendingOutputConverterType);

        private Type _pendingOutputConverterType;
        public ICosmosDbFunctionOptionBuilder<TCommandOuter> OutputBindingConverter<TConverter>() where TConverter : IOutputBindingConverter
        {
            if (_functionDefinition.OutputBinding != null)
            {
                _functionDefinition.OutputBinding.OutputBindingConverterType = typeof(TConverter);
            }
            else
            {
                _pendingOutputConverterType = typeof(TConverter);
            }

            return this;
        }
    }
}