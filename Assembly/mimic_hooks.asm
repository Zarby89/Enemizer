;Hooks
; sprite_prep.asm (2466) -> SpritePrep_EyegoreTrampoline:
org $0691B6 ; 0311B6   1E:C6FA? ; SpriteActive3_Transfer?
    JSL SpritePrep_EyegoreNew ;set eyegore to be only eyegore


; sprite_prep.asm (203) -> dw SpritePrep_DoNothing                ; 0xB8 - Dialogue Testing Sprite
org $0687CB ; 0307CB replace debugged sprite create by eyegore
    dw #$91B6 ; SpritePrep_Eyegore jump table


; Bank1E.asm (140) -> dw Sprite_DialogueTester   ; 0xB8 - debug artifact, dialogue tester
org $1E8BB1
    dw #$C795 ;Sprite_Eyegore jump table

; table starts at 6B8F1
org $0DB9A9
db $00 ; sprite B8 needs a damage type

org $06EC08 ; Bank06.asm (4593) - damage calcs
{
JSL resetSprite_Mimic
NOP
}

org $06EDA6 ; Bank06.asm (4876) - .notItemSprite
{
; REP #$20 : ASL #4 : ORA $0CF2 : PHX : REP #$10 : TAX  ;C2 20 : 0A 0A 0A 0A : 0D F2 0C : DA : C2 10 : AA
; SEP #$20                                              ;E2 20
; LDA $7F6000, X : STA $02                              ;BF 00 60 7F : 85 02
; SEP #$10

JSL notItemSprite_Mimic                             ; C2 20 : 0A 0A
;NOP : NOP : NOP : NOP : NOP : NOP : NOP : NOP : NOP ; 0A 0A : 0D F2 0C : DA : C2 10 : AA
;NOP : NOP                                           ; E2 20
;NOP : NOP : NOP : NOP : NOP : NOP                   ; BF 00 60 7F : 85 02
;NOP                                                 ; 
}
