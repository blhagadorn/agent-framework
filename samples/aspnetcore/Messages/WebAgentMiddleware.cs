﻿using System;
using System.Collections.Generic;
using AgentFramework.AspNetCore.Middleware;
using AgentFramework.AspNetCore.Options;
using AgentFramework.Core.Contracts;
using AgentFramework.Core.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace WebAgent.Messages
{
    public class WebAgentMiddleware : AgentMiddleware
    {
        public WebAgentMiddleware(
            RequestDelegate next,
            IWalletService walletService,
            IServiceProvider serviceProvider,
            IOptions<WalletOptions> walletOptions)
            : base(next, walletService, serviceProvider, walletOptions)
        {
        }

        public override IEnumerable<IMessageHandler> Handlers => new IMessageHandler[]
        {
            ServiceProvider.GetService<DefaultConnectionHandler>(),
            ServiceProvider.GetService<PrivateMessageHandler>()
        };
    }
}