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
            int pointerTableBase = AddressConstants.dungeonHeaderPointerTableBaseAddress; // 0x271E2
            int headerBase = AddressConstants.dungeonHeaderBaseAddress; // XkasSymbols.Instance.Symbols["room_header_table"]

            byte newRoomBank = romData[XkasSymbols.Instance.Symbols["moved_room_header_bank_value_address"]]; // AddressConstants.MovedRoomBank;
            romData[AddressConstants.RoomHeaderBankLocation] = newRoomBank; // change room header bank (at 0xB5E7) to bank to 0x40

            // copy header table
            for (int i = 0; i < 320; i++)
            {
                //get pointer of that room
                // pointers are 16bit with hard coded bank
                byte[] roomPointer = new byte[4];
                roomPointer[0] = romData[(pointerTableBase + (i * 2) + 0)];
                roomPointer[1] = romData[(pointerTableBase + (i * 2) + 1)];
                roomPointer[2] = 04; // bank
                int snesAddress = BitConverter.ToInt32(roomPointer, 0);
                int pcAddress = Utilities.SnesToPCAddress(snesAddress);

                // copy the room headers to the new room_header_table
                for (int j = 0; j < 14; j++)
                {
                    romData[headerBase + (i * 14) + j] = romData[pcAddress + j];
                }
            }

            // repoint the pointer table to the new header table
            for (int i = 0; i < 320; i++)
            {
                int snesAddress = Utilities.PCToSnesAddress(headerBase + (i * 14));
                byte low = (byte)(snesAddress & 0xFF);
                byte high = (byte)((snesAddress >> 8) & 0xFF);
                byte bank = (byte)((snesAddress >> 16) & 0xFF);

                //rewrite all room header address
                romData[pointerTableBase + (i * 2)] = low;
                romData[pointerTableBase + (i * 2) + 1] = high;
                // bank is set above. once for all rooms, so need to make sure we are in the same bank at the end
                if(bank != newRoomBank)
                {
                    throw new Exception("We changed banks in the middle of moving the room headers! This should have been caught by dev team, unless you were playing with files you shouldn't touch.");
                }
            }
        }
    }
}
