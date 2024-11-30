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
        //private string[] m_stringTable;
        internal DataTables? _tables;

        // ========================
        // Read Property.bin
        // ========================

        /// <summary>
        /// Read a file by its DataReader.
        /// </summary>
        /// <param name="rd"></param>
        internal virtual void Read(DataReader reader)
        {
            ReadHeader(reader);
            ReadDataTables(reader);
            ReadMoveData(reader);
            //ReadStringTable(rd);
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

        /// <summary>
        /// Reads the string table. Defunct for now, as Move Data naturally reads off its name first.
        /// </summary>
        /*
        internal virtual void ReadStringTable(DataReader reader)
        {
            List<string> strings = new List<string>();

            while(true)
            {
                byte[] strTableEnd = reader.ReadBytes(4);

                //String table end
                if (strTableEnd[0] == 204 &&
                    strTableEnd[1] == 204 &&
                    strTableEnd[2] == 204 &&
                    strTableEnd[3] == 0)
                    break;
                else
                    reader.Stream.Position -= 4;

                strings.Add(reader.ReadString());
            }

            m_stringTable = strings.ToArray();
        }*/

        internal abstract void ReadDataTables(DataReader reader);

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
