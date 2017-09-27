using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class EmptyBoss : Boss
    {
        public EmptyBoss() : base(BossType.NoBoss) { }
    }
}
