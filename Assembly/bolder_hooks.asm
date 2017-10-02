;================================================================================
; Remove bolder check for top quadrant so they work everywhere
;--------------------------------------------------------------------------------
; maybe this should be done in c# side?
;remove the Y scrolling check for boulders
org $09B72E
NOP #$0A
;--------------------------------------------------------------------------------
