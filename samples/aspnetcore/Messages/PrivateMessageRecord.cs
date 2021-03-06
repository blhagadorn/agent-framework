﻿using AgentFramework.Core.Models.Records;
using Newtonsoft.Json;

namespace WebAgent.Messages
{
    /// <summary>
    /// Represents a private message record in the user's wallet
    /// </summary>
    /// <seealso cref="AgentFramework.Core.Models.Records.RecordBase" />
    public class PrivateMessageRecord : RecordBase
    {
        public override string TypeName => "WebAgent.PrivateMessage";

        [JsonIgnore]
        public string ConnectionId
        {
            get => Get();
            set => Set(value);
        }

        public MessageDirection Direction { get; set; }

        public string Text { get; set; }
    }

    public enum MessageDirection
    {
        Incoming,
        Outgoing
    }
}