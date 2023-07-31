using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MotionLibrary.Data_Structures
{

    /// <summary>
    /// Animation data that Move Data Blocks contain, persists throughout OE Games.
    /// </summary>
    [Serializable]
    public struct MoveGMTStructOE
    {
        [JsonProperty(PropertyName = "Animation Name (GMT)")]
        public string GMTName { get; set; }

        [JsonProperty(PropertyName = "Directional Input X")]
        public float InputX { get; set; }

        [JsonProperty(PropertyName = "Directional Input Z")]
        public float InputZ { get; set; }

        [JsonProperty(PropertyName = "Directional Input Y")]
        public float InputY { get; set; }

        /// <summary>
        /// The Index of a GMT. Removed in Json due to the inclusion of GMT Indexing.
        /// </summary>
        [JsonIgnore]
        public int GmtIndex { get; set; }
    }
}
