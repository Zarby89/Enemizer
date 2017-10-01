;LDA $0F : ORA.b #$02 : STA ($92), Y
;NOP
;NOP
;LDA #$02 : STA ($92), Y

;EA
;A9029192

;C220A590186908008590E692E692E220
;		; C2 20
;        REP #$20
;NOP
;		;A5 90  : 18  : 69 08 00     : 85 90
;        LDA $90 : CLC : ADC.w #$0008 : STA $90
;NOP        
;		; E6 92 : E6 92
;        INC $92 : INC $92
;NOP
;		; E2 20
;		SEP #$20

lorom

org $00FFD7 ; Set rom on 2mb
db #$0C

org $5FFFFF ; write at the last position to expand on 2mb
db #$00

; org $0DBA80;JP VALID
; OAM_AllocateFromRegionA:
; org $0DBA84;JP VALID
; OAM_AllocateFromRegionB:
; org $0DBA88;JP VALID
; OAM_AllocateFromRegionC:
; org $0DBA8C;JP VALID
; OAM_AllocateFromRegionD:
; org $0DBA90;JP VALID
; OAM_AllocateFromRegionE:
; org $0DBA94;JP VALID
; OAM_AllocateFromRegionF:

; dw $0171 ; 0x0030 - 0x016F? (For now calling this region A)
; dw $0201 ; 0x01D0 - 0x01FF? (For now calling this region B)
; dw $0031 ; 0x0000 - 0x002F? (For now calling this region C)
; dw $00C1 ; 0x0030 - 0x00BF? (For now calling this region D)
; dw $0141 ; 0x0120 - 0x013F? (For now calling this region E)
; dw $01D1 ; 0x0140 - 0x01CF? (For now calling this region F)



; ; *$EE9AD-$EE9B5 LOCAL		
; org $1DE9AD
; Sprite4_PrepOamCoord:

; ; *$ED6F6-$ED6FD LONG
; org $1DD6F6
; Sprite_GiantMoldormLong:


;$31469
; org $061469
; Sprite_GiantMoldormTrampoline:
; {
	; JSL New_Sprite_GiantMoldormTrampoline
; }

; ;$ED881
; org $1DD881
; GiantMoldorm_Draw:
; {
; ;20 AD E9 
; ;A9 0B : 9D 50 0F
	; JSL New_GiantMoldorm_Draw
	
	; NOP #$04
; }







;ED8C0
; org $1DD8C1
; {
	 ; ; turn off head drawing
	 ; ;20 93 D9
	 ; ;        JSR GiantMoldorm_DrawHead
	 ; ;NOP #$03
; }

; adjust oam position after drawing eyes
;ED88E
org $1DD88E
{
    ; ; lda $90 : add.w #$0008 : sta $90
    ; INC $92 : INC $92

	JSL UpdateOamPosition
    NOP #08
	
	; LDA $90 : CLC : ADC.w #$0010 : STA $90
	; ASL $92 : ASL $92
}

; number of eyes
org $1DDBB2
{
    ; LDX.b #$01
	; number of eyes
    LDX.b #$08
}

; moldorm eye size
; org $1DDBFC
; {
    ; ; LDA $0F : ORA.b #$02 : STA ($92), Y    
    ; LDA #$02 : STA ($92), Y
    ; NOP : NOP
; }


; $EDBA9
; org $1DDBA9
; {
	; ; original ;        ; EDBA9 : EDBAC : EDBAD-EDBAE : EDBB0
	; ; original ;        LDA $0DE0, X : ADD.b #$FF : STA $06

	; JSL Subtract
	; NOP #$04
; }

!MOLDORM_EYES_FLAG = "$208000"

org $208000
db #$08

; Subtract:
; {
	; ; original ;LDA $0DE0, X : ADD.b #$FF : STA $06
	; LDA $0DE0, X
	; SEC
	; SBC -1 ; -3 for 4 eyes, -5 for 6 eyes, -7 for 8 eyes
	; STA $06
	
	; RTL
; }

UpdateOamPosition:
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

; UpdateOamPosition:
; {
	; PHX

	; LDX !MOLDORM_EYES_FLAG
	; .more_eyes
	; LDA $90 : CLC : ADC.w #$0004 : STA $90
	; DEX
	; BPL .more_eyes 		; X >= 0
	
	; LDA $92 : CLC : ADC.w !MOLDORM_EYES_FLAG : STA $92
	
	; PLX

	; RTL
; }



; New_Sprite_GiantMoldormTrampoline:
; {
	
	; ; lda #16
	; ; jsl OAM_AllocateFromRegionD

	
	; JSL Sprite_GiantMoldormLong

	; RTL
; }