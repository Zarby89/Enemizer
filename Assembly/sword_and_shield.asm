swordgfx:
;incbin swords.gfx
skip #$1000
shieldgfx:
;incbin shields.gfx
skip #$C00

CopySword:
{
    PHB : PHK : PLB
    PHP ;push processor byte
    REP #$30 ; set everything to 16-bit
    LDY #$0000
    LDA $7EF359 : AND.w #$00FF : ASL : TAX ;Load Sword Value
    LDA.w .sword_positon_gfx, X : TAX
    .loop_copy
    LDA swordgfx, X : PHX : TYX : STA $7E9000, X : PLX
    LDA swordgfx+#$200, X : PHX : TYX : STA $7E9180, X : PLX
    INX : INX : INY : INY
    CPY #$0180 : BCC .loop_copy
    PLP ;pull processor byte
    PLB
    RTL
    .sword_positon_gfx
    dw #$0000, #$0000, #$0400, #$0800, #$0C00 ; swords position in gfx file
}

CopyShield:
{
    PHB : PHK : PLB
    PHP ;push processor byte
    REP #$30 ; set everything to 16-bit
    LDY #$0300
    LDA $7EF35A : AND.w #$00FF : ASL : TAX ;Load Shield value
    LDA.w .shield_positon_gfx, X : TAX
    .loop_copy
    LDA shieldgfx, X : PHX : TYX : STA $7E9000, X : PLX
    LDA shieldgfx+#$200, X : PHX : TYX : STA $7E90C0, X : PLX
    INX : INX : INY : INY
    CPY #$03C0 : BCC .loop_copy
    PLP ;pull processor byte
    PLB
    RTL
    .shield_positon_gfx
    dw #$0000,#$0000, #$0400, #$0800
}
