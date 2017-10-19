using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class BossPool
    {
        protected List<Boss> pool = new List<Boss>();
        protected readonly Random rand;

        public BossPool(Random rand)
        {
            this.rand = rand;
        }

        public virtual void FillPool()
        {
            FillBasePool();
            FillGTPool();
        }

        protected virtual void FillBasePool()
        {
            pool.Add(new ArmosBoss());
            pool.Add(new LanmolaBoss());
            pool.Add(new MoldormBoss());
            pool.Add(new HelmasaurBoss());
            pool.Add(new ArrghusBoss());
            pool.Add(new MothulaBoss());
            pool.Add(new BlindBoss());
            pool.Add(new KholdstareBoss());
            pool.Add(new VitreousBoss());
            pool.Add(new TrinexxBoss());
        }

        protected virtual void FillGTPool()
        {
            pool.Add(new ArmosBoss()); // GT1
            pool.Add(new LanmolaBoss()); // GT2
            pool.Add(new MoldormBoss()); // GT3
        }

        public virtual Boss GetRandomBoss(List<BossType> excludedBossTypes, Graph graph)
        {
            var possibleBosses = this.pool.Where(x => excludedBossTypes.Contains(x.BossType) == false).ToList();
            if (possibleBosses.Any() == false)
            {
                //throw new Exception($"Couldn't find any possible bosses not disallowed");
                return null;
            }

            possibleBosses = possibleBosses.Where(x => x.CanBeUsed(graph)).ToList();
            if (possibleBosses.Any() == false)
            {
                //throw new Exception($"Couldn't find any possible bosses meeting item checks");
                return null;
            }

            Boss boss = possibleBosses.ElementAt(rand.Next(possibleBosses.Count()));

            return boss;
        }

        public virtual Boss GetNextBoss()
        {
            return this.pool.FirstOrDefault();
        }

        public virtual void RemoveBoss(Boss boss)
        {
            pool.Remove(boss);
        }

        public virtual void ReaddBoss(Boss boss)
        {
            pool.Add(boss);
        }
    }
}
