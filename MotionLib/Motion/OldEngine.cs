using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yarhl.IO;
using MotionLibrary.Data_Structures;
using MotionLib;

namespace MotionLibrary
{
    public class OldEngineFormat : PropertyBinFormat
    {
        private const int _tableOffset = 28; // Table values are 28 bytes from the end of the file
        public List<OEAnimEntry> Moves = new List<OEAnimEntry>();

        public static OldEngineFormat Read(string path)
        {
            if (!File.Exists(path))
                return null;

            byte[] buf = File.ReadAllBytes(path);

            DataStream readStream = DataStreamFactory.FromArray(buf, 0, buf.Length);
            DataReader reader = new DataReader(readStream) { Endianness = EndiannessMode.BigEndian, DefaultEncoding = Encoding.GetEncoding(932) };

            OldEngineFormat format = new OldEngineFormat();
            format.Read(reader);


            return format;
        }


        internal override void ReadMoveData(DataReader reader)
        {
            reader.Stream.Seek(reader.Stream.Length - 40);

            List<OEAnimEntry> entries = new List<OEAnimEntry>();

            uint numAnimProperties = reader.ReadUInt32();
            int nameTablePtr = reader.ReadInt32();
            int dataTblStart = reader.ReadInt32();

            reader.Stream.Seek(nameTablePtr);

            for(int i = 0; i < numAnimProperties; i++)
            {
                OEAnimEntry entry = new OEAnimEntry();
                entry.Name = reader.ReadStringPointer();

                uint nextEntry = (uint)reader.Stream.Position;

                //Seek to data of anim entry
                reader.Stream.Seek(dataTblStart + (i * 4), SeekMode.Start);

                int dataPtr = reader.ReadInt32();

                reader.Stream.RunInPosition(
                    delegate 
                    {
                        int unk0Val = reader.ReadInt32();
                        int mepIdx = reader.ReadInt32();
                        uint mainTableSize = reader.ReadUInt32();
                        uint movePropertiesStart = reader.ReadUInt32();

                        entry.UnkData = reader.ReadBytes(44);

                        uint propertiesStart = (uint)reader.Stream.Position;

                        short propertyUnk = reader.ReadInt16();
                        ushort propertyCount = reader.ReadUInt16();

                        for(int k = 0; k < propertyCount; k++)
                        {
                            OEAnimProperty property = OEAnimProperty.CreateFromReader(reader, propertiesStart);
                            entry.Properties.Add(property);
                        }

                    }, dataPtr);

                entries.Add(entry);
                reader.Stream.Seek(nextEntry, SeekMode.Start);
            }

            Moves = entries;
        }


        public override void ReadPropertyTables(DataReader rd)
        {
            rd.Stream.Seek(Header.FileSize - _tableOffset, SeekMode.Start);
            DataTables dt = new DataTables(rd);
            dt.ReadOldEngineTables();
        }

        public override void WritePropertyTables(DataWriter wr)
        {
            
        }
    }
}
