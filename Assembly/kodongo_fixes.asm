newKodongoCollision:
{
    LDA $0DE0, X : INC A : AND.b #$03 : STA $0DE0, X
    ;If they collide more than 32time then kill them !
    LDA $0DA0, X : INC A : STA $0DA0, X : CMP #$20 : BCC .continue
    LDA #$06 : STA $0DD0, X
.continue
RTL
}