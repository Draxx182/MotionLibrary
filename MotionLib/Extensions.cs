using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace MotionLib
{
    internal static class Extensions
    {
        public static string ReadStringPointer(this DataReader reader)
        {
            string str = null;
            int pointer = reader.ReadInt32();

            if (pointer < 0)
                return str;

            reader.Stream.RunInPosition(delegate { str = reader.ReadString(); }, pointer, SeekMode.Start);

            return str;
        }
    }
}
