using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yarhl.IO;

namespace MotionLibrary.Data_Structures
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public struct MoveDataOE
    {
        // Variables that don't get read in json.
        public int MepIndex;
        public uint Size;
        public uint PropertyTableOffset;

        /// <summary>
        /// Possibly correlates to the type of data used in Move Blocks
        /// (Always reads 16, maybe it's a short value?)
        /// </summary>
        [JsonProperty(PropertyName = "Unk-Byte Value")]
        public int Unk1;

        [JsonProperty(PropertyName = "Visual Effect ID")]
        public string MEPName;

        // These unks in particular don't have much documentation
        // on what they actually do.
        [JsonProperty(PropertyName = "Unk-1")]
        public short Unk2;
        [JsonProperty(PropertyName = "Unk-2")]
        public short Unk3;
        [JsonProperty(PropertyName = "Unk-3")]
        public short Unk4;
        [JsonProperty(PropertyName = "Unk-4")]
        public short Unk5;
        // Possible Filler
        [JsonProperty(PropertyName = "Unk-5")]
        public long Unk6;

        [JsonProperty(PropertyName = "Animation Block Section")]
        public List<MoveGMTStructOE> GMTBlocks;

        [JsonProperty(PropertyName = "Unk-6")]
        public short Unk7;
        [JsonProperty(PropertyName = "Unk-7")]
        public short Unk8;
        // Other Possible Filler
        [JsonProperty(PropertyName = "Unk-8")]
        public long Unk9;
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public struct MovePropertiesY0
    {
        private uint _numProperties;
        // Work on this later.
        public List<int> Properties;
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class MoveBlockOE
    {
        private const int baseVariableSize = 36;
        public string MoveName = "";
        public MoveDataOE Data = new MoveDataOE();

        /// <summary>
        /// Once data stream is in appropriate table, it reads the pointer where the move's string is set.
        /// After the string has been read, the stream goes back to the former table.
        /// </summary>
        /// <param name="rd">Data stream to read from file's variables.</param>
        public void ReadMoveName(DataReader rd)
        {
            var GoPtr = rd.ReadUInt32();
            var ReturnPtr = rd.Stream.Position;
            rd.Stream.Seek(GoPtr);
            MoveName = rd.ReadString();
            rd.Stream.Seek(ReturnPtr);
        }

        /// <summary>
        /// Once data stream is in appropriate table, it reads the pointer where the move's data is identified.
        /// After the data has been read, the stream goes back to the former table.
        /// </summary>
        /// <param name="rd">Data stream to read from file's variables.</param>
        public void ReadMoveData(DataReader rd)
        {
            var GoPtr = rd.ReadUInt32();
            var ReturnPtr = rd.Stream.Position;
            rd.Stream.Seek(GoPtr);

            Data.Unk1 = rd.ReadInt32();
            Data.MepIndex = rd.ReadInt32();
            Data.Size = rd.ReadUInt32();
            Data.PropertyTableOffset = rd.ReadUInt32();
            var GMTTableSize = Data.Size - baseVariableSize;

            Data.Unk2 = rd.ReadInt16();
            Data.Unk3 = rd.ReadInt16();
            Data.Unk4 = rd.ReadInt16();
            Data.Unk5 = rd.ReadInt16();
            Data.Unk6 = rd.ReadInt64();

            for (int i = 0; i < GMTTableSize; i += 12)
            {
                MoveGMTStructOE gmtBlock = new MoveGMTStructOE();
                gmtBlock.GmtIndex = rd.ReadInt32();
                gmtBlock.GMTName = "";
                gmtBlock.InputX = rd.ReadSingle();
                gmtBlock.InputZ = rd.ReadSingle();
                gmtBlock.InputY = rd.ReadSingle();
                Data.GMTBlocks.Add(gmtBlock);
            }

            Data.Unk7 = rd.ReadInt16();
            Data.Unk8 = rd.ReadInt16();
            Data.Unk9 = rd.ReadInt64();

            rd.Stream.Seek(ReturnPtr);
        }
    }
}
