Spawn_Bees:
{
    LDA #$79
    JSL Sprite_SpawnDynamically
    BMI .done
    LDA $22
    STA $0D10, Y
    LDA $23
    STA $0D30, Y
    LDA $20
    STA $0D00, Y
    LDA $21
    STA $0D20, Y
.done
    RTL
}