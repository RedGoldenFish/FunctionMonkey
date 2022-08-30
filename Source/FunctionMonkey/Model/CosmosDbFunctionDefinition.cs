using FunctionMonkey.Abstractions.Builders.Model;
using System;
using System.Collections.Generic;

namespace FunctionMonkey.Model
{
    public class CosmosDbFunctionDefinition : AbstractFunctionDefinition
    {
        public CosmosDbFunctionDefinition(Type commandType) : base("CosmosFn", commandType)
        {
            TrackRemainingWork = true;
        }

        public CosmosDbFunctionDefinition(Type commandType, Type resultType) : base("CosmosFn", commandType, resultType)
        {
        }

        public string DatabaseName { get; set; }

        public string ContainerName { get; set; }

        public string ConnectionStringName { get; set; }

        public Type DocumentType { get; set; }

        public string DocumentTypeName { get; set; }

        public IReadOnlyCollection<CosmosDbCommandProperty> CommandProperties { get; set; }

        public bool IsDocumentCommand { get; set; }

        public bool IsDocumentBatchCommand { get; set; }

        public string LeaseContainerName { get; set; }

        public string LeaseDatabaseName { get; set; }

        public bool CreateLeaseCollectionIfNotExists { get; set; }

        public bool StartFromBeginning { get; set; }

        public string LeaseConnectionStringName { get; set; }

        public string LeaseContainerPrefix { get; set; }

        public bool TrackRemainingWork { get; set; }

        public int? MaxItemsPerInvocation { get; set; }

        public int? FeedPollDelay { get; set; }
        public int? LeaseAcquireInterval { get; set; }
        public int? LeaseExpirationInterval { get; set; }
        public int? LeaseRenewInterval { get; set; }
        public int? LeasesContainerThroughput { get; set; }
        public string ErrorHandlerTypeName { get; set; }
        public Type ErrorHandlerType { get; set; }
        public string RemainingWorkCronExpression { get; set; }
    }
}
