﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AgentFramework.Core.Contracts;
using AgentFramework.Core.Exceptions;
using AgentFramework.Core.Handlers.Internal;
using AgentFramework.Core.Utils;
using Hyperledger.Indy.PoolApi;
using Hyperledger.Indy.WalletApi;
using Microsoft.Extensions.DependencyInjection;

namespace AgentFramework.Core.Handlers
{
    /// <summary>
    /// Base agent implementation
    /// </summary>
    public abstract class AgentBase
    {
        private readonly IList<IMessageHandler> _handlers;

        /// <summary>Gets the provider.</summary>
        /// <value>The provider.</value>
        protected IServiceProvider Provider { get; }

        /// <summary>Initializes a new instance of the <see cref="AgentBase"/> class.</summary>
        protected AgentBase(IServiceProvider provider)
        {
            Provider = provider;
            _handlers = new List<IMessageHandler>();
        }

        /// <summary>Adds a handler for supporting default connection flow.</summary>
        protected void AddConnectionHandler()
        {
            _handlers.Add(new DefaultConnectionHandler(Provider.GetService<IConnectionService>()));
            _handlers.Add(new OutgoingMessageHandler());
            _handlers.Add(new HttpOutgoingMessageHandler(Provider.GetService<HttpClientHandler>()
                                                         ?? new HttpClientHandler()));
        }
        /// <summary>Adds a handler for supporting default credential flow.</summary>
        protected void AddCredentialHandler()
        {
            _handlers.Add(new DefaultCredentialHandler(Provider.GetService<ICredentialService>()));
        }
        /// <summary>Adds the handler for supporting default proof flow.</summary>
        protected void AddProofHandler()
        {
            _handlers.Add(new DefaultProofHandler(Provider.GetService<IProofService>()));
        }
        /// <summary>Adds a default forwarding handler.</summary>
        protected void AddForwardHandler()
        {
            _handlers.Add(new DefaultForwardHandler(Provider.GetService<IConnectionService>()));
        }

        /// <summary>Adds a custom the handler using dependency injection.</summary>
        /// <typeparam name="T"></typeparam>
        protected void AddHandler<T>() where T : IMessageHandler => _handlers.Add(Provider.GetService<T>());

        /// <summary>Adds an instance of a custom handler.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        protected void AddHandler<T>(T instance) where T : IMessageHandler => _handlers.Add(instance);

        /// <summary>
        /// Invoke the handler pipeline and process the passed message.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="wallet">The wallet.</param>
        /// <param name="pool">The pool.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Expected inner message to be of type 'ForwardMessage'</exception>
        /// <exception cref="AgentFrameworkException">Couldn't locate a message handler for type {messageType}</exception>
        protected async Task ProcessAsync(byte[] body, Wallet wallet, Pool pool = null)
        {
            EnsureConfigured();

            var agentContext = new AgentContext {Wallet = wallet, Pool = pool};
            agentContext.AddNext(new MessagePayload(body, true));

            while (agentContext.TryGetNext(out var message))
            {
                MessagePayload messagePayload;
                if (message.Packed)
                {
                    var unpacked = await CryptoUtils.UnpackAsync(agentContext.Wallet, message.Payload);
                    messagePayload = new MessagePayload(unpacked.Message, false);
                }
                else
                {
                    messagePayload = message;
                }

                if (_handlers.Where(handler => handler != null).FirstOrDefault(
                        handler => handler.SupportedMessageTypes.Any(
                            type => type.Equals(messagePayload.GetMessageType(), StringComparison.OrdinalIgnoreCase))) is IMessageHandler messageHandler)
                {
                    await messageHandler.ProcessAsync(agentContext, messagePayload);
                }
                else
                {
                    throw new AgentFrameworkException(ErrorCode.InvalidMessage,
                        $"Couldn't locate a message handler for type {messagePayload.GetMessageType()}");
                }
            }
        }

        private void EnsureConfigured()
        {
            if (_handlers == null || !_handlers.Any())
                ConfigureHandlers();
        }

        /// <summary>Configures the handlers.</summary>
        protected virtual void ConfigureHandlers()
        {
            AddConnectionHandler();
            AddForwardHandler();
        }
    }
}