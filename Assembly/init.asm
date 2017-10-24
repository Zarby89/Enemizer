OnInitFileSelect:
{
    LDA.b #$10 : STA $BC
    LDA !RANDOM_SPRITE_FLAG : BEQ .continue
    JSL GetRandomInt : AND #$1F : !ADD #$60 : STA $BC
.continue
    LDA #$00 : STA !SOUNDFX_LOADED

    JSL $00893D;Restore the previous code
    RTL
}
