org $008A01
LDA $BC

org $1BEDF9
JSL Palette_ArmorAndGloves ;4bytes
RTL ;1byte 
NOP #$01


org $1BEE1B
JSL Palette_ArmorAndGloves_part_two
RTL

org $06F40C
JSL change_sprite : NOP #$01 ;LDA $0E20, X : CMP.b #$61

; TODO: move this to main hooks
org $0CCD9D
JSL OnInitFileSelect

