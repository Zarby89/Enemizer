;-------------
NMIHookAction:
{
    ;-----------------------------------------
    ; do our shell stuff
    PHA
    PHP

    SEP #$20 ; get into 8-bit mode
    
    LDA !SHELL_DMA_FLAG : BEQ .return ; check our draw flag
    AND #$01 : BNE .loadKholdstare
    LDA !SHELL_DMA_FLAG : AND #$02 : BNE .loadTrinexx
    BRA .return ; just in case
    ;BIT #$01 : BEQ .loadKholdstare
    ;BIT #$02 : BEQ .loadTrinexx

.loadKholdstare
    JSL DMAKholdstare
    LDA #$00 : STA !SHELL_DMA_FLAG ; clear our draw flag
    BRA .return

.loadTrinexx
    JSL DMATrinexx
    LDA #$00 : STA !SHELL_DMA_FLAG ; clear our draw flag

.return
    PLP
    PLA
    ;-----------------------------------------
    ; restore code Bank00.asm (164-167)
    PHB
    ; Sets DP to $0000
    LDA.w #$0000 : TCD

JML.l NMIHookReturn
}

DMAKholdstare:
{
    %DMA_VRAM(#$34,#$00,#$24,#$B0,#$00,#$10,#$00)
    RTL
}

DMATrinexx:
{
    ; TODO: change this to trinexx gfx
    %DMA_VRAM(#$34,#$00,#$24,#$C0,#$00,#$08,#$00)
    %DMA_VRAM(#$3A,#$A0,#$24,#$C8,#$00,#$00,#$C0)

    RTL
}
