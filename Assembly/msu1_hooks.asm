;=======================================
;
; MSU-1 Enhanced Audio Patch
; ZeLDA no Densetsu - Kamigami no Triforce
; Modified for VT Randomizer
; 
; Author: qwertymodo
;
; Modified for use with Enemizer
;=======================================

!REG_MSU_STATUS = $2000

!REG_MSU_ID_0 = $2002
!REG_MSU_ID_1 = $2003
!REG_MSU_ID_2 = $2004
!REG_MSU_ID_3 = $2005
!REG_MSU_ID_4 = $2006
!REG_MSU_ID_5 = $2007

!REG_MSU_ID_01 = $2002
!REG_MSU_ID_23 = $2004
!REG_MSU_ID_45 = $2006


!VAL_MSU_ID_0 = #$53    ;   'S'
!VAL_MSU_ID_1 = #$2D    ;   '-'
!VAL_MSU_ID_2 = #$4D    ;   'M'
!VAL_MSU_ID_3 = #$53    ;   'S'
!VAL_MSU_ID_4 = #$55    ;   'U'
!VAL_MSU_ID_5 = #$31    ;   '1'

!VAL_MSU_ID_01 = #$2D53 ;   'S-'
!VAL_MSU_ID_23 = #$534D ;   'MS'
!VAL_MSU_ID_45 = #$3155 ;   'U1'


!REG_MSU_TRACK = $2004
!REG_MSU_TRACK_LO = $2004
!REG_MSU_TRACK_HI = $2005
!REG_MSU_VOLUME = $2006
!REG_MSU_CONTROL = $2007


!FLAG_MSU_PLAY = #$01
!FLAG_MSU_REPEAT = #$02
!FLAG_MSU_STATUS_TRACK_MISSING = #$08
!FLAG_MSU_STATUS_AUDIO_PLAYING = #$10
!FLAG_MSU_STATUS_AUDIO_REPEATING = #$20
!FLAG_MSU_STATUS_AUDIO_BUSY = #$40
!FLAG_MSU_STATUS_DATA_BUSY = #$80


!REG_CURRENT_VOLUME = $0127
!REG_TARGET_VOLUME = $0129
!REG_CURRENT_MSU_TRACK = $012B
!REG_MUSIC_CONTROL = $012C
!REG_CURRENT_TRACK = $0130
!REG_CURRENT_COMMAND = $0133

!REG_SPC_CONTROL = $2140
!REG_NMI_FLAGS = $4210

!VAL_COMMAND_FADE_OUT = #$F1
!VAL_COMMAND_FADE_HALF = #$F2
!VAL_COMMAND_FULL_VOLUME = #$F3
!VAL_COMMAND_LOAD_NEW_BANK = #$FF

!VAL_VOLUME_INCREMENT = #$10
!VAL_VOLUME_DECREMENT = #$02
!VAL_VOLUME_MUTE = #$0F
!VAL_VOLUME_HALF = #$80
!VAL_VOLUME_FULL = #$FF


ORG $0080D7
spc_nmi:
    JML msu_main
    NOP
spc_continue:


ORG $08C421
    JML pendant_fanfare
    NOP
pendant_continue:
ORG $08C42B
pendant_done:


ORG $08C62A
    JML crystal_fanfare
    NOP
crystal_done:
ORG $08C637
crystal_continue:


ORG $0EE6EC
    JSL ending_wait
