using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class LogicalBoss : Node
    {
        public SpecialItem Boss { get; set; }

        public LogicalBoss(string logicalId, string name, string requirements)
        {
            this.LogicalId = logicalId;
            this.Name = name;
            this.Boss = (SpecialItem)Data.GameItems.Items[logicalId];
        }
    }
}
