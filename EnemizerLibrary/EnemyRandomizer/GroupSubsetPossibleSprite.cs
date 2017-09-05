using System.Collections.Generic;
using System.Linq;

namespace EnemizerLibrary
{
    public class GroupSubsetPossibleSprite
    {
        public int GroupSubsetId { get; set; }
        public int SpriteId { get; set; }
        public string SpriteName
        {
            get
            {
                return SpriteConstants.GetSpriteName(SpriteId);
            }
        }

        public List<int> RequiredSecondarySubsetId { get; set; } = new List<int>();

        public GroupSubsetPossibleSprite(int groupSubsetId, int spriteId, int[] requiredSecondarySubsetId = null)
        {
            this.GroupSubsetId = groupSubsetId;
            this.SpriteId = spriteId;
            if (requiredSecondarySubsetId != null)
            {
                this.RequiredSecondarySubsetId = requiredSecondarySubsetId.ToList();
            }
        }
    }
}
