;================================================================================
; NMI Hook
;--------------------------------------------------------------------------------
; rando already hooks the Bank00.asm : 164 (PHA : PHX : PHY : PHD : PHB) so we have to hook after that
org $0080D0 ; <- D0 - Bank00.asm : 164-167 (PHB, LDA.w #$0000)
JML.l NMIHookAction
org $0080D5 ; <- D5 - Bank00.asm : 164-167 (PHB, LDA.w #$0000)
NMIHookReturn:
;--------------------------------------------------------------------------------
