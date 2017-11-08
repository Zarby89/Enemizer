using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Node
    {
        public string LogicalId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"({LogicalId}-{Name})";
        }

        public virtual void Reset()
        {
            
        }
    }
}
