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

; Enemizer reserved memory
; $7F50B0 - $7F50BF - Downstream Reserved (Enemizer)

;=Constants======================================================================

!BUSHES_FLAG = "$408000"
!BLIND_DOOR_FLAG = "$408001"
!MOLDORM_EYES_FLAG = "$408002"

!SHELL_DMA_FLAG = "$7F50B0"
;================================================================================

incsrc hooks.asm
incsrc DMA.asm

;================================================================================
org $408000
incsrc enemizerflags.asm
incsrc bushes.asm
incsrc NMI.asm
incsrc terrorpin.asm
incsrc special_action.asm
incsrc bosses_moved.asm
;incsrc sprite_damage.asm
incsrc bossdrop.asm
incsrc moldorm.asm

;================================================================================

org $0DBA71
GetRandomInt:
