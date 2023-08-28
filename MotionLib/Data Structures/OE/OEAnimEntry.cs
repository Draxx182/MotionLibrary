using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionLib
{
    public class OEAnimEntry
    {
        public string Name;
        public string MEPName;

        public int UnkVal0;
        public byte[] UnkData;

        public List<OEAnimProperty> Properties = new List<OEAnimProperty>();

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return "Blank Anim Property Entry";

            return Name;
        }
    }
}
