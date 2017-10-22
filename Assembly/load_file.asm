LoadFile:
    JSL LoadNewSoundFx

    LDA.b #$00 : STA $7EC011 ; restore what we overwrote
RTL