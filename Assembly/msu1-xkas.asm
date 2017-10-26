;=======================================
;
; MSU-1 Enhanced Audio Patch
; ZeLDA no Densetsu - Kamigami no Triforce
; Modified for VT Randomizer
; 
; Author: qwertymodo
;
; Free space used: 0x77DDD-0x77F8A
;
;=======================================

lorom

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


ORG $0EFDDD
track_list:
    DB $00,$01,$03,$03,$03,$03,$03,$03
    DB $01,$03,$01,$03,$03,$03,$03,$03
    DB $03,$03,$03,$01,$03,$03,$03,$03
    DB $03,$03,$03,$03,$03,$01,$03,$03
    DB $03,$01,$01


ORG $0EFE00
msu_main:
    LDA $4210
    LDA !REG_MSU_ID_01
    CMP !VAL_MSU_ID_01
    BEQ .continue
.nomsu
    SEP #$30
    JML spc_continue
.continue
    LDA !REG_MSU_ID_23
    CMP !VAL_MSU_ID_23
    BNE .nomsu
    LDA !REG_MSU_ID_45
    CMP !VAL_MSU_ID_45
    BNE .nomsu
    SEP #$30
    LDX !REG_MUSIC_CONTROL
    BNE command_ff
    
do_fade:
    LDA !REG_CURRENT_VOLUME
    CMP !REG_TARGET_VOLUME
    BNE .continue
    JML spc_continue
.continue
    BCC .increment
.decrement
    SBC !VAL_VOLUME_DECREMENT
;   CMP !VAL_VOLUME_MUTE
    BCS .set
.mute
    STZ !REG_CURRENT_VOLUME
    STZ !REG_MSU_CONTROL
    STZ !REG_CURRENT_MSU_TRACK
    BRA .set
.increment
    ADC !VAL_VOLUME_INCREMENT
    BCC .set
    LDA !VAL_VOLUME_FULL
.set
    STA !REG_CURRENT_VOLUME
    STA !REG_MSU_VOLUME
    JML spc_continue

command_ff:
    CPX !VAL_COMMAND_LOAD_NEW_BANK
    BNE command_f3
    JML spc_continue

command_f3:
    CPX !VAL_COMMAND_FULL_VOLUME
    BNE command_f2
    STX !REG_SPC_CONTROL
    STX !REG_CURRENT_COMMAND
    LDA !VAL_VOLUME_FULL
    STA !REG_TARGET_VOLUME
    JML spc_continue

command_f2:
    CPX !VAL_COMMAND_FADE_HALF
    BNE command_f1
    STX !REG_SPC_CONTROL
    STX !REG_CURRENT_COMMAND
    LDA !VAL_VOLUME_HALF
    STA !REG_TARGET_VOLUME
    JML spc_continue

command_f1:
    CPX !VAL_COMMAND_FADE_OUT
    BNE load_track
    STX !REG_SPC_CONTROL
    STX !REG_CURRENT_COMMAND
    STX !REG_CURRENT_TRACK
    STZ !REG_TARGET_VOLUME
    JML spc_continue

load_track:
    CPX !REG_CURRENT_MSU_TRACK
    BNE .continue
    CPX #$1B
    BEQ .continue
    JML spc_continue
.continue
    STX !REG_MSU_TRACK_LO
    STZ !REG_MSU_TRACK_HI
    STZ !REG_MSU_CONTROL
    LDA !VAL_VOLUME_FULL
    STA !REG_TARGET_VOLUME
    STA !REG_CURRENT_VOLUME
    STA !REG_MSU_VOLUME

msu_check_busy:
    LDA !REG_MSU_STATUS
    BIT !FLAG_MSU_STATUS_AUDIO_BUSY
    BNE msu_check_busy
    BIT !FLAG_MSU_STATUS_TRACK_MISSING
    BEQ msu_play

spc_fallback:
    STZ !REG_MSU_CONTROL
    STZ !REG_CURRENT_MSU_TRACK
    STZ !REG_TARGET_VOLUME
    STZ !REG_CURRENT_VOLUME
    STZ !REG_MSU_VOLUME
    JML spc_continue

msu_play:
    LDA $1A9D28 ;   Check if SPC data is already muted
    BEQ .continue
    LDA !VAL_COMMAND_FADE_OUT
    STA !REG_SPC_CONTROL
.continue
    LDA track_list,x
    STA !REG_MSU_CONTROL
    STX !REG_CURRENT_COMMAND
    STX !REG_CURRENT_TRACK
    STX !REG_CURRENT_MSU_TRACK
    JML spc_continue


pendant_fanfare:
    REP #$20
    LDA !REG_MSU_ID_01
    CMP !VAL_MSU_ID_01
    BNE .fallback
    LDA !REG_MSU_ID_23
    CMP !VAL_MSU_ID_23
    BNE .fallback
    LDA !REG_MSU_ID_45
    CMP !VAL_MSU_ID_45
    BNE .fallback
    SEP #$20
    LDA !REG_MSU_STATUS
    BIT !FLAG_MSU_STATUS_AUDIO_PLAYING
    BNE .continue
.done
    JML pendant_done
.continue
    JML pendant_continue
.fallback
    SEP #$20
    LDA !REG_SPC_CONTROL
    BNE .continue
    BRA .done


crystal_fanfare:
    REP #$20
    LDA !REG_MSU_ID_01
    CMP !VAL_MSU_ID_01
    BNE .fallback
    LDA !REG_MSU_ID_23
    CMP !VAL_MSU_ID_23
    BNE .fallback
    LDA !REG_MSU_ID_45
    CMP !VAL_MSU_ID_45
    BNE .fallback
    SEP #$20
    LDA !REG_MSU_STATUS
    BIT !FLAG_MSU_STATUS_AUDIO_PLAYING
    BNE .continue
.done
    JML crystal_done
.continue
    JML crystal_continue
.fallback
    SEP #$20
    LDA !REG_SPC_CONTROL
    BNE .continue
    BRA .done


ending_wait:
    REP #$20
    LDA !REG_MSU_ID_01
    CMP !VAL_MSU_ID_01
    BNE .done
    LDA !REG_MSU_ID_23
    CMP !VAL_MSU_ID_23
    BNE .done
    LDA !REG_MSU_ID_45
    CMP !VAL_MSU_ID_45
    BNE .done
    SEP #$20
.wait
    LDA !REG_MSU_STATUS
    BIT !FLAG_MSU_STATUS_AUDIO_PLAYING
    BNE .wait
.done
    SEP #$20
    LDA #$22
    RTL