using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemizer
{
    public class files_names
    {
        public string name = "";
        public string file = "";
        public files_names(string name, string file)
        {
            this.name = name;
            this.file = file;
        }
        public override string ToString()
        {
            return name;
        }

    }
}
