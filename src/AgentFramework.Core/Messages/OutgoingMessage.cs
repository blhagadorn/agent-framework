﻿using System;
using Newtonsoft.Json;

namespace AgentFramework.Core.Messages
{
    public class OutgoingMessage : IAgentMessage
    {
        [JsonProperty("@id")] public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("@type")] public string Type { get; set; } = "did:test:123;/spec/internal/outgoing";

        public string OutboundMessage { get; set; }

        public string InboundMessage { get; set; }
    }
}
