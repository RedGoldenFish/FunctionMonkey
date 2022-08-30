using AzureFromTheTrenches.Commanding.Abstractions;

namespace FunctionMonkey.Commanding.Cosmos.Abstractions
{
    public interface ICosmosDbDocumentCommand<T> : ICommand where T : IDocument
    {
        T Document { get; set; }
    }
}
