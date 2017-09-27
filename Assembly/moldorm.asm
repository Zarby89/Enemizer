Moldorm_UpdateOamPosition:
{
	PHX

	LDA !MOLDORM_EYES_FLAG : TAX
	.more_eyes
	LDA $90 : CLC : ADC.w #$0004 : STA $90
	LDA $92 : CLC : ADC.w #$0001 : STA $92
	DEX : BPL .more_eyes ; X >= 0

	PLX
	
	RTL
}
