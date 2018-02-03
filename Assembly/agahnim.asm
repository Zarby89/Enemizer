newAgahBall:
{
 
;IF DAMAGE
    CLC ;Clear carry
    STZ $0D90, X ;not sent by the player anymore prevent agahnim from taking damage from the ball
    LDA #$60 : STA $0DF0
    LDA !AGAHNIM_FUN_BALLS : BEQ .damage
    JSL GetRandomInt : BMI .no_damage
    .damage
        PHX ;keep the current sprite id? not sure why
        LDA.b #$A0 : STA $00;?
        INC $0D90, X ;sent by the player anymore
        LDA.b #$10 ;?
        LDX.b #$00 ;Damage class used by the next jump
        JSL $06EDCB ;Jump to damage part stuff
        PLX ;restore the current sprite id? not sure why again
        STZ $0DD0, X ;Set sprite state of energy ball to "Dead"
        LDA $0D50, X : STA $0F40 ;Load x velocity of energy ball and store it in agahnim recoiling x
        LDA $0D40, X : STA $0F30 ;Load y velocity of energy ball and store it in agahnim recoiling y
        SEC;Set carry if we did damage
    .no_damage
    RTL
}