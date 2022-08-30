using AzureFromTheTrenches.Commanding.Abstractions;
using System.Collections.Generic;

namespace FunctionMonkey.Commanding.Cosmos.Abstractions
{
    public interface ICosmosDbDocumentBatchCommand<T> : ICommand where T : IDocument
    {
        IReadOnlyCollection<T> Documents { get; set; }
    }

    public interface IDocument
    {
        public string Id { get; set; }
    }
}
