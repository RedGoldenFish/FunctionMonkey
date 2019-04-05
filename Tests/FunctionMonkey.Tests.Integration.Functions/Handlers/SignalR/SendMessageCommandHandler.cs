﻿using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions.SignalR;
using FunctionMonkey.Tests.Integration.Functions.Commands.SignalR;

namespace FunctionMonkey.Tests.Integration.Functions.Handlers.SignalR
{
    internal class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, SignalRMessage>
    {
        public Task<SignalRMessage> ExecuteAsync(SendMessageCommand command, SignalRMessage previousResult)
        {
            return Task.FromResult(new SignalRMessage
            {
                Arguments = new object[] {command.Message},
                GroupName = null,
                Target = "sendMessageCommand",
                UserId = null
            });
        }
    }
}
