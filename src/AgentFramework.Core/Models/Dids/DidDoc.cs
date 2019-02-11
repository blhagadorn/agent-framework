﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AgentFramework.Core.Models.Dids
{
    /// <summary>
    /// Strongly type DID doc model.
    /// </summary>
    public class DidDoc
    {
        /// <summary>
        /// List of public keys available on the DID doc.
        /// </summary>
        [JsonProperty("publicKey")]
        public IList<DidDocKey> Keys { get; set; }

        /// <summary>
        /// List of services available on the did doc.
        /// </summary>
        [JsonProperty("service")]
        public IList<IDidDocServiceEndpoint> Services { get; set; }
    }
}
