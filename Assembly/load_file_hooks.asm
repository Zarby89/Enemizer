org $028118 ; Bank02.asm(342) : LDA.b #$00 : STA $7EC011
Module_LoadFile_indoors:
;aka Module_LoadGame.indoors
    ; LDA.b #$00 : STA $7EC011
    JSL.l LoadFile : NOP : NOP
