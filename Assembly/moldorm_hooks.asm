; adjust oam position after drawing eyes
;ED88E
org $1DD88E
{
    ; original: GiantMoldorm_Draw+5lines (sprite_giant_moldorm.asm)
    ; lda $90 : add.w #$0008 : sta $90
    ; INC $92 : INC $92

	JSL Moldorm_UpdateOamPosition
    NOP #08
}

; set number of eyes
org $1DDBB2 ;$0EDBB2
{
    ; LDX.b #$01
	; number of eyes (-1)
    ;0EDBB2 0EDBB3
    LDX.b #$07
}
