using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace MotionLibrary.Data_Structures
{
    internal class DataTables
    {
        private DataReader _rd;

        /// <summary>
        /// Amount of moves listed in property.bin.
        /// </summary>
        public int NumMoves;
        /// <summary>
        /// The pointer to the table that points to the move names' strings.
        /// </summary>
        public uint PtrMoveNames;
        /// <summary>
        /// The pointer to the table that points to the data structures holding the data of a move.
        /// </summary>
        public uint PtrMoveData;

        /// <summary>
        /// Amount of GMTs (animations) that are recognized in property.bin.
        /// </summary>
        public int NumGMTs;
        /// <summary>
        /// The pointer to the table that holds pointers to the names of these animations.
        /// </summary>
        public uint PtrGMTNames;

        /// <summary>
        /// Amount of MEPs (Visually Generated Effects) that *can* be recognized in property.bin.
        /// The amount of MEPs have to correlate with the amount of moves, not including NONE (found at the start of every property.bin and ActionSet).
        /// If MEPs are not congruent with the amount of moves, it will cause loading issues when trying to add new MEPs.
        /// </summary>
        public int NumMEPs;
        /// <summary>
        /// The pointer to the table that holds pointers to the names of these visual effects.
        /// </summary>
        public uint PtrMEPNames;

        /// <summary>
        /// Amount of Battle Sets (Found in ActionSet.cas) that are recognized in property.bin.
        /// </summary>
        public int? NumSets;
        /// <summary>
        /// The pointer to the table that holds the names of the BattleSets.
        /// </summary>
        public uint? PtrSetNames;
        /// <summary>
        /// The pointer to the table that recognizes the Data of these BattleSets.
        /// Although the amount of BattleSets have to correlate with the names, this does
        /// not have to correlate with the names of the BattleSets themselves.
        /// </summary>
        public uint? PtrSetData;

        public DataTables(DataReader rd)
        {
            _rd = rd;
        }

        /// <summary>
        /// Reads OE DataTables - Set at the end of the files with an offset of 0x1C, backwards.
        /// </summary>
        public void ReadOldEngineTables()
        {
            NumMoves = _rd.ReadInt32();
            PtrMoveNames = _rd.ReadUInt32();
            PtrMoveData = _rd.ReadUInt32();

            NumGMTs = _rd.ReadInt32();
            PtrGMTNames = _rd.ReadUInt32();

            NumMEPs = _rd.ReadInt32();
            PtrMEPNames = _rd.ReadUInt32();

            NumSets = _rd.ReadInt32();
            PtrSetNames = _rd.ReadUInt32();
            PtrSetData = _rd.ReadUInt32();
        }

        /// <summary>
        /// Reads OOE DataTables - Set at the end of the files with an offset of 0x14, backwards.
        /// </summary>
        public void ReadOlderEngineTables()
        {
            NumMoves = _rd.ReadInt32();
            PtrMoveNames = _rd.ReadUInt32();
            PtrMoveData = _rd.ReadUInt32();

            NumGMTs = _rd.ReadInt32();
            PtrGMTNames = _rd.ReadUInt32();

            NumMEPs = _rd.ReadInt32();
            PtrMEPNames = _rd.ReadUInt32();

            /// OOE property.bin holds no ActionSet properties in property.bin.
            NumSets = null;
            PtrSetNames = null;
            PtrSetData = null;
        }
    }
}
