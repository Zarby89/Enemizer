﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EnemizerLibrary
{
    public class OverworldAreaCollection
    {
        public List<OverworldArea> OverworldAreas { get; set; } = new List<OverworldArea>();
        RomData romData;
        Random rand;
        SpriteRequirementCollection spriteRequirementCollection;

        public OverworldAreaCollection(RomData romData, Random rand, SpriteRequirementCollection spriteRequirementCollection)
        {
            this.romData = romData;
            this.rand = rand;
            this.spriteRequirementCollection = spriteRequirementCollection;

            LoadAreas();
        }

        void LoadAreas()
        {
            for (int i = 0; i < 0x120; i++)
            {
                var owArea = new OverworldArea(romData, i);
                OverworldAreas.Add(owArea);
            }
        }

        public void UpdateRom()
        {
            foreach(var a in OverworldAreas)
            {
                a.UpdateRom();
            }
        }

        public void RandomizeAreaSpriteGroups(SpriteGroupCollection spriteGroups)
        {
            // TODO: this needs to be updated???

            foreach (var a in OverworldAreas.Where(x => OverworldAreaConstants.DoNotRandomizeAreas.Contains(x.AreaId) == false))
            {
                List<SpriteRequirement> doNotUpdateSprites = spriteRequirementCollection
                                                            .DoNotRandomizeSprites
                                                            .Where(x => //x.CanSpawnInRoom(a)
                                                                        //&& 
                                                                        a.Sprites.Select(y => y.SpriteId).ToList().Contains(x.SpriteId)
                                                                )
                                                            .ToList();

                var possibleSpriteGroups = spriteGroups.GetPossibleOverworldSpriteGroups(doNotUpdateSprites).ToList();

                //Debug.Assert(possibleSpriteGroups.Count > 0);

                a.GraphicsBlockId = (byte)possibleSpriteGroups[rand.Next(possibleSpriteGroups.Count)].GroupId;
            }

            //// force any rooms we need to
            //foreach (var sg in spriteGroups.SpriteGroups.Where(x => x.ForceRoomsToGroup != null && x.ForceRoomsToGroup.Count > 0))
            //{
            //    foreach (var forcedR in OverworldAreas.Where(x => sg.ForceRoomsToGroup.Contains(x.AreaId)))
            //    {
            //        forcedR.GraphicsBlockId = (byte)sg.GroupId;
            //    }
            //}
        }

    }
}