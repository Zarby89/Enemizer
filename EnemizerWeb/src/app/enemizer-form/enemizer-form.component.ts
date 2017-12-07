import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { NgForm } from '@angular/forms';

import
{
    OptionFlags, AbsorbableTypesDictionary, RandomizeEnemiesType,
    RandomizeEnemyHPType, RandomizeBossesType, SwordTypes, ShieldTypes,
    AbsorbableTypes, HeartBeepSpeed, BeeLevel, BossType, RandomizerOptions,
    Patch
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
    romFilename: string;
    romError: string;
    romBytes: Uint8Array = null;
    
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

    onRomChange (event?: HTMLInputEvent)
    {
        console.log('onRomChange');
        let files = event.target.files;
        if (files.length === 0)
        {
            return;
        }

        this.romError = '';
        this.romFilename = files[0].name;

        let reader = new FileReader();
        reader.onload = () =>
        {
            let arrayBuffer = reader.result;
            let array = new Uint8Array(arrayBuffer);
            this.romBytes = array;
            // if (!this.isValidRom(this.romBytes))
            // {
            //     this.romError = 'Invalid rom file';
            //     return;
            // }
        };

        reader.readAsArrayBuffer(files[0]);

        console.log(files);
    }

    onSubmit () 
    {
        const randoOptions = JSON.stringify(this.randomizerOptions);
        const enemOptions = JSON.stringify(this.optionFlags);

        console.log(randoOptions);

        let params = new HttpParams()
            .set('seedNumber', null)
            .set('randomizerOptions', JSON.stringify(this.randomizerOptions))
            .set('enemizerOptions', JSON.stringify(this.optionFlags));

        const req = this.http.get<Patch>("http://localhost:49375/api/enemizer", { params: params })
            .subscribe(data =>
            {
                console.log(data);
                this.romBytes = this.resizeUint8(this.romBytes, 4 * 1024 * 1024);

                if (this.romBytes)
                {
                    for (let randoPatch of data.randoPatch.Patches)
                    {
                        for (let i = 0; i < randoPatch.patchData.length; i++)
                        {
                            this.romBytes[randoPatch.address + i] = randoPatch.patchData[i];
                        }
                    }

                    for (let enemizerPatch of data.enemPatch)
                    {
                        for (let i = 0; i < enemizerPatch.patchData.length; i++)
                        {
                            this.romBytes[enemizerPatch.address + i] = enemizerPatch.patchData[i];
                        }
                    }

                    this.downloadBlob(this.romBytes, this.romFilename, 'application/octet-stream');
                }
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

    downloadBlob = (data, fileName, mimeType) =>
    {
        let blob, url;
        blob = new Blob([data], {
            type: mimeType
        });
        url = window.URL.createObjectURL(blob);
        this.downloadURL(url, fileName);
        setTimeout(function ()
        {
            return window.URL.revokeObjectURL(url);
        }, 1000);
    }

    downloadURL = (data, fileName) =>
    {
        let a;
        a = document.createElement('a');
        a.href = data;
        a.download = fileName;
        document.body.appendChild(a);
        a.style = 'display: none';
        a.click();
        a.remove();
    }

    resizeUint8 = (baseArrayBuffer, newByteSize) =>
    {
        let resizedArrayBuffer = new ArrayBuffer(newByteSize),
            len = baseArrayBuffer.byteLength,
            resizeLen = (len > newByteSize) ? newByteSize : len;
    
            (new Uint8Array(resizedArrayBuffer, 0, resizeLen)).set(new Uint8Array(baseArrayBuffer, 0, resizeLen));
    
        return new Uint8Array(resizedArrayBuffer);
    }
    get diagnostic () { return JSON.stringify(this.optionFlags); }
}

interface HTMLInputEvent extends Event
{
    target: HTMLInputElement & EventTarget;
}
