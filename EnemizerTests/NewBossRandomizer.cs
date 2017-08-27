using System;
using System.Linq;
using Xunit;
using EnemizerLibrary;

namespace EnemizerTests
{
    public class NewBossRandomizerTests
    {
        [Fact]
        public void default_boss_pool_contains_two_armos()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(2, br.PossibleBossesPool.Where(x => x.BossType == BossType.Armos).Count());
        }

        [Fact]
        public void default_boss_pool_contains_two_lanmola()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(2, br.PossibleBossesPool.Where(x => x.BossType == BossType.Lanmola).Count());
        }

        [Fact]
        public void default_boss_pool_contains_two_moldorm()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(2, br.PossibleBossesPool.Where(x => x.BossType == BossType.Moldorm).Count());
        }

        [Fact]
        public void default_boss_pool_contains_one_helmasaur()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(1, br.PossibleBossesPool.Where(x => x.BossType == BossType.Helmasaur).Count());
        }

        [Fact]
        public void default_boss_pool_contains_one_arrghus()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(1, br.PossibleBossesPool.Where(x => x.BossType == BossType.Arrghus).Count());
        }

        [Fact]
        public void default_boss_pool_contains_one_mothula()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(1, br.PossibleBossesPool.Where(x => x.BossType == BossType.Mothula).Count());
        }

        [Fact]
        public void default_boss_pool_contains_one_blind()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(1, br.PossibleBossesPool.Where(x => x.BossType == BossType.Blind).Count());
        }

        [Fact]
        public void default_boss_pool_contains_one_kholdstare()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(1, br.PossibleBossesPool.Where(x => x.BossType == BossType.Kholdstare).Count());
        }

        [Fact]
        public void default_boss_pool_contains_one_vitreous()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(1, br.PossibleBossesPool.Where(x => x.BossType == BossType.Vitreous).Count());
        }

        [Fact]
        public void default_boss_pool_contains_one_trinexx()
        {
            BossRandomizer br = new BossRandomizer();
            Assert.Equal(1, br.PossibleBossesPool.Where(x => x.BossType == BossType.Trixnexx).Count());
        }
    }
}
