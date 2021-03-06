﻿using System;
using AgentFramework.Core.Contracts;
using AgentFramework.Core.Handlers;
using System.Threading.Tasks;
using AgentFramework.Core.Models;
using AgentFramework.Core.Models.Messaging;
using AgentFramework.Core.Models.Records;

namespace WebAgent.Messages
{
    public class PrivateMessageHandler : MessageHandlerBase<PrivateMessage>
    {
        private readonly IWalletRecordService _recordService;

        public PrivateMessageHandler(IWalletRecordService recordService)
        {
            _recordService = recordService;
        }

        protected override Task ProcessAsync(PrivateMessage message, AgentContext context, ConnectionRecord connection)
        {
            Console.WriteLine($"Processing message by {connection.Id}");

            return _recordService.AddAsync(context.Wallet, new PrivateMessageRecord
            {
                Id = Guid.NewGuid().ToString(),
                ConnectionId = connection.Id,
                Text = message.Text,
                Direction = MessageDirection.Incoming
            });
        }
    }
}