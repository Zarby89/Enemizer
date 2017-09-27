lorom

;================================================================================

!ADD = "CLC : ADC"
!SUB = "SEC : SBC"
!BLT = "BCC"
!BGE = "BCS"

org $00FFD7 ; Set rom on 2mb
db #$0C

org $5FFFFF ; write at the last position to expand on 2mb
db #$00

;=Constants======================================================================

!BUSHES_FLAG = "$408000"
!BLIND_DOOR_FLAG = "$408001"
!MOLDORM_EYES_FLAG = "$408002"

;================================================================================

incsrc hooks.asm
incsrc bosses_hooks.asm
incsrc moldorm_hooks.asm
incsrc DMA.asm

;================================================================================
org $408000
incsrc enemizerflags.asm
incsrc bushes.asm
incsrc terrorpin.asm
incsrc special_action.asm
incsrc bosses_moved.asm
incsrc bossdrop.asm
incsrc moldorm.asm

;================================================================================

org $0DBA71
GetRandomInt:
