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

    compareFn: ((f1: any, f2: any) => boolean) | null = this.compareByKey;

    compareByKey(f1: any, f2: any)
    {
        return f1 && f2 && f1.toString() === f2.toString();
    }

    get diagnostic() { return JSON.stringify(this.optionFlags); }
}
