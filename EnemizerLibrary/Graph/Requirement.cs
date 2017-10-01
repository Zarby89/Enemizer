using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Requirement
    {
        public List<Item> Requirements { get; set; } = new List<Item>();

        public Requirement(params Item[] items)
        {
            this.Requirements.AddRange(items);
        }

        public bool RequirementsMet(List<Item> items)
        {
            if (Requirements.Count == 0)
            {
                return true;
            }
            if(Requirements.Intersect(items).Count() == Requirements.Count)
            {
                return true;
            }
            return false;
        }
    }
}
