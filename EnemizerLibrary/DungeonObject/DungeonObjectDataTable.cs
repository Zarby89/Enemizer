using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonObjectDataTable
    {
        public byte HeaderByte0 { get; set; }
        public byte HeaderByte1 { get; set; }
        public List<LayerDungeonObject> Layer1Objects { get; set; } = new List<LayerDungeonObject>();
        public List<DoorDungeonObject> Layer1DoorObjects { get; set; } = new List<DoorDungeonObject>();
        public List<LayerDungeonObject> Layer2Objects { get; set; } = new List<LayerDungeonObject>();
        public List<DoorDungeonObject> Layer2DoorObjects { get; set; } = new List<DoorDungeonObject>();
        public List<LayerDungeonObject> Layer3Objects { get; set; } = new List<LayerDungeonObject>();
        public List<DoorDungeonObject> Layer3DoorObjects { get; set; } = new List<DoorDungeonObject>();

        RomData romData;
        int startAddress;

        public int Length
        {
            get
            {
                var length = 2; // header bytes

                length += Layer1Objects.Count * 3;
                if(Layer1DoorObjects.Count > 0)
                {
                    // 0xFF 0xFF might not be there if there are no doors on this layer
                    length += 2;
                }
                length += Layer1DoorObjects.Count * 3;

                length += 2; // 0xFF 0xFF

                length += Layer2Objects.Count * 3;
                if (Layer2DoorObjects.Count > 0)
                {
                    // 0xFF 0xFF might not be there if there are no doors on this layer
                    length += 2;
                }
                length += Layer2DoorObjects.Count * 3;

                length += 2; // 0xFF 0xFF

                length += Layer3Objects.Count * 3;

                length += 2; // 0xF0 0xFF // as far as I can tell this is always in the data even if there are no doors

                length += Layer3DoorObjects.Count * 2;

                length += 2; // 0xFF 0xFF

                return length;
            }
        }

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

            LoadData(index);
        }

        int LoadData(int index)
        {
            List<LayerDungeonObject> currentLayer = this.Layer1Objects;
            List<DoorDungeonObject> currentDoorLayer = this.Layer1DoorObjects;

            bool isDoor = false;

            while (true)
            {
                if (IsDoorStartChunk(index))
                {
                    isDoor = true;
                    index += 2;
                }

                if (IsEndingChunk(index))
                {
                    if(currentLayer == this.Layer1Objects || currentDoorLayer == this.Layer1DoorObjects)
                    {
                        currentLayer = this.Layer2Objects;
                        currentDoorLayer = this.Layer2DoorObjects;
                    }
                    else if (currentLayer == this.Layer2Objects || currentDoorLayer == this.Layer2DoorObjects)
                    {
                        currentLayer = this.Layer3Objects;
                        currentDoorLayer = this.Layer3DoorObjects;
                    }
                    else if (currentLayer == this.Layer3Objects || currentDoorLayer == this.Layer3DoorObjects)
                    {
                        return index;
                    }

                    isDoor = false;
                    index += 2;
                    continue;
                }

                if (isDoor)
                {
                    currentDoorLayer.Add(DoorDungeonObject.GetDungeonObjectFromBytes(romData.GetDataChunk(index, 2)));
                    index += 2;
                }
                else
                {
                    currentLayer.Add(LayerDungeonObject.GetDungeonObjectFromBytes(romData.GetDataChunk(index, 3)));
                    index += 3;
                }

            }
        }

        public void WriteRom(RomData romData, int newAddress)
        {
            romData[newAddress++] = HeaderByte0;
            romData[newAddress++] = HeaderByte1;

            newAddress = WriteLayer(Layer1Objects, Layer1DoorObjects, newAddress, false);
            newAddress = WriteLayer(Layer2Objects, Layer2DoorObjects, newAddress, false);
            newAddress = WriteLayer(Layer3Objects, Layer3DoorObjects, newAddress, true);

            romData[newAddress++] = 0xFF;
            romData[newAddress++] = 0xFF;
        }

        int WriteLayer(List<LayerDungeonObject> layerObjects, List<DoorDungeonObject> layerDoors, int newAddress, bool isLastLayer)
        {
            foreach (var l1 in layerObjects)
            {
                romData[newAddress++] = l1.Bytes[0];
                romData[newAddress++] = l1.Bytes[1];
                romData[newAddress++] = l1.Bytes[2];
            }
            if (isLastLayer || layerDoors.Count > 0)
            {
                romData[newAddress++] = 0xF0;
                romData[newAddress++] = 0xFF;
            }
            foreach (var l1 in layerDoors)
            {
                romData[newAddress++] = l1.Bytes[0];
                romData[newAddress++] = l1.Bytes[1];
            }

            romData[newAddress++] = 0xFF;
            romData[newAddress++] = 0xFF;

            return newAddress;
        }
        bool IsEndingChunk(int index)
        {
            return romData[index] == 0xFF && romData[index + 1] == 0xFF;
        }

        bool IsDoorStartChunk(int index)
        {
            return romData[index] == 0xF0 && romData[index + 1] == 0xFF;
        }
    }
}
