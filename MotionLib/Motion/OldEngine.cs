using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yarhl.IO;
using MotionLibrary.Data_Structures;

namespace MotionLibrary.Motion
{
    public class OldEngine : Property
    {
        private const int _tableOffset = 28; // Table values are 28 bytes from the end of the file
        public List<MoveBlockOE> ListOfMoves = new List<MoveBlockOE>();

        public OldEngine(FileHeader h)
        {
            Header = h;
        }

        public override void ReadPropertyTables(DataReader rd)
        {
            rd.Stream.Seek(Header.FileSize - _tableOffset, SeekMode.Start);
            DataTables dt = new DataTables(rd);
            dt.ReadOldEngineTables();

            ReadMoveTable(rd, dt);
        }

        private void ReadMoveTable(DataReader rd, DataTables dt)
        {
            rd.Stream.Seek(dt.PtrMoveNames, SeekMode.Start);
            for (int i = 0; i < 50 /*dt.NumMoves*/; i++)
            {
                MoveBlockOE moveBlock = new MoveBlockOE();
                moveBlock.ReadMoveName(rd);
                ListOfMoves.Add(moveBlock);
            }

            rd.Stream.Seek(dt.PtrMoveData, SeekMode.Start);
            foreach (MoveBlockOE mb in ListOfMoves)
            {
                mb.ReadMoveData(rd);
            }
        }

        public override void WritePropertyTables(DataWriter wr)
        {
            
        }
    }
}
