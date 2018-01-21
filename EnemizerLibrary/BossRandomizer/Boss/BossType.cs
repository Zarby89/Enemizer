using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public enum BossType
    {
        Kholdstare = 0,
        Moldorm = 1,
        Mothula = 2,
        Vitreous = 3,
        Helmasaur = 4,
        Armos = 5,
        Lanmola = 6,
        Blind = 7,
        Arrghus = 8,
        Trinexx = 9,

        // don't use these. they are only for manual settings passed in by randomizer web
        Agahnim = 10,
        Agahnim2 = 11,
        Ganon = 12,

        NoBoss = 255
    }
}
