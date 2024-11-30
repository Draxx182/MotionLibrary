using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionLibrary.Data_Structures.OE
{
    [JsonObject(MemberSerialization.OptIn)]
    public struct OEAnimStruct
    {
        [JsonProperty("MEP Entry Name", Order = 1)]
        public string GMTName;
        [JsonProperty("X-Analog Direction", Order = 2)]
        public float XAnalog;
        [JsonProperty("Y-Analog Direction", Order = 2)]
        public float YAnalog;
        [JsonProperty("Unknown Value 1", Order = 3)]
        public ushort UnkVal1;
        [JsonProperty("Unknown Value 2", Order = 4)]
        public byte UnkVal2;

    }
}
