;-------------
NMIHookAction:
{
    ;-----------------------------------------
    ; do our shell stuff
    PHA
    
    LDA $0000 : BNE .doNothing
    BIT #$01 : BEQ .loadKholdstare
    BIT #$02 : BEQ .loadTrinexx

.loadKholdstare
    %DMA_VRAM(#$34,#$00,#$24,#$B0,#$00,#$10,#$00)

.loadTrinexx
    %DMA_VRAM(#$34,#$00,#$24,#$B0,#$00,#$10,#$00)

.doNothing
    PLA
    ;-----------------------------------------
    ; restore code Bank00.asm (164-167)
    PHB
    ; Sets DP to $0000
    LDA.w #$0000 : TCD

JML.l NMIHookReturn
}
