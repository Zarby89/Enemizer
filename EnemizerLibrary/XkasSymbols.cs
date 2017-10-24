using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class XkasSymbols
    {
        public static XkasSymbols Instance { get { return Nested.instance; } }
        public Dictionary<string, int> Symbols { get; private set; }
        private XkasSymbols(string filename)
        {
            Symbols = new Dictionary<string, int>();

            var lines = File.ReadAllLines(filename);
            foreach(var l in lines)
            {
                var s = l.Split(new string[] { " = $" }, StringSplitOptions.None);
                if(s.Length != 2)
                {
                    continue;
                }
                var symbol = s[0];
                var snesAddress = int.Parse(s[1], System.Globalization.NumberStyles.HexNumber);
                var pcAddress = Utilities.SnesToPCAddress(snesAddress);
                Symbols[symbol] = pcAddress;
            }
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly XkasSymbols instance = new XkasSymbols("exported_symbols.txt");
        }
    }
}
