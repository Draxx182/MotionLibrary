using Newtonsoft.Json;
using System;
using System.Net;
using Yarhl.IO;

namespace MotionLibrary
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OEAnimProperty
    {

        [JsonProperty("Property Data")]
        public OEAnimPropertyGeneral propertyData;

        public byte[] UnreadBytes;

        internal static OEAnimProperty CreateFromReader(DataReader reader, uint propertiesStart)
        {
            byte propertyType = 0;

            reader.Stream.RunInPosition(delegate { propertyType = reader.ReadByte(); }, reader.Stream.Position + 7, SeekOrigin.Begin);

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

            createdProperty.propertyData.Start = reader.ReadUInt16();
            createdProperty.propertyData.End = reader.ReadUInt16();

            createdProperty.propertyData.Modifier = reader.ReadByte();

            createdProperty.propertyData.Control1 = reader.ReadByte();
            createdProperty.propertyData.Control2 = reader.ReadByte();

            OEPropertyType pType = (OEPropertyType)reader.ReadByte();
            createdProperty.propertyData.Type = pType;

            createdProperty.propertyData.Control3 = reader.ReadInt32();

            int dataPtr = reader.ReadInt32();
            int dataAbsolutePos = (int)propertiesStart + dataPtr;

            if(dataPtr > 0)
                reader.Stream.RunInPosition(delegate { createdProperty.ReadData(reader); }, dataAbsolutePos, SeekOrigin.Begin);

            return createdProperty;
        }

        internal virtual void ReadData(DataReader reader)
        {

        }

        public override string ToString()
        {
            return $"Type ({Extensions.GetEnumDescription(propertyData.Type)})";
        }

    }
}
