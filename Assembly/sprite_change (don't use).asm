lorom

!SUB = "SEC : SBC"
!ADD = "CLC : ADC"

org $00FFD7 ; Set rom on 24mb
db #$0C

org $7FFFFF ; write at the last position to expand on 3mb
db #$00


org $408000
incbin Oldman.spr
org $418000
incbin Rumia.spr
org $428000
incbin Samus.spr
org $438000
incbin SuperBunny.spr
org $448000
incbin Toad.spr
org $458000
incbin sodacan.spr
org $468000
incbin tmnt.spr
org $478000
incbin orb.spr
org $488000
incbin wizzrobe.spr
org $498000
incbin roykoopa.spr
org $4A8000
incbin purplechest.spr
org $4B8000
incbin pony.spr
org $4C8000
incbin garfield.spr
org $4D8000
incbin girl.spr
org $4E8000
incbin zelda.spr
org $4F8000
incbin darklink.spr
org $508000
incbin darkzelda.spr
org $518000
incbin cucco.spr
org $528000
incbin boo.spr
org $538000
incbin zora.spr
org $548000
incbin mclink.spr
org $558000
incbin maiden.spr
org $568000
incbin kirby.spr
org $578000
incbin maplequeen.spr
org $588000
incbin marisa.spr
org $598000
incbin mog.spr
org $5A8000
incbin mikejones.spr
org $5B8000
incbin frog.spr
org $5C8000
incbin catboo.spr
org $5D8000
incbin backwardslink.spr
org $5E8000
incbin decidueye.spr
org $5F8000
incbin boxes.spr

org $008A01
LDA #$40

;org $1BEDF9
;JSL Palette_ArmorAndGloves
;RTL

;org $1BEE1B
;JSL Palette_ArmorAndGloves
;RTL




;On Room Transition -> Move Sprite depending on the room loaded
org $028979 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
;JSL change_sprite
org $028C16 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
;JSL change_sprite
org $029338 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
;JSL change_sprite
org $028256 ;  JSL Dungeon_ResetSprites ; REPLACE THAT (Sprite initialization) original jsl : $09C114
;JSL change_sprite
;org $04F4C9
;db #5 ;starting with 5 bombs

org $089543 ;Bomb timer
db #$25 ;A0 default


org $08994B;bomb speed
db #$D0, #$30, #$00, #$00, #$00, #$00, #$D0, #$30

org $09815A 
NOP #$03 ; remove Zposition stz

org $1CF54E
db #$03

org $07A140
db #$03

org $098127 ;bomb create? 098127

JSL bomb_create ;23

CMP #$FF : BNE .has_bomb
NOP #$01
db #$80, #$79 ; BRA to the end
.has_bomb
;if A == 0 then jump 
;NOP #16


org $099098
db $80

org $0895A5
;NOP #$10 ;16
JSR $8D68 : BCC .next
;BRA .next
.spritecollision
JSL bomb_explode
;call explode
.next
;NOP #$02
;LDA $0385, X : db #$F0, #$05 ;BEQ 05
LDA $03E4, X : CMP.b #$1C : db #$D0, #$05 ;BEQ 05

org $089594 ;Bomb execute code hook
JSL $248050
;NOP #$09 ; Remove code that have to be restored in JSL
;if bomblaunched = true then throw it
BEQ .notlaunched
JSR $9AAB
STZ $028A, X
NOP #$01

;20688DB03E
.notlaunched



org $248050
	LDA $0C72, X : STA $74
	LDA $0280, X : STA $75
	STZ $0280, X
	LDA $028A, X
	
RTL

bomb_explode:
{
LDA $0C5E, X : BNE .next
LDA #$02 : STA $039F, X
.next
RTL
}

bomb_create:
{
JSL change_sprite
 PHB : PHK : PLB


.player_has_bombs
LDA #$02 : STA $028A, X
LDA #$06 : STA $029E, X ;Bomb elevation
LDA $7EF343 ;retore bomb count 

.bombs_left_over
PLB
RTL 
}


    Palette_ArmorAndGloves:
    {
        ; Load armor palette
         PHB : PHK : PLB
        REP #$20
        
        ; Check what Link's armor value is.
        LDA $7EF35B : AND.w #$00FF : TAX
        
        ; (DEC06, X)
        LDA $1BEC06, X : AND.w #$00FF : ASL A : ADC.w #$F000 : STA $00
        ;replace D308 by 7000 and search
        REP #$10
        
        LDA.w #$01E2 ; Target SP-7 (sprite palette 6)
        LDX.w #$000E ; Palette has 15 colors
        
        TXY : TAX
        
        LDA $BC : AND #$00FF : STA $02
        ;LDA #$0040 : STA $02
    
    .loop
    
        LDA [$00] : STA $7EC300, X : STA $7EC500, X
        
        INC $00 : INC $00
        
        INX #2
        
        DEY : BPL .loop

        SEP #$30
        
        
        PLB
        INC $15
        RTL
    }

	change_sprite:
	{
	JSL $09C114         ; Restore the dungeon_resetsprites
	;$0DBA71 = random int
	JSL $0DBA71 : AND #$1F : !ADD #$40 : STA $BC
	JSL Palette_ArmorAndGloves
	STZ $0710
	RTL
	}

