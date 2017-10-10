org $0DBB67
Sound_SetSfxPanWithPlayerCoords:

org $0DBB8A
Sound_SetSfx3PanLong:

org $8888
Sound_LoadSongBank:
org $8900 ; change Sound_LoadSongBank to be JSL instead of JSR
RTL

org $890D ; change Sound_LoadIntroSongBank to JSL to Sound_LoadSongBank
    JSL NewLoadSoundBank_Intro

org $891F ; 
    JSL NewLoadSoundBank


;---------------------------------------------------------
; hack new soundfx in and change item get sound fx to new one
; set heart pick up sound to item get
; org $08C4CF
; db #$0F ; default is #$0B

; this is in C# side so it can be turned off
; ; sound fx 3, #$0F background note
; org $1A8D58
; db $00

; ; sound fx 3, #$0F background note #2? not sure what this does exactly
; org $1A8D97
; db $00

; ; 0xD1869
; org $1A9869 ; sound fx 3, #$0F tracker data
; ; original is E0 0B 10 78 B9 BA BB 60 BC 00
; db $E0, $19 ; set instrument $0B
; db $7f ; length
; ;db $ed, $e0
; db $97 ; note to play
; db $00 ; end


; sample 19 - FFFF FFFF in vanila
org $198068
db $88, $31, $FC, $38

; sfx instrument 19
org $268000
db $09, $00, $E1, $3E
; Format: 9 bytes per sample instrument.
; Byte 0: Left volume
; Byte 1: Right volume
; Byte 2: Starting pitch 1
; Byte 3: Starting pitch 2
; Byte 4: Sample (SRCN) number
; Byte 5: ADSR 1 / GAIN
; Byte 6: ADSR 2
; Byte 7: GAIN
; Byte 8: Tuning
db $7F, $7F, $00, $00, $19, $FF, $F0, $70, $04
; what.brr ; 774bytes -> ARAM $3188
db $74, $07, $88, $31
incbin what4.brr
db $00, $00, $00, $08
