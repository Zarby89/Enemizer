Palette_ArmorAndGloves:
{
    ;DEDF9
    LDA !RANDOM_SPRITE_FLAG : BNE .continue
        LDA.b #$10 : STA $BC ; Load Original Sprite Location
        REP #$21
        LDA $7EF35B
        JSL $1BEDFF;Read Original Palette Code
    RTL
    .part_two
    SEP #$30
    LDA !RANDOM_SPRITE_FLAG : BNE .continue
        REP #$30
        LDA $7EF354
        JSL $1BEE21;Read Original Palette Code
    RTL

    .continue

    PHX : PHY : PHA
    ; Load armor palette
        PHB : PHK : PLB
    REP #$20
    
    ; Check what Link's armor value is.
    LDA $7EF35B : AND.w #$00FF : TAX
    
    ; (DEC06, X)
    
    LDA $1BEC06, X : AND.w #$00FF : ASL A : ADC.w #$F000 : STA $00
    ;replace D308 by 7000 and search
    REP #$10
    
    LDA.w #$01E2 ; Target SP-7 (sprite palette 6)
    LDX.w #$000E ; Palette has 15 colors
    
    TXY : TAX
    
    ;LDA  $7EC178 : AND #$00FF : STA $02
    LDA.b $BC : AND #$00FF : STA $02

.loop

    LDA [$00] : STA $7EC300, X : STA $7EC500, X
    
    INC $00 : INC $00
    
    INX #2
    
    DEY : BPL .loop

    SEP #$30
    
    
    PLB
    INC $15
    PLA : PLY : PLX
    RTL
}

change_sprite:
{
    ;JSL $09C114         ; Restore the dungeon_resetsprites

    LDA !RANDOM_SPRITE_FLAG : BEQ .continue
    JSL GetRandomInt : AND #$1F : !ADD #$60 : STA $BC
    STA $7EC178
    JSL Palette_ArmorAndGloves
    STZ $0710

    .continue
    LDA $0E20, X : CMP.b #$61;Restored Code

    RTL
}

