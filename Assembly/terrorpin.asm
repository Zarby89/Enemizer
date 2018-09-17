;================================================================================
; Terrorpin AI Fixes
;================================================================================
FixTerrorpin:
{
    LDA !ENABLE_TERRORPIN_AI_FIX : BNE .new ; check if option is on
        ; do the old code that smokes A
        AND.b #$03 : STA $0DE0, X
        RTL

    .new
        PHA ; save A so the orignal code doesn't kill it
        AND.b #$03 : STA $0DE0, X ; restore what we overwrote
        PLA ; restore A so the AND/BNE in the original code actually does something
	RTL
}
