LoadOverworldSprites:
    ; restore code
    STA $01 ; 85 01
    LDY.w #$0000 ; A0 00 00

    ; set bank
    LDA #$09 : STA $02 ; default is bank 9
RTL