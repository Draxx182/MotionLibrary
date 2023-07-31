using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;
using MotionLibrary.Data_Structures;

namespace MotionLibrary.Motion
{
    public abstract class Property
    {
        public FileHeader Header { get; set; }

        internal virtual void Read(DataReader rd)
        {
            ReadPropertyTables(rd);
        }

        public abstract void ReadPropertyTables(DataReader reader);

        internal virtual void Write(DataWriter wr)
        {
            WritePropertyTables(wr);
        }

        public abstract void WritePropertyTables(DataWriter wr);
    }
}
