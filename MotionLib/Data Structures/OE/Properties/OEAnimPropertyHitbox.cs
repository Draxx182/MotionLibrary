using System;
using Yarhl.IO;

namespace MotionLibrary
{
    public class OEAnimPropertyHitbox : OEAnimProperty
    {
        public HitboxLocation1Flag HitboxLocation1 = 0;
        public ushort HitboxLocation2 = 0;

        public ushort MoveEffect1 = 0;
        public byte Strength = 1;
        public byte HitLocation = 0;

        public ushort Flags = 255;
        public byte Damage = 0;
        public byte Heat = 0;

        public uint MoveEffect2 = 0;

        internal override void ReadData(DataReader reader)
        {
            HitboxLocation1 = (HitboxLocation1Flag)reader.ReadUInt16();
            HitboxLocation2 = reader.ReadUInt16();

            MoveEffect1 = reader.ReadUInt16();
            Strength = reader.ReadByte();
            HitLocation = reader.ReadByte();

            Flags = reader.ReadUInt16();
            Damage = reader.ReadByte();
            Heat = reader.ReadByte();

            MoveEffect2 = reader.ReadUInt32();
        }
    }
}
