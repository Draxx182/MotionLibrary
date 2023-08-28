using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionLibrary.Data_Structures
{
    public struct FileHeader
    {
        /// <summary>
        /// Used to identify file types. Always four characters in Yakuza games.
        /// </summary>
        public string Magic;

        /// <summary>
        /// Determines the endianness of a file - Unsure if this is actually the identifier
        /// but this is usually manually written either way.
        /// </summary>
        public ushort Endianness;

        /// <summary>
        /// Will be four bytes in length, does *not* always determine exact file structure.
        /// </summary>
        public uint FileVersion;

        /// <summary>
        /// Only some files use and write in a filesize. Usually ones with pointers at the
        /// end of a file. Needs to be rewritten at the repacking of a file if used.
        /// </summary>
        public uint FileSize;

        /// <summary>
        /// File headers of Old Engine games (Yakuza 5, Yakuza 0, Yakuza Kiwami 1)
        /// </summary>
        public static FileHeader PropertyOldEngine
        {
            get
            {
                FileHeader fileHeader = new FileHeader();
                fileHeader.Magic = "CAPR";
                fileHeader.Endianness = 513;
                fileHeader.FileVersion = 7;
                fileHeader.FileSize = 0; // Still needs to be set.

                return fileHeader;
            }
        }

        /// <summary>
        /// File headers of Older Engine games (Yakuza 3, Yakuza 4)
        /// </summary>
        public static FileHeader PropertyOlderEngine
        {
            get
            {
                FileHeader fileHeader = PropertyOldEngine;
                fileHeader.FileVersion = 5;

                return fileHeader;
            }
        }
    }
}
