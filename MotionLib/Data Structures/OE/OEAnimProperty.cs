using System;
using System.Net;
using Yarhl.IO;

namespace MotionLib
{
    public class OEAnimProperty
    {
        public ushort Start;
        public ushort End;

        public byte Modifier;

        public byte Unk1;
        public byte Unk2;

        public OEPropertyType Type;

        public int Unk3;

        public byte[] UnreadBytes;

        internal static OEAnimProperty CreateFromReader(DataReader reader, uint propertiesStart)
        {
            byte propertyType = 0;

            reader.Stream.RunInPosition(delegate { propertyType = reader.ReadByte(); }, reader.Stream.Position + 7, SeekMode.Start);


            OEAnimProperty createdProperty = new OEAnimProperty();

            switch(propertyType)
            {
                default:
                    createdProperty = new OEAnimProperty();
                    break;
                case 1:
                    createdProperty = new OEAnimPropertyVoiceSE();
                    break;
                case 2:
                    createdProperty = new OEAnimPropertySE();
                    break;
                case 5:
                    createdProperty = new OEAnimPropertyHitbox();
                    break;
            }

            createdProperty.Start = reader.ReadUInt16();
            createdProperty.End = reader.ReadUInt16();

            createdProperty.Modifier = reader.ReadByte();

            createdProperty.Unk1 = reader.ReadByte();
            createdProperty.Unk2 = reader.ReadByte();

            createdProperty.Type = (OEPropertyType)reader.ReadByte();

            createdProperty.Unk3 = reader.ReadInt32();

            int dataPtr = reader.ReadInt32();
            int dataAbsolutePos = (int)propertiesStart + dataPtr;

            if(dataPtr > 0)
                reader.Stream.RunInPosition(delegate { createdProperty.ReadData(reader); }, dataAbsolutePos, SeekMode.Start);

            return createdProperty;
        }

        internal virtual void ReadData(DataReader reader)
        {

        }

        public override string ToString()
        {
            return $"Property ({Type})";
        }

    }
}
