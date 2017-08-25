using EnemizerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemizer
{
    public class EnemizerConfig
    {
        public bool CheckForUpdates { get; set; }
        public OptionFlags OptionFlags { get; set; } = new OptionFlags();
    }
}
