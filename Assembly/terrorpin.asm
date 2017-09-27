;================================================================================
; Terrorpin AI Fixes
;================================================================================
FixTerrorpin:
{
	PHA ; save A so the orignal code doesn't kill it
	
	AND.b #$03 : STA $0DE0, X ; restore what we overwrote
	
	PLA ; restore A so the AND/BNE in the original code actually does something
	
	RTL
}
