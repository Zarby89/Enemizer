org $9C50B ; 0x4C50B
{
    ; .loadData
    ;     ; $4C50B-
    ;     STA $01 ; 85 01
    ;     ; $4C50D-
    ;     LDY.w #$0000 ; A0 00 00
    JSL LoadOverworldSprites
    NOP
}
    
org $9C510 ; 0x4C510
LDA [$00], Y ; replace LDA ($00), Y
; CMP.b #$FF : BEQ .stopLoading
; INY #2
org $9C518 ; 0x4C518
LDA [$00], Y ; replace LDA ($00), Y
; DEY #2 : CMP.b #$F4 : BNE .notFallingRocks
; INC $0FFD
; INY #3
; BRA .nextSprite
; .notFallingRocks ; Anything other than falling rocks.
org $9C528 ; 0x4C528
LDA [$00], Y ; replace LDA ($00), Y
; PHA : LSR #4 : ASL #2 : 
org $9C531 ; 0x4C531
STA $0A ; STA $02
; INY
org $9C534 ; 0x4C534
LDA [$00], Y ; replace LDA ($00), Y
; LSR #4 : CLC
org $9C53B ; 0x4C53B
ADC $0A ; ADC $02
; STA $06
; PLA : ASL #4 : STA $07
org $9C546 ; 0x4C546
LDA [$00], Y ; replace LDA ($00), Y
; AND.b #$0F : ORA $07 : STA $05
; INY
org $9C54F ; 0x4C54F
LDA [$00], Y ; replace LDA ($00), Y
; LDX $05 : INC A : STA $7FDF80, X

    ;     ; $4C558-
    ;     ; Move on to the next sprite / overlord.
    ;     INY ; C8
    ;     ; $4C559-
    ;     BRA .nextSprite ; 80 B5
    
    ; .stopLoading
    ;     ; $4C55B-
    ;     SEP #$10 ; E2 10
    ;     ; $4C55D-
    ;     RTS      ; 60
