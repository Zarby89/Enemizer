; 0xF5ABA
org $1EDABA ; Agahnim Ball - Code replaced above
    JSL newAgahBall
    BCS .hasbeendamaged ;IF Carry set
    JMP $DA46 ; 1EDA46 - routine for agahnim ball where it set speed and direction toward link
 
    .hasbeendamaged
    NOP #$13 ;Remove unused code that is in our new function
