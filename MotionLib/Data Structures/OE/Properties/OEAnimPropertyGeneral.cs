using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MotionLibrary
{
    [JsonObject(MemberSerialization.OptIn)]
    public struct OEAnimPropertyGeneral
    {
        // Order different from what order they are read in.
        [JsonProperty("Start Frame", Order = 2)]
        public ushort Start;
        [JsonProperty("End Frame", Order = 3)]
        public ushort End;
        [JsonProperty("Modifier Value", Order = 7)]
        public byte Modifier;

        [JsonProperty("Control Value 1", Order = 4)]
        public byte Control1;
        [JsonProperty("Control Value 2", Order = 5)]
        public byte Control2;
        [JsonProperty("Property Type ID", Order = 1)]
        public OEPropertyType Type;
        [JsonProperty("Control Value 3", Order = 6)]
        public int Control3;
    }
}
