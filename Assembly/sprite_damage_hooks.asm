;org $06F3F6 ;change sprites damage to go up to 128 instead of using 8 damage classes
;original code : LDA $0CD2, X : AND.b #$0F : STA $00 : ASL A : ADC $00 : ADD $7EF35B : TAY
JSL new_sprites_damage
;NOP #$12 ; Remove the 12 bytes remainings
