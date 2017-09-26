;================================================================================
; Special action
;================================================================================
check_special_action:
{
    LDA $7E0CF3 : BEQ .no_special_action
        LDA.b #$05 : STA $11
        STZ $7E0CF3 
    .no_special_action
    JSL $078000
    RTL
}