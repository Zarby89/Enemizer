using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class EnemizerBasePath
    {
        public static EnemizerBasePath Instance { get { return Nested.instance; } }

        public string BasePath { get; set; } = "";

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly EnemizerBasePath instance = new EnemizerBasePath();
        }

    }
}
