using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class GeneralPatches
    {
        public static void MoveRoomHeaders(RomData romData)
        {
            // TODO: clean this up

            romData[0x0B5E7] = 0x24;//change room header bank to bank to 24

            for (int i = 0; i < 320; i++)
            {
                //get pointer of that room
                byte[] roomPointer = new byte[4];//27502
                roomPointer[0] = romData[(0x271E2 + (i * 2) + 0)];
                roomPointer[1] = romData[(0x271E2 + (i * 2) + 1)];
                roomPointer[2] = 04;
                int address = BitConverter.ToInt32(roomPointer, 0);
                int pcadd = Utilities.SnesToPCAddress(address);

                for (int j = 0; j < 14; j++)
                {
                    romData[0x120090 + (i * 14) + j] = romData[pcadd + j];
                }
            }


            for (int i = 0; i < 320; i++)
            {
                //0x0271E2  //rewrite all room header address
                //0x120090
                romData[0x0271E2 + (i * 2)] = ((byte)Utilities.PCToSnesAddress(0x120090 + (i * 14)));
                romData[0x0271E2 + (i * 2) + 1] = ((byte)(Utilities.PCToSnesAddress((0x120090 + (i * 14))) >> 8));

            }
        }
    }
}
