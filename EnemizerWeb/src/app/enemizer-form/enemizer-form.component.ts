import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { NgForm } from '@angular/forms';

import
{
    OptionFlags, AbsorbableTypesDictionary, RandomizeEnemiesType,
    RandomizeEnemyHPType, RandomizeBossesType, SwordTypes, ShieldTypes,
    AbsorbableTypes, HeartBeepSpeed, BeeLevel, BossType, RandomizerOptions
} from '../optionFlags';

import { KeysPipe } from '../keys.pipe';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http/src/response';

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

    constructor(private http: HttpClient) { }

    ngOnInit ()
    {
    }

    onSubmit () 
    {
        const randoOptions = JSON.stringify(this.randomizerOptions);
        const enemOptions = JSON.stringify(this.optionFlags);

        console.log(randoOptions);

        let params = new HttpParams()
            .set('randomizerOptions', JSON.stringify(this.randomizerOptions))
            .set('enemizerOptions', JSON.stringify(this.optionFlags));

        const req = this.http.get("http://localhost:49375/api/enemizer", { params: params })
            .subscribe(
            data =>
            {
                console.log(data);
            },
            (err: HttpErrorResponse) =>
            {
                if (err.error instanceof Error)
                {
                    console.log("Client side error");
                }
                else
                {
                    console.log("Server side error");
                }
                //console.log("error");
            }
            );
    }

    get diagnostic () { return JSON.stringify(this.optionFlags); }
}
