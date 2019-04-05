using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Tests.Integration.Functions.Commands.Model;
using FunctionMonkey.Tests.Integration.Functions.Commands.TestInfrastructure;

namespace FunctionMonkey.Tests.Integration.Functions.Commands.OutputBindings
{
    public class HttpTriggerStorageQueueOutputCommand : ICommand<StorageQueuedMarkerIdCommand>
    {
        public Guid MarkerId { get; set; }
    }
}