using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonProperties
    {
        public string name = "";
        public byte boss = 255;
        public int pointerAddr = -1;
        public int room = 0;
        public DungeonProperties(string name,int pointerAddr,int room)
        {
            this.name = name;
            this.pointerAddr = pointerAddr;
            this.room = room;
        }


    }
}
