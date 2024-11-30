using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yarhl.IO;

namespace MotionLibrary
{
    internal static class Extensions
    {
        public static string? ReadStringPointer(this DataReader reader)
        {
            string? str = null;
            int pointer = reader.ReadInt32();

            if (pointer < 0)
                return str;

            reader.Stream.RunInPosition(delegate { str = reader.ReadString(); }, pointer, SeekOrigin.Begin);

            return str;
        }

        public static string GetEnumDescription(Enum value)
        {
            if (value == null) { return ""; }

            DescriptionAttribute? attribute = value.GetType()
                    .GetField(value.ToString())
                    ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        // https://stackoverflow.com/questions/3917086/convert-bitarray-to-string
        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
