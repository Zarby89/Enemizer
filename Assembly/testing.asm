lorom

; x button hook
;----------------------------------------
; inside dungeon/room
org $0287FB
InsideDungeon_XButton:
JSL Dungeon_XButton_Hook
;----------------------------------------

org $0DBB67
Sound_SetSfxPanWithPlayerCoords:

org $0DBB8A
Sound_SetSfx3PanLong:


org $258000
;----------------------------------------
Dungeon_XButton_Hook:
    LDA $F6 : AND.b #$40 : BEQ .xIsNotPressed 
    ; x pressed code 
		LDA.b #$0F : JSL Sound_SetSfx3PanLong
    ; 0A is start up chime
    ; 0F is chest item get sound

    .xIsNotPressed
    LDA #$00
    RTL
;----------------------------------------

; sound fx 3, #$0F background note
org $1A8D58
db $00

; sound fx 3, #$0F background note #2? not sure what this does exactly
org $1A8D97
db $00

org $1A9869 ; sound fx 3, #$0F tracker data
; original is E0 0B 10 78 B9 BA BB 60 BC 00
db $E0, $0B ; set instrument $0B
db $7f ; length
db $DB ; note to play
db $00 ; end
