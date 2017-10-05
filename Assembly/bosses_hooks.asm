; insert kholdstare shell gfx file
; pc file address = 0x123000
org $24B000
GFX_Kholdstare_Shell:
incbin shell.gfx
warnpc $24C001  ; should have written 0x1000 bytes and apparently we need to go 1 past that or it'll yell at us
; TODO: load trinexx gfx
org $24C000
GFX_Trinexx_Shell:
incbin rocks.gfx
warnpc $24C801

;Move rooms header at position $248090 (0x120090)
; put this back in the c# code
;incsrc headers.asm

;Gibdo key drop hardcoded in skullwoods to fix problems
;some bosses are dropping a key when there's a key drop avaiable in
;the previous room 

org $09DD74 ;Gibdo draw code (JSL Sprite_DrawShadowLong)
db #$00, #$00 ; Remove key drop in skull woods
org $1EBB37 ;Gibdo draw code (JSL Sprite_DrawShadowLong)
JSL gibdo_drop_key

;Move All Bosses Sprites in Top Left Quadrant
;Trinexx
org $09E5BA
db $00, $05, $07, $CB, $05, $07, $CC, $05, $07, $CD

;Armos
org $09E887
db $00, $05, $04, $53, $05, $07, $53, $05, $0A, $53
db $08, $0A, $53, $08, $07, $53, $08, $04, $53, $08, $E7, $19

;Kholdstare
org $09EA01
db $00, $05, $07, $A3, $05, $07, $A4, $05, $07, $A2

;Arrghus
org $09D997
db $00, $07, $07, $8C, $07, $07, $8D, $07, $07, $8D
db $07, $07, $8D, $07, $07, $8D, $07, $07, $8D, $07, $07, $8D
db $07, $07, $8D, $07, $07, $8D, $07, $07, $8D, $07, $07, $8D
db $07, $07, $8D, $07, $07, $8D, $07, $07, $8D

;Moldorm
org $09D9C3
db $00, $09, $09, $09

;Mothula (16 E7 07 (code for moving floor random) )
org $09DC31
db $00, $06, $08, $88, $FF

;Lanmolas
org $09DCCB
db $00, $07, $06, $54, $07, $09, $54, $09, $07, $54

;Helmasaure
org $09E049
db $00, $06, $07, $92

;Vitreous
org $09E457
db $00, $05, $07, $BD

;Blind
org $09E654
db $00, $05, $09, $CE


;On Room Transition -> Move Sprite depending on the room loaded
org $028979 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
JSL boss_move
org $028C16 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
JSL boss_move
org $029338 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
JSL boss_move
org $028256 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
JSL boss_move

; water tiles removed in arrghus room
org $1FA15C 
db $FF, $FF, $FF, $FF, $F0, $FF, $61, $18, $FF, $FF

; Arrghus can stand on ground
org $0DB6BE
db $00

;-----------------------------------------------
org $0DD97F
Kholdstare_Draw:

org $1E9518 ; sprite_kholdstare.asm (154) : JSL Kholdstare_Draw
JSL new_kholdstare_code;Write new gfx in the vram
;-----------------------------------------------

org $1DAD67 ; sprite_trinexx.asm (62) : LDA.b #$03 : STA $0DC0, X
JSL new_trinexx_code
