lorom

!SUB = "SEC : SBC"
!ADD = "CLC : ADC"

;MUST BE IN MAIN FILE
!RANDOM_SPRITE_FLAG = #$00

;MUST BE IN HOOKS FILE
;MUST BE IN HOOKS FILE
;MUST BE IN HOOKS FILE
org $008A01
LDA $BC

org $1BEDF9
JSL Palette_ArmorAndGloves ;4bytes
RTL ;1byte 
NOP #$01


org $1BEE1B
JSL Palette_ArmorAndGloves_part_two
RTL

org $06F40C
JSL change_sprite : NOP #$01 ;LDA $0E20, X : CMP.b #$61

org $0CCD9D
JSL OnInitFileSelect




; NEW CODE THAT MUST BE IN NEW ASM FILE
; NEW CODE THAT MUST BE IN NEW ASM FILE
; NEW CODE THAT MUST BE IN NEW ASM FILE
org $24A3FA

    OnInitFileSelect:
    {
        LDA !RANDOM_SPRITE_FLAG : BEQ .continue
        LDA.b #$10 : STA $BC
	    JSL $0DBA71 : AND #$1F : !ADD #$60 : STA $BC
        .continue
        JSL $00893D;Restore the previous code
        RTL
    }


    Palette_ArmorAndGloves:
    {
        ;DEDF9
        LDA !RANDOM_SPRITE_FLAG : BNE .continue
            LDA.b #$10 : STA $BC ; Load Original Sprite Location
            REP #$21
            LDA $7EF35B
            JSL $1BEDFF;Read Original Palette Code
        RTL
        .part_two
        SEP #$30
        LDA !RANDOM_SPRITE_FLAG : BNE .continue
            REP #$30
            LDA $7EF354
            JSL $1BEE21;Read Original Palette Code
        RTL

        .continue

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
        
        ;LDA  $7EC178 : AND #$00FF : STA $02
        LDA.b $BC : AND #$00FF : STA $02
    
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
    LDA !RANDOM_SPRITE_FLAG : BEQ .continue
	JSL $0DBA71 : AND #$1F : !ADD #$60 : STA $BC
    STA $7EC178
	JSL Palette_ArmorAndGloves
	STZ $0710

    .continue
    LDA $0E20, X : CMP.b #$61;Restored Code

	RTL
	}

