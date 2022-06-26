using Azure.Storage.Queues.Models;
using FunctionMonkey.Abstractions.Builders.Model;
using FunctionMonkey.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FunctionMonkey.Compiler.Core.Implementation
{
    internal interface ITriggerReferenceProvider
    {
        Assembly[] GetTriggerReference(AbstractFunctionDefinition functionDefinition);
    }

    internal class TriggerReferenceProvider : ITriggerReferenceProvider
    {
        private static readonly Dictionary<Type, Assembly[]> TriggerReferences = new Dictionary<Type, Assembly[]>
        {
            {typeof(HttpFunctionDefinition), new [] {typeof(HttpTriggerAttribute).Assembly}},
            {typeof(ServiceBusQueueFunctionDefinition), new [] {typeof(ServiceBusTriggerAttribute).Assembly }},
            {typeof(ServiceBusSubscriptionFunctionDefinition), new [] {typeof(ServiceBusTriggerAttribute).Assembly }},
            {typeof(StorageQueueFunctionDefinition), new [] {typeof(QueueTriggerAttribute).Assembly }},
            {typeof(BlobStreamFunctionDefinition), new [] {typeof(BlobTriggerAttribute).Assembly }},
            {typeof(BlobFunctionDefinition), new [] {typeof(BlobTriggerAttribute).Assembly, typeof(QueueMessage).Assembly }},
            {typeof(TimerFunctionDefinition), new [] {typeof(TimerTriggerAttribute).Assembly }},
            {typeof(CosmosDbFunctionDefinition), new [] {typeof(CosmosDBTriggerAttribute).Assembly }},
            {typeof(SignalRCommandNegotiateFunctionDefinition), new [] {typeof(SignalRAttribute).Assembly }},
            {typeof(SignalRBindingExpressionNegotiateFunctionDefinition), new [] {typeof(SignalRAttribute).Assembly }},
            {typeof(SignalRClaimNegotiateFunctionDefinition), new [] {typeof(SignalRAttribute).Assembly }},
            {typeof(EventHubFunctionDefinition), new [] {typeof(EventHubAttribute).Assembly}}
        };

        public Assembly[] GetTriggerReference(AbstractFunctionDefinition functionDefinition)
        {
            if (TriggerReferences.TryGetValue(functionDefinition.GetType(), out Assembly[] assemblies))
            {
                return assemblies;
            }
            throw new ConfigurationException($"No trigger reference mapping configured for a function of type {functionDefinition.GetType().Name}");
        }
    }
}
