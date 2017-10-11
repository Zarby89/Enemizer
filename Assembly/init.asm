OnInitFileSelect:
{
    LDA.b #$10 : STA $BC
    LDA !RANDOM_SPRITE_FLAG : BEQ .continue
    JSL $0DBA71 : AND #$1F : !ADD #$60 : STA $BC
.continue

    JSL LoadNewSoundFx

    JSL $00893D;Restore the previous code
    RTL
}
