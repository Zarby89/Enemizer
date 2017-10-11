new_sprites_damage:
{
	LDA $7EF35B : STA $00 ; set armor value in $00
	LDA $0CD2, X : AND.b #$7F ;load damage the sprite is doing
	CPY $00 : BEQ .no_mail
	.have_mail
		LSR : DEY ;decrease A by half 
	CPY $00 : BNE .have_mail ;while $00 > 0 then loop back and decrease damage by half
		.no_mail
	TAY
	STA $00 : STA $0373
	RTL
}
