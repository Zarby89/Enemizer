; ;================================================================================
; ; insert kholdstare & trinexx shell gfx file
; ;--------------------------------------------------------------------------------
; ; pc file address = 0x123000
; org $24B000
; GFX_Kholdstare_Shell:
; incbin shell.gfx
; warnpc $24C001      ; should have written 0x1000 bytes and apparently we need to go 1 past that or it'll yell at us

; org $24C000
; GFX_Trinexx_Shell:
; incbin rocks.gfx
; warnpc $24C801

; GFX_Trinexx_Shell2:
; incbin rocks2.gfx
; warnpc $24C8C1
; ;--------------------------------------------------------------------------------

    ; ; *$4C290-$4C2D4 LOCAL
    ; Dungeon_LoadSprites:

; *$4C114-$4C174 LONG
org $9C114
Dungeon_ResetSprites: ; Bank09.asm(822)

; *$4C44E-$4C498 LONG
org $9C44E
Sprite_ResetAll: ; Bank09.asm(1344)

;================================================================================
; fix skull woods gibdo key drop
;--------------------------------------------------------------------------------
;Gibdo key drop hardcoded in skullwoods to fix problems
;some bosses are dropping a key when there's a key drop avaiable in
;the previous room 

; org $09DD74     ; Gibdo draw code (JSL Sprite_DrawShadowLong)
; db #$00, #$00   ; Remove key drop in skull woods

; org $1EBB37     ; Gibdo draw code (JSL Sprite_DrawShadowLong)
; JSL gibdo_drop_key
;--------------------------------------------------------------------------------

;================================================================================
; Move All Bosses Sprites in Top Left Quadrant
;--------------------------------------------------------------------------------
; sprite values for rooms, with coordinates changed
;Trinexx
org $09E5BA ; 0x4E5BA ; [0xB]
db $00 ; "Sort Spr" in Hyrule Magic
db $05, $07, $CB ; trinexx body?            ; 15 07 CB
db $05, $07, $CC ; trinexx ice head?        ; 15 07 CC
db $05, $07, $CD ; trinexx fire head?       ; 15 07 CD
db $FF ; terminator

;Armos - Eastern
org $09E887 ; 0x4E887 ; [0x17]
db $00 ; "Sort Spr" in Hyrule Magic
db $05, $04, $53 ; armos                    ;15 14 53
db $05, $07, $53 ; armos                    ;15 17 53
db $05, $0A, $53 ; armos                    ;15 1A 53
db $08, $0A, $53 ; armos                    ;18 1A 53
db $08, $07, $53 ; armos                    ;18 17 53
db $08, $04, $53 ; armos                    ;18 14 53
db $08, $E7, $19 ; armos overlord           ;18 F7 19
db $FF ; terminator

;Kholdstare
org $09EA01 ; 0x4EA01 ; [0xB]
db $00 ; "Sort Spr" in Hyrule Magic
db $05, $07, $A3 ; kholdstare shell         ;05 17 A3
db $05, $07, $A4 ; fallling ice             ;05 17 A4
db $05, $07, $A2 ; kholdstare               ;05 17 A2
db $FF ; terminator

;Arrghus
org $09D997 ; 0x4D997 ; [0x2C]
db $00 ; "Sort Spr" in Hyrule Magic
db $07, $07, $8C ; arrghus                  ;17 07 8C
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $07, $07, $8D ; spawn                    ;17 07 8D
db $FF ; terminator

;Moldorm - ToH
org $09D9C3 ; 0x4D9C3 ; [0x5]
db $00 ; "Sort Spr" in Hyrule Magic
db $09, $09, $09 ; moldorm                  ;0E 12 09
db $FF ; terminator

;Mothula 
org $09DC31 ; 0x4DC31 ; [0x5] (really [0x8])
db $00 ; "Sort Spr" in Hyrule Magic
db $06, $08, $88 ; mothula                  ;16 18 88
; truncated moving floor overlord           ;16 E7 07
db $FF ; terminator

;Lanmolas - Desert
org $09DCCB ; 0x4DCCB ; [0xB]
db $00 ; "Sort Spr" in Hyrule Magic
db $07, $06, $54 ; lanmolas                 ;17 06 54
db $07, $09, $54 ; lanmolas                 ;17 09 54
db $09, $07, $54 ; lanmolas                 ;19 07 54
db $FF ; terminator

;Helmasaure
org $09E049 ; 0x4E049 ; [0x5]
db $00 ; "Sort Spr" in Hyrule Magic
db $06, $07, $92 ; helmasaur                ;16 17 92
db $FF ; terminator

;Vitreous
org $09E457 ; 0x4E457 ; [0x5]
db $00 ; "Sort Spr" in Hyrule Magic
db $05, $07, $BD ; vitreous                 ;15 07 BD
db $FF ; terminator

;Blind
org $09E654 ; 0x4E654 ; [0x5]
db $00 ; "Sort Spr" in Hyrule Magic
db $05, $09, $CE ; blind                    ;15 19 CE
db $FF ; terminator

; Armos - GT ; this shouldn't get used unless boss randomization is turned off
org $09DB23 ; 0x4DB23 ; [0x23] // need 0x38 to fit arrghus+spawn and fairies (use 0x4D87E-)
db $00 
db $05, $04, $53 ; armos                    ;15 14 53 
db $05, $07, $53 ; armos                    ;15 17 53 
db $05, $0A, $53 ; armos                    ;15 1A 53 
db $08, $0A, $53 ; armos                    ;18 1A 53 
db $08, $07, $53 ; armos                    ;18 17 53 
db $08, $04, $53 ; armos                    ;18 14 53 
db $08, $E7, $19 ; armos overlord           ;18 F7 19 
db $07, $07, $E3 ; fairy                    ;07 07 E3 
db $07, $08, $E3 ; fairy                    ;07 08 E3 
db $08, $07, $E3 ; fairy                    ;08 07 E3 
db $08, $08, $E3 ; fairy                    ;08 08 E3 
db $FF

; Lanmola - GT ; this shouldn't get used unless boss randomization is turned off
org $09E1BE ; 0x4E1BE ; [0x11] // need 0x32 to fit arrghus+spawn and bunny beam+medusa (use 0x4D8B6-)
db $00 
db $07, $06, $54 ; lanmolas                 ;17 06 54
db $07, $09, $54 ; lanmolas                 ;17 09 54
db $09, $07, $54 ; lanmolas                 ;19 07 54 
db $18, $17, $D1 ; bunny beam               ;18 17 D1
db $1C, $03, $C5 ; medusa                   ;1C 03 C5
db $FF

; Moldorm - GT ; this shouldn't get used unless boss randomization is turned off
org $09DF1E ; 0x4DF1E ; [0x5]
db $00 ; "Sort Spr" in Hyrule Magic
db $09, $09, $09 ; moldorm                  ;0E 12 09
db $FF ; terminator

;--------------------------------------------------------------------------------

;================================================================================
; On Room Transition -> Move Sprite depending on the room loaded
;--------------------------------------------------------------------------------
org $028979 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
JSL boss_move
org $028C16 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
JSL boss_move
org $029338 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
JSL boss_move
org $028256 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
JSL boss_move
;--------------------------------------------------------------------------------

;================================================================================
; water tiles removed in arrghus room
;--------------------------------------------------------------------------------
org $1FA15C 
db $FF, $FF, $FF, $FF, $F0, $FF, $61, $18, $FF, $FF

; Arrghus can stand on ground
org $0DB6BE
db $00
;--------------------------------------------------------------------------------

;================================================================================
; Draw kholdstare shell
;--------------------------------------------------------------------------------
org $0DD97F ; jump point
Kholdstare_Draw:

org $1E9518 ; sprite_kholdstare.asm (154) : JSL Kholdstare_Draw
JSL new_kholdstare_code ; Write new gfx in the vram
;--------------------------------------------------------------------------------

;================================================================================
; Draw trinexx shell
;--------------------------------------------------------------------------------
org $1DAD67 ; sprite_trinexx.asm (62) : LDA.b #$03 : STA $0DC0, X
JSL new_trinexx_code
;--------------------------------------------------------------------------------
