import { Component, OnInit } from '@angular/core';

import { OptionFlags, AbsorbableTypesDictionary, RandomizeEnemiesType, 
         RandomizeEnemyHPType, RandomizeBossesType, SwordTypes, ShieldTypes, 
         AbsorbableTypes, HeartBeepSpeed, BeeLevel, BossType, RandomizerOptions 
       } from '../optionFlags';

import { KeysPipe } from '../keys.pipe';

@Component({
    selector: 'app-enemizer-form',
    providers: [],
    templateUrl: './enemizer-form.component.html',
    styleUrls: ['./enemizer-form.component.css']
})
export class EnemizerFormComponent implements OnInit
{
    randomizerOptions: RandomizerOptions = new RandomizerOptions();
    LogicTypeList = [
        { key: "NoMajorGlitches", value: "No Glitches" },
        { key: "OverworldGlitches", value: "Overworld Glitches" },
        { key: "MajorGlitches", value: "Major Glitches" },
    ];
    ModeTypeList = [
        { key: "standard", value: "Standard" },
        { key: "open", value: "Open" },
        { key: "swordless", value: "Swordless" },
    ];
    GoalTypeList = [
        { key: "ganon", value: "Defeat Ganon" },
        { key: "dungeons", value: "All Dungeons" },
        { key: "pedestal", value: "Master Sword Pedestal" },
        { key: "triforce-hunt", value: "Triforce Pieces" },
    ];
    DifficultyTypeList = [
        { key: "easy", value: "Easy" },
        { key: "normal", value: "Normal" },
        { key: "hard", value: "Hard" },
        { key: "expert", value: "Expert" },
        { key: "insane", value: "Insane" },
    ];
    VariationTypeList = [
        { key: "none", value: "None" },
        { key: "timed-race", value: "Timed Race" },
        { key: "timed-ohko", value: "Timed OHKO" },
        { key: "ohko", value: "OHKO" },
        { key: "triforce-hunt", value: "Triforce Piece Hunt" },
        { key: "key-sanity", value: "Key-sanity" },
    ];
    HeartBeepSpeedTypeList = [
        { key: "off", value: "Off" },
        { key: "normal", value: "Normal Speed" },
        { key: "half", value: "Half Speed" },
        { key: "quarter", value: "Quarter Speed" },
    ];
    ShuffleTypeList = [
        { key: "off", value: "Off" },
        { key: "simple", value: "Simple" },
        { key: "restricted", value: "Restricted" },
        { key: "full", value: "Full" },
        { key: "madness", value: "Madness" },
        { key: "insanity", value: "Insanity" },
    ];


    optionFlags: OptionFlags = new OptionFlags();

    RandomizeEnemiesTypeList = RandomizeEnemiesType;
    RandomizeEnemyHPTypeList = RandomizeEnemyHPType;
    AbsorbableTypesList = AbsorbableTypes;
    RandomizeBossesTypeList = RandomizeBossesType;
    HeartBeepSpeedList = HeartBeepSpeed;
    BeeLevelList = [
        { key: 0, value: "Bees??" }, 
        { key: 1, value: "Bees!" }, 
        { key: 2, value: "Beeeeees!?" }, 
        { key: 3, value: "Beeeeeeeeeeeeeeeeeeeees" }
    ];
    BossTypeList = BossType;

    constructor() { }

    ngOnInit ()
    {
    }

    onSubmit() 
    { 

    }

    get diagnostic() { return JSON.stringify(this.optionFlags); }
}
