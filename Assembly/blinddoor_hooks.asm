;================================================================================
; Blind door close
;--------------------------------------------------------------------------------
; 
org $028849 ; Bank02.asm(1588) - original code : JSL $078000 //Hook on player main when transition are over execute player code
JSL check_special_action ;using the variable 7E0CF3 if it not 00 then trap the player in that room 
;could be changed easily to support more than only 1 function
;--------------------------------------------------------------------------------

org $078000
Player_Main:
