using MotionLibrary.Data_Structures.OE;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionLibrary
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OEAnimEntry
    {
        public string? Name { get; set; }

        [JsonProperty("Unknown Value 0", Order = 1)]
        public int UnkVal0;
        [JsonProperty("Blend-In Frames", Order = 2)]
        public ushort BlendFrames;
        [JsonProperty("Shift Animation Frame", Order = 3)]
        public ushort SpeedShift;
        [JsonProperty("Shift Movement Speed", Order = 4)]
        public byte MovementSpeed;
        [JsonProperty("Unknown Value 1", Order = 5)]
        public byte UnkVal1;
        [JsonProperty("Unknown Value 2", Order = 6)]
        public byte UnkVal2;
        [JsonProperty("Unknown Value 3", Order = 7)]
        public byte UnkVal3;


        [JsonProperty("MEP Entry Name", Order = 8)]
        public string MEPName = string.Empty;
        [JsonProperty("Animation List", Order = 9)]
        public Dictionary<string, OEAnimStruct> Animations = new Dictionary<string, OEAnimStruct>();

        [JsonProperty("Move Properties", Order = 10)]
        public Dictionary<string, OEAnimProperty> Properties = new Dictionary<string, OEAnimProperty>();

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return "Blank Anim Property Entry";

            return Name;
        }
    }
}
