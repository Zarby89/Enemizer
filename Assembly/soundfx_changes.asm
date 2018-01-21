NewLoadSoundBank_Intro:
    ; restore
    SEI
    JSL Sound_LoadSongBank ; change to be JSL
RTL

NewLoadSoundBank:
    ; restore
    SEI
    JSL Sound_LoadSongBank ; change to be JSL
RTL

LoadNewSoundFx:
{
    LDA !SOUNDFX_LOADED : BEQ +
        RTL
    +
    LDA #$01 : STA !SOUNDFX_LOADED

    SEI
    ; Shut down NMI until music loads
    STZ $4200
    
    ; Stop all HDMA
    STZ $420C
    
    STZ $0136
    
    LDA.b #$FF : STA $2140 ; tell N-SPC to load data

    ; new code
    ; load up our new instrument and sample incbin
    LDA.b #$00 : STA $00
    LDA.b #$80 : STA $01
    LDA.b #$26 : STA $02
    JSL Sound_LoadSongBank

    ; Re-enable NMI and joypad
    LDA.b #$81 : STA $4200

    CLI

    ;JSL $00893D;Restore the previous code
    RTL
}
