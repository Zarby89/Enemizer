lorom

;================================================================================

!ADD = "CLC : ADC"
!SUB = "SEC : SBC"
!BLT = "BCC"
!BGE = "BCS"

!BUSHES_FLAG = "$408000"
!BLIND_DOOR_FLAG = "$408001"

;================================================================================

incsrc hooks.asm
incsrc bosses_hooks.asm
incsrc DMA.asm

org $408000
incsrc enemizerflags.asm
incsrc randomize_bushes.asm
incsrc terrorpin.asm
incsrc bosses_moved.asm

;================================================================================

org $0DBA71
GetRandomInt:
