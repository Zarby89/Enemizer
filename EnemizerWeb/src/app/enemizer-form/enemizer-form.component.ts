import { Component, OnInit } from '@angular/core';

import { OptionFlags, AbsorbableTypesDictionary, RandomizeEnemiesType, 
         RandomizeEnemyHPType, RandomizeBossesType, SwordTypes, ShieldTypes, 
         AbsorbableTypes, HeartBeepSpeed, BeeLevel, BossType 
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
    optionFlags: OptionFlags = new OptionFlags();

    RandomizeEnemiesTypeList = RandomizeEnemiesType;
    RandomizeEnemyHPTypeList = RandomizeEnemyHPType;
    AbsorbableTypesList = AbsorbableTypes;
    RandomizeBossesTypeList = RandomizeBossesType;
    HeartBeepSpeedList = HeartBeepSpeed;
    BeeLevelList = BeeLevel;
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
