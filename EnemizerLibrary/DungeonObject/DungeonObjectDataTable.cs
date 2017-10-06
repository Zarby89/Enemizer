using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonObjectDataTable
    {
        public byte HeaderByte0 { get; private set; }
        public byte HeaderByte1 { get; private set; }
        public List<Type1DungeonObject> Layer1Objects { get; set; } = new List<Type1DungeonObject>();
        public List<Type1DungeonObject> Layer2Objects { get; set; } = new List<Type1DungeonObject>();
        public List<Type1DungeonObject> Layer3Objects { get; set; } = new List<Type1DungeonObject>();
        public List<Type2DungeonObject> Type2Objects { get; set; } = new List<Type2DungeonObject>();

        RomData romData;
        int startAddress;

        public DungeonObjectDataTable(RomData romData, int startAddress)
        {
            this.romData = romData;
            this.startAddress = startAddress;

            LoadData();
        }

        void LoadData()
        {
            HeaderByte0 = romData[startAddress];
            HeaderByte1 = romData[startAddress + 1];

            int index = startAddress + 2;

            index = LoadType1Chunks(index);
            index = LoadType2Chunks(index);

        }

        int LoadType1Chunks(int index)
        {
            List<Type1DungeonObject> currentLayer = this.Layer1Objects;
            while (true)
            {
                if (IsEndingChunk(index))
                {
                    if(currentLayer == this.Layer1Objects)
                    {
                        currentLayer = this.Layer2Objects;
                    }
                    else if (currentLayer == this.Layer2Objects)
                    {
                        currentLayer = this.Layer3Objects;
                    }
                    else if (currentLayer == this.Layer3Objects && !IsType2StartChunk(index + 2))
                    {
                        throw new Exception("There is no layer 4. Something went wrong loading dungeon objects");
                    }
                    index += 2;
                    continue;
                }
                if(IsType2StartChunk(index))
                {
                    return index + 2;
                }

                currentLayer.Add(Type1DungeonObject.GetDungeonObjectFromBytes(romData.GetDataChunk(index, 3)));

                index += 3;
            }
        }

        int LoadType2Chunks(int index)
        {
            while(true)
            {
                if(IsEndingChunk(index))
                {
                    return index + 2;
                }
                Type2Objects.Add(Type2DungeonObject.GetDungeonObjectFromBytes(romData.GetDataChunk(index, 2)));
                index += 2;
            }
        }

        bool IsEndingChunk(int index)
        {
            return romData[index] == 0xFF && romData[index + 1] == 0xFF;
        }

        bool IsType2StartChunk(int index)
        {
            return romData[index] == 0xF0 && romData[index + 1] == 0xFF;
        }
    }
}
