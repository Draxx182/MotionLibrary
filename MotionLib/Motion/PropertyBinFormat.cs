using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;
using MotionLibrary.Data_Structures;
using System.Reflection.PortableExecutable;

namespace MotionLibrary
{
    public abstract class PropertyBinFormat
    {
        public FileHeader Header { get; set; }
        internal DataTables? _tables;
        public List<String> MEPs = new List<String>();
        public List<String> GMTs = new List<String>();

        // ========================
        // Read Property.bin
        // ========================

        /// <summary>
        /// Read a file by its DataReader.
        /// </summary>
        /// <param name="reader">Yarhl Data Reader.</param>
        internal virtual void Read(DataReader reader)
        {
            ReadHeader(reader);
            ReadDataTables(reader);
            ReadMEPList(reader);
            ReadGMTList(reader);
            ReadMoveData(reader);
            reader.Stream.Dispose();
        }

        /// <summary>
        /// Reads the first 16-bytes of the file. Should be universal.
        /// </summary>
        /// <param name="reader">Yarhl Data Reader.</param>
        internal virtual void ReadHeader(DataReader reader)
        {
            FileHeader header = new FileHeader();

            header.Magic = reader.ReadString(4);
            header.Endianness = reader.ReadUInt16();
            reader.Stream.Position += 2; // Padding for endianess
            header.FileVersion = reader.ReadUInt32();
            header.FileSize = reader.ReadUInt32();

            Header = header;
        }

        internal abstract void ReadDataTables(DataReader reader);

        internal virtual void ReadMEPList(DataReader reader)
        {
            if (_tables is null) throw new DataTablesNullException();

            reader.Stream.Seek(_tables.PtrMEPNames);
            for (int i = 0; i < _tables.NumMEPs; i++)
            {
                string mep = reader.ReadStringPointer() ?? "";
                MEPs.Add(mep);
            }
        }

        internal virtual void ReadGMTList(DataReader reader)
        {
            if (_tables is null) throw new DataTablesNullException();

            reader.Stream.Seek(_tables.PtrGMTNames);
            for (int i = 0; i < _tables.NumGMTs; i++)
            {
                string gmt = reader.ReadStringPointer() ?? "";
                GMTs.Add(gmt);
            }
        }

        internal abstract void ReadMoveData(DataReader reader);

        // ========================
        // Write Property.bin
        // ========================

        internal virtual void Write(DataWriter wr)
        {
            WriteDataTables(wr);
        }

        internal abstract void WriteDataTables(DataWriter wr);
    }
}
