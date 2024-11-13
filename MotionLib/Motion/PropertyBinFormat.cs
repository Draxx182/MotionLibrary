using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;
using MotionLibrary.Data_Structures;

namespace MotionLibrary
{
    public abstract class PropertyBinFormat
    {
        public FileHeader Header { get; set; }

        private string[] m_stringTable;

        /// <summary>
        /// Reads the first 16-bytes of the file.
        /// </summary>
        /// <param name="reader">Yarhl Data Reader.</param>
        internal virtual void ReadHeader(DataReader reader)
        {
            FileHeader header = new FileHeader();

            header.Magic = reader.ReadString(4);

            header.Endianness = reader.ReadUInt16();
            reader.Stream.Position += 2;
            header.FileVersion = reader.ReadUInt32();
            header.FileSize = reader.ReadUInt32();
        }

        internal virtual void Read(DataReader rd)
        {
            //ReadPropertyTables(rd);
            ReadHeader(rd);
            ReadMoveData(rd);
           //ReadStringTable(rd);
        }


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
        }

        internal virtual void ReadMoveData(DataReader reader)
        {

        }

        public abstract void ReadPropertyTables(DataReader reader);

        internal virtual void Write(DataWriter wr)
        {
            WritePropertyTables(wr);
        }

        public abstract void WritePropertyTables(DataWriter wr);
    }
}
