﻿using AgentFramework.Core.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgentFramework.Core.Handlers
{
    /// <summary>
    /// Message handler interface
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Gets the supported message types.
        /// </summary>
        /// <value>
        /// The supported message types.
        /// </value>
        IEnumerable<string> SupportedMessageTypes { get; }

        /// <summary>
        /// Processes the agent message
        /// </summary>
        /// <param name="agentContext">The agent context.</param>
        /// <param name="messagePayload">The agent message context.</param>
        /// <returns></returns>
        Task ProcessAsync(IAgentContext agentContext, MessagePayload messagePayload);
    }
}