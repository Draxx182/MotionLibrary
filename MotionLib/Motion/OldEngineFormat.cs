using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yarhl.IO;
using MotionLibrary.Data_Structures;
using MotionLibrary.Data_Structures.OE;

namespace MotionLibrary
{
    public class OldEngineFormat : PropertyBinFormat
    {
        private const int _tableOffset = 40; // Table values are 28 bytes from the end of the file
        public List<OEAnimEntry> Moves = new List<OEAnimEntry>();

        // ========================
        // Read Property.bin
        // ========================

        /// <summary>
        /// Read a given file path.
        /// </summary>
        /// <returns>Returns an object of this format
        /// or a null value if the file path does not exist.</returns>
        public static OldEngineFormat? Read(string path)
        {
            if (!File.Exists(path))
                return null;

            // Reads the file and then creates a DataStream and Reader from it.
            byte[] buf = File.ReadAllBytes(path);
            DataStream readStream = DataStreamFactory.FromArray(buf, 0, buf.Length);
            DataReader reader = new DataReader(readStream) { Endianness = EndiannessMode.BigEndian, DefaultEncoding = Encoding.GetEncoding(932) };
            // Creates a new object and calls the superclass' read with the newly created DataReader.
            OldEngineFormat format = new OldEngineFormat();
            format.Read(reader);

            return format;
        }

        internal override void ReadDataTables(DataReader rd)
        {
            // Read the Data Tables at the end of the end of the file.
            rd.Stream.Seek(Header.FileSize - _tableOffset, SeekOrigin.Begin);
            DataTables dt = new DataTables(rd);
            dt.ReadOldEngineTables();
            _tables = dt;
        }

        internal override void ReadMoveData(DataReader reader)
        {
            List<OEAnimEntry> entries = new List<OEAnimEntry>();

            // Data Tables needed for this.
            if (_tables is null)
            {
                throw new DataTablesNullException();
            }
            reader.Stream.Seek(_tables.PtrMoveNames);

            for (int i = 0; i < _tables.NumMoves; i++)
            {
                // Creates new entry to add to the list.
                OEAnimEntry entry = new OEAnimEntry();
                entry.Name = reader.ReadStringPointer() ?? "";

                uint nextEntry = (uint)reader.Stream.Position;

                //Seek to data of anim entry
                reader.Stream.Seek(_tables.PtrMoveData + (i * 4), SeekOrigin.Begin);

                int dataPtr = reader.ReadInt32();

                // Goes to the data block itself and reads its values.
                reader.Stream.RunInPosition(
                    delegate 
                    {
                        entry.UnkVal0 = reader.ReadInt32();
                        int mepIdx = reader.ReadInt32();
                        uint mainTableSize = reader.ReadUInt32();
                        uint movePropertiesStart = reader.ReadUInt32();

                        entry.BlendFrames = reader.ReadUInt16();
                        entry.SpeedShift = reader.ReadUInt16();
                        entry.UnkVal1 = reader.ReadByte();
                        reader.SkipPadding(2);
                        // Won't write number of shifts to reader anymore.
                        int numOfAnimations = reader.ReadByte();
                        reader.SkipPadding(24);

                        for (int k = 0; k < numOfAnimations; k++)
                        {
                            OEAnimStruct animStructure = new OEAnimStruct();
                            reader.ReadInt32();
                            animStructure.GMTName = "";
                            animStructure.XAnalog = reader.ReadSingle();
                            animStructure.YAnalog = reader.ReadSingle();
                            animStructure.UnkVal1 = reader.ReadUInt16();
                            animStructure.UnkVal2 = reader.ReadByte();
                            reader.SkipPadding(2);
                            entry.Animations.Add($"Animation Entry {k}", animStructure);
                        }

                        uint propertiesStart = (uint) dataPtr + movePropertiesStart;
                        reader.Stream.Seek(propertiesStart, SeekOrigin.Begin);

                        short propertyUnk = reader.ReadInt16();
                        ushort propertyCount = reader.ReadUInt16();
                        for(int k = 0; k < propertyCount; k++)
                        {
                            OEAnimProperty property = OEAnimProperty.CreateFromReader(reader, propertiesStart);
                            entry.Properties.Add($"Property {k} | {property}", property);
                        }

                    }, dataPtr);

                entries.Add(entry);
                reader.Stream.Seek(nextEntry, SeekOrigin.Begin);
            }

            Moves = entries;
        }

        // ========================
        // Write Property.bin
        // ========================

        internal override void WriteDataTables(DataWriter wr)
        {
            
        }
    }
}
