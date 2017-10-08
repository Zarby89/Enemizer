lorom

;!SUB = "SEC : SBC"
;!ADD = "CLC : ADC"

;org $00FFD7 ; Set rom on 24mb
;db #$0C

;org $7FFFFF ; write at the last position to expand on 3mb
;db #$00

; ALL OF THIS NEED TO BE RECODED IN C#
; ALL OF THIS NEED TO BE RECODED IN C#
; ALL OF THIS NEED TO BE RECODED IN C#
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

; ALL OF THIS NEED TO BE IN HOOKS
; ALL OF THIS NEED TO BE IN HOOKS
; ALL OF THIS NEED TO BE IN HOOKS
org $008A01
LDA $BC

org $1BEDF9
JSL Palette_ArmorAndGloves
RTL

org $1BEE1B
JSL Palette_ArmorAndGloves
RTL

org $06F40C
JSL change_sprite : NOP #$01 ;LDA $0E20, X : CMP.b #$61


; NEW CODE THAT MUST BE IN NEW ASM FILE
; NEW CODE THAT MUST BE IN NEW ASM FILE
; NEW CODE THAT MUST BE IN NEW ASM FILE
org $24A3FA

    Palette_ArmorAndGloves:
    {
        PHX : PHY : PHA
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
        
        LDA  $7EC178 : AND #$00FF : STA $02
        ;LDA #$0040 : STA $02
    
    .loop
    
        LDA [$00] : STA $7EC300, X : STA $7EC500, X
        
        INC $00 : INC $00
        
        INX #2
        
        DEY : BPL .loop

        SEP #$30
        
        
        PLB
        INC $15
        PLA : PLY : PLX
        RTL
    }

	change_sprite:
	{
	;JSL $09C114         ; Restore the dungeon_resetsprites
	;$0DBA71 = random int
	JSL $0DBA71 : AND #$1F : !ADD #$40 : STA $BC
    STA $7EC178
	JSL Palette_ArmorAndGloves
	STZ $0710
    LDA $0E20, X : CMP.b #$61;Restored Code

	RTL
	}

