using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OptionFlags
    {
        public bool RandomizeEnemies { get; set; }
        public RandomizeEnemiesType RandomizeEnemiesType { get; set; } = RandomizeEnemiesType.Chaos; // default to Chaos

        public bool RandomizeEnemyHealthRange { get; set; }
        public int RandomizeEnemyHealthRangeAmount { get; set; }

        public bool RandomizeEnemyDamage { get; set; }
        public bool AllowEnemyZeroDamage { get; set; }
    }

    public enum RandomizeEnemiesType
    {
        Basic,
        Normal,
        Hard,
        Chaos,
        Insanity
    }
}
