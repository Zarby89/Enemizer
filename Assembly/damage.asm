CheckIfLinkShouldDie:
    ; before this we should have:
    ; LDA $7EF36D - this gets hooked, but we should have LDA at the end of it

    CMP $00 : BCC .dead
        SEC : SBC $00
        BRA .done
    .dead
        LDA #$00
.done
RTL
