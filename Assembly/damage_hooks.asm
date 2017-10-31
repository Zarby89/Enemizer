org $780CA ; Bank07.asm(179)
JSL CheckIfLinkShouldDie : NOP : NOP : NOP
;SEC : SBC.b $00 : CMP #$00 : BEQ .linkIsDead ; Bank07.asm(179) - 

org $780D1
BNE linkNotDead : NOP : NOP ; Bank07.asm(183) - CMP.b #$A8 : BCC .linkNotDead

org $780D5
linkIsDead:

org $780F7
linkNotDead:
