using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public class BossNodes
    {
        public BossNodes()
        {

        }
        
        public Dictionary<string, LogicalBoss> Nodes = new Dictionary<string, LogicalBoss>()
        {
            { "[Agahnim Boss]", new LogicalBoss("[Agahnim Boss]", "Agahnim 1", "L2 Sword;Bug Catching Net;Hammer") },
            { "[Desert Boss]", new LogicalBoss("[Desert Boss]", "Lanmolas", "") },
            { "[Eastern Boss]", new LogicalBoss("[Eastern Boss]", "Armos Knights", "") },
            { "[Ganon]", new LogicalBoss("[Ganon]", "Ganon", "L2 Sword") },
            { "[Agahnim 2 Boss]", new LogicalBoss("[Agahnim 2 Boss]", "Agahnim 2", "L2 Sword;Bug Catching Net;Hammer") },
            { "[GT Armos Boss]", new LogicalBoss("[GT Armos Boss]", "Armos Knights", "") },
            { "[GT Lanmolas Boss]", new LogicalBoss("[GT Lanmolas Boss]", "Lanmolas", "") },
            { "[GT Moldorm Boss]", new LogicalBoss("[GT Moldorm Boss]", "Moldorm", "") },
            { "[Hera Boss]", new LogicalBoss("[Hera Boss]", "Moldorm", "") },
            { "[Ice Boss]", new LogicalBoss("[Ice Boss]", "Kholdstare", "Fire Rod;Bombos,L1 Sword") },
            { "[Mire Boss]", new LogicalBoss("[Mire Boss]", "Vitreous", "") },
            { "[PoD Boss]", new LogicalBoss("[PoD Boss]", "Helmasaur King", "") },
            { "[Skull Boss]", new LogicalBoss("[Skull Boss]", "Mothula", "") },
            { "[Swamp Boss]", new LogicalBoss("[Swamp Boss]", "Arrghus", "Hookshot") },
            { "[Thieves Boss]", new LogicalBoss("[Thieves Boss]", "Blind", "") },
            { "[Turtle Boss]", new LogicalBoss("[Turtle Boss]", "Trinexx", "Fire Rod,Ice Rod") },
        };
    }
}
