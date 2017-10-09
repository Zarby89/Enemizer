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

org $06EC08 ; Bank06.asm (4593)
JSL resetSprite_Mimic
NOP
