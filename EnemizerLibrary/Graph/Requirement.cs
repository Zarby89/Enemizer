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

        public static List<Requirement> MakeRequirementListFromString(string req)
        {
            if(String.IsNullOrEmpty(req))
            {
                return null;
            }

            List<Requirement> ret = new List<Requirement>();

            foreach (var r in req.Split(';'))
            {
                if (r.Length > 0)
                {
                    List<Item> items = new List<Item>();
                    foreach (var reqItem in r.Split(','))
                    {
                        Item item = Data.GameItems.Items.Values.Where(x => x.LogicalId == reqItem).FirstOrDefault();
                        if (item == null)
                        {
                            throw new Exception($"MakeRequirementListFromString - could not find item {reqItem}");
                        }
                        items.Add(item);
                    }
                    ret.Add(new Requirement(items.ToArray()));
                }
            }

            return ret;
        }
    }
}
