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

!BUSHES_FLAG = "$408100"
!BLIND_DOOR_FLAG = "$408101"
!MOLDORM_EYES_FLAG = "$408102"
!RANDOM_SPRITE_FLAG = "$408103"

; Enemizer reserved memory
; $7F50B0 - $7F50BF - Downstream Reserved (Enemizer)
!SHELL_DMA_FLAG = "$7F50B0"
!SOUNDFX_LOADED = "$7F50B1"
;================================================================================

incsrc hooks.asm
incsrc DMA.asm
;incsrc testing.asm ; make sure to comment this out for release!!!
incsrc externalhooks.asm ; this is from z3randomizer source. be sure to check for updates

;================================================================================
org $408000
EnemizerTablesStart:
incsrc enemizer_info_table.asm
incsrc enemizerflags.asm
incsrc bushes_table.asm
incsrc room_header_table.asm

; code
EnemizerCodeStart:
incsrc bushes.asm
incsrc NMI.asm
incsrc init.asm
incsrc terrorpin.asm
incsrc special_action.asm
incsrc bosses_moved.asm
;incsrc sprite_damage.asm
incsrc damage.asm
incsrc bossdrop.asm
incsrc moldorm.asm
incsrc sprite_randomizer.asm
incsrc kodongo_fixes.asm
incsrc mimic_fixes.asm
;incsrc location_menu.asm
incsrc load_file.asm
incsrc soundfx_changes.asm
incsrc msu1.asm
incsrc sword_and_shield.asm

; data
incsrc room_object_table.asm
incsrc shell_gfx.asm
warnpc $40FFFF ;if we hit this we need to split stuff by bank
;================================================================================

incsrc export_symbols.asm

org $0DBA71
GetRandomInt:

org $0DBB67
Sound_SetSfxPanWithPlayerCoords:

org $0DBB8A
Sound_SetSfx3PanLong:

