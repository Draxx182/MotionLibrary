using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace MotionLibrary
{
    public class OEAnimPropertySE : OEAnimProperty
    {
        public ushort Cuesheet;
        public ushort ID;

        public short UnkSE1 = -1;
        public short UnkSE2 = -1;
        public short UnkSE3 = -1;
        public short UnkSE4 = -1;
        public short UnkSE5 = -1;
        public short UnkSE6 = -1;
        public short UnkSE7 = -1;
        public short UnkSE8 = -1;

        internal override void ReadData(DataReader reader)
        {
            Cuesheet = reader.ReadUInt16();
            ID = reader.ReadUInt16();

            UnkSE1 = reader.ReadInt16();
            UnkSE2 = reader.ReadInt16();
            UnkSE3 = reader.ReadInt16();
            UnkSE4 = reader.ReadInt16();
            UnkSE5 = reader.ReadInt16();
            UnkSE6 = reader.ReadInt16();
            UnkSE7 = reader.ReadInt16();
            UnkSE8 = reader.ReadInt16();
        }
    }
}
