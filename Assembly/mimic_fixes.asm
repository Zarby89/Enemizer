    ; ;new code at the bottom of main.asm
    ; SpritePrep_EyegoreNew:
    ; {
    ;     LDA $0E20, X : CMP.b #$B8 : BEQ .mimic ;If sprite id == debugger sprite
    ;         JSL $1EC71A ;set eyegore to be only eyegore
    ;     RTL
    ;     .mimic
    ;         JSL $1EC70D;set eyegore to be mimic
    ;     RTL
    ; }


    ; SpritePrep_EyegoreNew:
    ; {
    ;     LDA $0E20, X : CMP.b #$B8 : BEQ .mimic ;If sprite id == debugger sprite
    ;         JSL $1EC71A ;set eyegore to be only eyegore
    ;     RTL
    ;     .mimic
    ;         LDA #$83 : STA $0E20, X : JSL $0DB818 ;Sprite_LoadProperties of green eyegore
    ;         LDA #$B8 : STA $0E20, X ; set the sprite back to mimic
    ;         JSL $1EC70D;set eyegore to be mimic
    ;     RTL
    ; }

SpritePrep_EyegoreNew:
{
    LDA $0E20, X : CMP.b #$B8 : BEQ .mimic ;If sprite id == debugger sprite
        JSL $1EC71A ; 0xF471A set eyegore to be only eyegore (.not_goriya?)
    RTL
    .mimic
        LDA #$83 : STA $0E20, X : JSL $0DB818 ; 0x6B818 Sprite_LoadProperties of green eyegore
        LDA #$B8 : STA $0E20, X ; set the sprite back to mimic
        LDA $0CAA, X : AND #$FB : ORA #$80 : STA $0CAA, X ; STZ $0CAA, X
        ;INC $0DA0, X
        JSL $1EC70D ;0xF470D set eyegore to be mimic (.is_goriya?)
    RTL
}

resetSprite_Mimic:
{
    LDA $0E20, X
    CMP.b #$B8 : BNE .notMimic
    LDA #$83 : STA $0E20, X ; overwrite the sprite id with green eyegore id

.notMimic
    ; restore code
    ;LDA $0E20, X
    
    CMP.b #$7A

    RTL
}