    ; *$5FFA8-$5FFF5 LONG
    WallMaster_SendPlayerToLastEntrance:
    {
        JSL Dungeon_SaveRoomData.justKeys
        JSL Dungeon_SaveRoomQuadrantData
        JSL Sprite_ResetAll
        
        ; Don't use a starting point entrance.
        STZ $04AA ; 9C AA 04
;$04AA[0x02] -   
;    Flag, that if nonzero, tells us to load using a starting point entrance 
;    value rather than a normal entrance value.
;$04AA[0x01] -   This is used when you die and choose to save & continue
;                It is the dungeon entrance to put Link into.
;                This variable is only used if you die in a dungeon, and is set to the
;                last dungeon entrance you went into.
        
        ; Falling into an overworld hole mode.
        LDA.b #$11 : STA $10
;$10[0x01] - (Main)    
;    Main Module index. This controls which top level subprogram we're currently
;    in.
;    0x11 - Happens when you fall into a hole from the OW.
        
        STZ $11
;$11[0x01] - (Main)
;    Submodule Index (See $B0)
;$B0[0x01] - Sub-submodule index. (Submodules of the $11 submodule index.)

        STZ $14
;$14[0x01] - (NMI)
;    Value based flag, that if nonzero, causes the tilemap to update from one of
;    several source addresses. Some are in ram, but most are in rom. However,
;    the ram address $001000 is most commonly used as the source address buffer.
;    The others are used for highly specific parts of the game code, such as
;    the intro.
    
    ; *$5FFBF ALTERNATE ENTRY POINT
    
        STZ $0345
;$0345[0x01] -   (Player)
;    Set to 1 when we are in deep water. 0 otherwise.
        
        ; \wtf 0x11? Written here? I thought these were all even.
        STA $005E
;$5E[0x01] - (Player)
;    Speed setting for Link. The different values this can be set to index into
;    a table that sets his real speed. Some common values:
;    0x00 - Normal walking speed
;    0x02 - Walking (or dashing) on stairs
;    0x10 - Dashing
        
        STZ $03F3
;$03F3[0x01] -   ????

        STZ $0322
;$0322[0x01] -   ????

        STZ $02E4
;$02E4[0x01] -   (Player)
;    Flag that, if nonzero, will not allow Link to move.
;    Requires further research as to its generalized usage.
;    Also... Link cannot bring down the menu if this is nonzero.
;    Additionally.

        STZ $0ABD
;$0ABD[0x01] -   (Palette)
;    Used in order to swap palettes under certain special circumstances.
;    Apparently related almost entirely to the flute boy ghost and the ponds of wishing.
;    When zero, doesn't induce any behavior change, but when nonzero, it will cause
;    SP-0 and SP-7 (full) to swap and SP-5 and SP-3 (second halves) to swap.

        STZ $036B
;$036B[0x01] -   ????

        STZ $0373
;$0373[0x01] -   (Player)
;    Putting a non zero value here indicates how much life to subtract from Link.
;    (quick reference: 0x08 = one heart)

        
        STZ $27
;$27[0x01] - (Player)
;    Link's Recoil for vertical collisions

        STZ $28
;$28[0x01] - (Player)
;    Link's Recoil for horizontal collisions

		
        STZ $29
;$29[0x01] - (Player)
;    Vertical resistance
        
        STZ $24
;$24[0x02] - (Dungeon, Overworld)
;    0xFFFF usually, but if Link is elevated off the ground it is considered to
;    be his Z coordinate. That is, it's his height off of the ground.
        

        STZ $0351
;$0351[0x01] -   (PlayerOam)
;    Value that, if set to 1, draws the water ripples around the player sprite
;    while standing in water. The drawing, of course, uses sprites.
;    If the value is 2, then a patch of tall grass is instead. Any value other
;    than 2 (besides 0) produces the water effect.

        STZ $0316
;$0316[0x02] -   ???? bunny stuffs? 

        STZ $031F
;$031F[0x01] -   
;    Countdown timer that, when it's set, causes Link's sprite to
;    blink, i.e. flash on and off
        

        LDA.b #$00 : STA $5D
;$5D[0x01] - Player Handler or "State"
;    0x00 - ground state
        
        STZ $4B
;$4B[0x01] - 
;    Link's visibility status. If set to 0x0C, Link will disappear.
    
    ; *$5FFEE ALTERNATE ENTRY POINT
    
        JSL Ancilla_TerminateSelectInteractives
        JML Player_ResetState
    }

	
	; *$121B1-$121E4 LONG (Bank02.asm)
    Dungeon_SaveRoomData:
    {
        LDA $040C : CMP.b #$FF : BEQ .notInPalace
        
        LDA.b #$19 : STA $11
        
        STZ $B0
        
        LDA.b #$33 : STA $012E
        
        JSL Dungeon_SaveRoomQuadrantData
    
    ; *$121C7 ALTERNATE ENTRY POINT (Bank02.asm)
    .justKeys
    
        ; branch if in a non palace interior.
        LDA $040C : CMP.b #$FF : BEQ .return
;$040C[0x02] -   (Dungeon)
;    Map index for dungeons. If it's equal to 0xFF, that means there is no map
;    for that area.
;	0x00 - 0x1A (multiple of 2)        

        ; Is it the Sewer?
        CMP.b #$02 : BNE .notSewer
;    0x02 - Hyrule Castle
        
        ; If it's the sewer, put them in the same slot as Hyrule Castles's. annoying :p
        LDA.b #$00
    
    .notSewer
    
        LSR A : TAX
        
        ; Load our current count of keys for this dungeon.
        ; Save it to an appropriate slot.
        LDA $7EF36F : STA $7EF37C, X
;$7EF36F = current key count
;$7EF37C,X = dungeon key count
    
    .return
    
        RTL
    
    .notInPalace ; we never get here from wallmaster
    
        ; Play the error sound effect
        LDA.b #$3C : STA $012E
        
        RTL
    }


	.......
    ; *$13929 ALTERNATE ENTRY POINT (Bank02.asm)
    shared Dungeon_SaveRoomQuadrantData:
    
        ; figures out which Quadrants Link has visited in a room.
        
        ; Mapped to bit 3.
        LDA $A7 : ASL #2 : STA $00
        
        ; Mapped to bit 2.
        LDA $A6 : ASL A : ORA $00
;$A6[0x01] - Set to 0 or 2, but it depends upon the dungeon room's layout
;            and the quadrant it was entered from. Further investigation seems
;            to indicate that its purpose is to control the camera / scrolling
;            boundaries in dungeons.
;$A7[0x01] - Same as $A6, but for vertical camera scrolling.

        
        ; Mapped to bit 1.
        ORA $AA
;$AA[0x01] - 2 if you are the lower half of the room. 0 if you are on the upper half.
        
        ; Mapped to bit 0.        
        ORA $A9
;$A9[0x01] - 0 if you are on the left half of the room. 1 if you are on the right half.
        
        ; X ranges from 0x00 to 0x0F
        TAX
        
        ; These determine the quadrants Link has seen in this room.
        LDA $02B5CC, X : ORA $0408 : STA $0408
;    ; $135CC-$135DB ($02B5CC)
;    {
;        db $08, $04, $02, $01, $0C, $0C, $03, $03
;        db $0A, $05, $0A, $05, $0F, $0F, $0F, $0F
;    }
;$0408[0x02] -   Only lowest 4 bits are used. Record of the quadrants that Link
;                has visited in the current room.

        JSR $B947 ; $13947 IN ROM ; Save the room data and exit.
        
        RTL
    }

    ; *$13947-$13967 LOCAL
    {
        ; Saves data for the current room
        
        REP #$30
        
        ; What room are we in... use it as an index.
        LDA $A0 : ASL A : TAX
;$A0[0x02] - The index used for loading a dungeon room. There are 296 rooms all in all. (mirrored in other variables).
        
        ; Store other data, like chests opened, bosses killed, etc.
        LDA $0402 : LSR #4 : STA $06
;$0402[0x01] -   Certainly related to $0403, but contains other information I haven�t looked at yet.
;$00[0x10] - (Main)
;    Mainly used as work registers. Storage of addresses and values.
        
        ; Store information about this room when it changes.
        LDA $0400 : AND.w #$F000 : ORA $0408 : ORA $06 : STA $7EF000, X
;$0400[0x02] -   Tops four bits: In a given room, each bit corresponds to a
;                door being opened. If set, it has been opened by some means
;                (bomb, key, etc.)
;$0408[0x02] -   Only lowest 4 bits are used. Record of the quadrants that Link
;                has visited in the current room.
;$7EF000[0x500] -    Save Game Memory, which gets mapped to a slot in SRAM when
;                    you save your game. SRAM slots are at $70:0000, $70:0500,
;                    and $70:0A00. They are also mirrored in the next three
;                    slots. See the sram documentation for more details.
        
        SEP #$30
        
        RTS
    }
	

    ; *$4C44E-$4C498 LONG (Bank09.asm)
    Sprite_ResetAll:
    {
        JSL Sprite_DisableAll  ; $4C22F IN ROM
    
    ; *$4C452 ALTERNATE ENTRY POINT
    .justBuffers
    
        STZ $0FDD : STZ $0FDC : STZ $0FFD
        STZ $02F0 : STZ $0FC6 : STZ $0B6A
        STZ $0FB3
        
        LDA $7EF3CC : CMP.b #$0D
        
        ; branch if Link has the super bomb tagalong following him
        BEQ .superBomb
        
        LDA.b #$FE : STA $04B4
    
    .superBomb
    
        REP #$10
        
        LDX.w #$0FFF
        LDA.b #$00
    
    .clearLocationBuffer
    
        STA $7FDF80, X : DEX
        
        BPL .clearLocationBuffer
        
        LDX.w #$01FF
    
    .clearDeathBuffer
    
        STA $7FEF80, X : DEX
        
        BPL .clearDeathBuffer
        
        SEP #$10
        
        LDY.b #$07
        LDA.b #$FF
    
    .clearRecentRoomsList
    
        STA $0B80, Y : DEY
        
        BPL .clearRecentRoomsList
        
        RTL
    }


    ; *$4AC6B-$4ACF2 LONG (Bank09.asm)
    Ancilla_TerminateSelectInteractives:
    {
        PHB : PHK : PLB
        
        LDX.b #$05
    
    .nextObject
    
        ; check for 3D crystal
        LDA $0C4A, X : CMP.b #$3E : BNE .not3DCrystal
        
        TXY
        
        BRA .checkIfCarryingObject
    
    .not3DCrystal
    
        ; checks if any cane of somaria blocks are in play?
        LDA $0C4A, X : CMP.b #$2C : BNE .checkIfCarryingObject
        
        STZ $0646
        
        LDA $48 : AND.b #$80 : BEQ .checkIfCarryingObject
        
        ; reset Link's grabby status
        STZ $48 : STZ $5E
    
    .checkIfCarryingObject
    
        LDA $0308 : BPL .notCarryingAnything
        
        TXA : INC A : CMP $02EC : BEQ .spareObject
        
        BRA .terminateObject
    
    .notCarryingAnything
    
        TXA : INC A : CMP $02EC : BNE .terminateObject
        
        STZ $02EC
    
    .terminateObject
    
        STZ $0C4A, X
    
    .spareObject
    
        DEX : BPL .nextObject
        
        LDA $037A : AND.b #$10 : BEQ .theta
        
        STZ $46
        STZ $037A
    
    .theta
    
        ; Reset flute playing interval timer.
        STZ $03F0
        
        ; Reset tagalong detatchment timer.
        STZ $02F2
        
        ; Only place this is written to. Never read.
        STZ $02F3
        STZ $035F
        STZ $03FC
        
        STZ $037B
        STZ $03FD
        STZ $0360
        
        LDA $5D : CMP.b #$13 : BNE .notUsingHookshot
        
        LDA.b #$00 : STA $5D
        
        LDA $3A   : AND.b #$BF : STA $3A
        LDA $50   : AND.b #$FE : STA $50
        LDA $037A : AND.b #$FB : STA $037A
        
        STZ $037E
    
    .notUsingHookshot
    
        PLB
        
        RTL
    }

    ; *$3F1A3-$3F259 LONG
    Player_ResetState:
    {
        STZ $26     ;$26[0x01] - (Player) The direction(s) that Link is pushing against.

        STZ $67     ;$67[0x01] - (Player) Indicates which direction Link is walking (even if not going anywhere).
        STZ $031F   ;$031F[0x01] - Countdown timer that, when it's set, causes Link's sprite to blink, i.e. flash on and off
        STZ $034A   ;$034A[0x01] - (Player) Flag indicating whether Link is moving or not. (I think)
        
        JSL Player_ResetSwimState
        
        STZ $02E1   ;$02E1[0x01] - (Player) Link is transforming? (Poofing in a cloud to transform into something else.)
        STZ $031F   ;$031F[0x01] - Countdown timer that, when it's set, causes Link's sprite to blink, i.e. flash on and off
        STZ $03DB   ;$03DB[0x06] - (Ancilla) Special Object ram.
        STZ $02E0   ;$02E0[0x01] - (Player) Flag for Link's graphics set. 0 - Normal Link
        STZ $56     ;$56[0x01] - (PlayerOam) Link's graphic status. 1 - bunny link, 0 - real link.
        STZ $03F5   ;$03F5[0x02] - (Player) The timer for Link's tempbunny state. 
        STZ $03F7   ;$03F7[0x01] - (Player) Flag indicating whether the "poof" needs to occur for Link to transform into the tempbunny.
        STZ $03FC   ;$03FC[0x01] - ????
        STZ $03F8   ;$03F8[0x01] - Flag set if you are near a PullForRupees sprite
        STZ $03FA   ;$03FA[0x02] - Relates to Link's OAM routine in Bank 0D somehow. Appears to be the 9th bit of the X coordinate of some sprite(s).
        STZ $03E9   ;$03E9[0x01] - Flag that seems to set when moving gravestones are in play and puzzle sound is playing.
        STZ $0373   ;$0373[0x01] - (Player) Putting a non zero value here indicates how much life to subtract from Link. (quick reference: 0x08 = one heart)
        STZ $031E   ;$031E[0x01] - used as an offset for a table to retrieve values for $031C. The offset comes in increments of four, depending on which direction Link is facing when he begins to spin. This makes sense, given that he always spins the same direction, and allows for reusability between the different directions, each one being a sub set of the full sequence.
        STZ $02F2   ;$02F2[0x01] - (Tagalong) Used as a bitfield of event flags of a temporary nature relating to Tagalongs.
        STZ $02F8   ;$02F8[0x01] - Flag used to make Link make a noise when he gets done bouncing after a wall he's dashed into. Thus, it only has any use in relation to dashing.
        STZ $02FA   ;$02FA[0x01] - Flag that is set if you are near a moveable statue (close enough to grab it)
        STZ $02E9   ;$02E9[0x01] - Item Receipt Method. 0 - Receiving item from an NPC or message
        STZ $02DB   ;$02DB[0x01] - (Player) Triggered by the Whirlpool sprite (but only when touched in Area 0x1B). The Whirlpool sprite used is not visible to the player, and is placed in the open perimeter gate to Hyrule Castle after beating Agahnim. I guess they needed some mechanism to make that happen, and that sprite was specialized in order to do so. Apparently this is set when warping to the Dark World?
    
    ; *$3F1E6 ALTERNATE ENTRY POINT
    ; called by mirror warping.
    
        STZ $02F5   ;$02F5[0x01] - (Player) 0 - Not on a Somaria Platform.
        STZ $0079   ;$79[0x01] - (Player) Controls whether to do a spin attack or not? Update: Actually looks more like a step counter for the spin attack...
        STZ $0302   ;$0302[0x01] - (Player) ????
        STZ $02F4   ;$02F4[0x01] - Only use is for caching the current value of $0314 in some instances
        STZ $48     ;$48[0x01] - (PlayerOam) If set, when the A button is pressed, the player sprite will enter the "grabbing at something" state.
        STZ $5A     ;$5A[0x01] - (Player) ????
        STZ $5B     ;$5B[0x01] - (Player) 0 - indicates nothing, 1 - Player is dangerously near the edge of a pit, 2 - Player is falling
        
        ; \wtf Why zeroed twice? probably a typo on the programmer's end.
        ; Or maybe it was aliased to two different names...
        STZ $5B     ; "
    
    ; *$3F1FA ALTERNATE ENTRY POINT
    ; called by some odd balls.
    
        STZ $036C   ;$036C[0x01] - Action index when interacting with tiles, like pots, rocks, or chests. 0 - ???, 1 - Picks up a pot or bush.
        STZ $031C   ;$031C[0x01] - (Player) tells us the actual graphic/state to use on the given step of a spin attack
        STZ $031D   ;$031D[0x01] - (Player) step counter for the spin attack
        STZ $0315   ;$0315[0x01] - Seems to be a flag that is set to 0 if Link is not moving, and 1 if he is moving. However it doesn't seem to get reset to zero.
        STZ $03EF   ;$03EF[0x01] - (Player, PlayerOam) Normally zero. If set to nonzero, it forces Link to the pose where he is holding his sword up. One example of where this is used is right after Ganon is defeated.
        STZ $02E3   ;$02E3[0x01] - (Player) (Slightly uncertain about this) Delay timer between attacks involving the sword. In essence, the repeat rate at which you are able to swing your sword. May have an impact on other types of sword attacks like stabbing and dash attacks.
        STZ $02F6   ;$02F6[0x02] - (Player) Bitfield for interaction with Blue Rupee, Grabbable, and Key Door tiles
        STZ $0301   ;$0301[0x01] - (Player) [bmuaethr] When non zero, Link has something in his hand, poised to strike. It's intended that only one bit in this flag be set at any time, though.
        STZ $037A   ;$037A[0x01] - (Player) Puts Link in various positions, 1 - shovel, 2 - praying, etc... cane of somaria. May also have something to do with bombs?
        STZ $020B   ;$020B[0x01] - Seems to be a debug value for Module 0x0E.0x01
        STZ $0350   ;$0350[0x01] - (WriteOnly) free ram, though it would need to be reclaimed from the game engine as it's currently used in several places as an apparent debug variable. Specifically, it always written to, but never read.
        STZ $030D   ;$030D[0x01] - ????
        STZ $030E   ;$030E[0x01] - Always seems to be set to 0, and only read during OAM handling of the player sprite.
        STZ $030A   ;$030A[0x01] - (PlayerOam) Step counter used with $030B. Also, $030A-B seem to be used for the opening of the desert palace
        STZ $3B     ;$3B[0x01] - (Player) Bitfield for the A button
        STZ $3A     ;$3A[0x01] - Bitfield for the B and Y buttons
        STZ $3C     ;$3C[0x01] - (Player) Lower Nibble: How many frames the B button has been held, approximately. Upper nibble: set to 9 on spin attack release.
        STZ $0308   ;$0308[0x01] - Bit 7 - is set when Link is carrying something. Bit 1 - set when Link is praying?
        STZ $0309   ;$0309[0x01] - 0 - nothing. 1 - picking up something. 2 - throwing something or halfway done picking up something.
        STZ $0376   ;$0376[0x01] -   bit 0: Link is grabbing a wall.
        STZ $50     ;$50[0x01] - (Player) A flag indicating whether a change of the direction Link is facing is possible. For example, when the B button is held down with a sword. 0        - Can change
        STZ $4D     ;$4D[0x01] - An Auxiliary Link handler. As far as I know, 0x00 - ground state (normal)
        STZ $46     ;$46[0x01] - (Player) A countdown timer that incapacitates Link when damaged or in recoil state. If nonzero, no movement input is recorded for Link.
        STZ $0360   ;$0360[0x01] - A flag that, when nonzero, causes Link to be electrocuted when touching an enemy. This seems counterintuitive to me, but whatever.
        STZ $02DA   ;$02DA[0x01] - (Player) Flag indicating whether Link is in the pose used to hold an item or not. 0 - no extra pose.
        STZ $55     ;$55[0x01] - Cape flag, when set, makes you invisible and invincible. You can also go through objects, such as bungies.
        
        JSR $9D84  ; $39D84 IN ROM (BRANCH_EPSILON)
        
        STZ $037B   ;$037B[0x01] - (Player) If nonzero, disables Link's ability to receive hits from sprites. (But not pits)
        STZ $0300   ;$0300[0x01] - Link's state changes? Happens when using boomerang. Also related to electrocution maybe?
        STZ $037E   ;$037E[0x01] - (Player), (Hookshot) Bit 0 - Hookshot is dragging Link somewhere. Bit 1 - ???? seems like it gets toggled every 3 frames, or something like that.
        STZ $02EC   ;$02EC[0x01] - seems to be a flag for (Link's) collision with bombs. Maybe other uses
        STZ $0314   ;$0314[0x01] - (Player) The index (the X in $0DD0 for example) of the sprite that Link is "touching"
        STZ $03F8   ;$03F8[0x01] - Flag set if you are near a PullForRupees sprite
        STZ $02FA   ;$02FA[0x01] -   Flag that is set if you are near a moveable statue (close enough to grab it)
        
        RTL
    }

; *$39D84-$39E62 LOCAL
{

BRANCH_EPSILON:

    ; Bring Link to stop
    STZ $5E
    
    LDA $48 : AND.b #$F6 : STA $48
    
    ; Stop any animations Link is doing
    STZ $3D
    STZ $3C
    
    ; Nullify button input on the B button
    LDA $3A : AND.b #$7E : STA $3A
    
    ; Make it so Link can change direction if need be
    LDA $50 : AND.b #$FE : STA $50
    
    BRL BRANCH_ALPHA

; *$39D9F ALTERNATE ENTRY POINT

    BIT $48 : BNE BRANCH_BETA
    
    LDA $48 : AND.b #$09 : BNE BRANCH_GAMMA

BRANCH_BETA:

    LDA $47    : BEQ BRANCH_DELTA
    CMP.b #$01 : BEQ BRANCH_EPSILON

BRANCH_GAMMA:

    LDA $3C : CMP.b #$09 : BNE BRANCH_ZETA
    
    LDX.b #$0A : STX $3C
    
    LDA $9CBF, X : STA $3D

BRANCH_ZETA:

    DEC $3D : BPL BRANCH_THETA
    
    LDA $3C : INC A : CMP.b #$0D : BNE BRANCH_KAPPA
    
    LDA $7EF359 : INC A : AND.b #$FE : BEQ BRANCH_LAMBDA
    
    LDA $48 : AND.b #$09 : BEQ BRANCH_LAMBDA
    
    LDY.b #$01
    LDA.b #$1B
    
    JSL AddWallTapSpark ; $49395 IN ROM
    
    LDA $48 : AND.b #$08 : BNE BRANCH_MUNU
    
    LDA $05 : JSR Player_DoSfx2
    
    BRA BRANCH_XI

BRANCH_MUNU:

    LDA.b #$06 : JSR Player_DoSfx2

BRANCH_XI:

    ; Do sword interaction with tiles
    LDY.b #$01
    
    JSR $D077   ; $3D077 IN ROM
    
BRANCH_LAMBDA:

    LDA.b #$0A

BRANCH_KAPPA:

    STA $3C : TAX
    
    LDA $9CBF, X : STA $3D
    
BRANCH_THETA:

    BRA BRANCH_RHO

BRANCH_DELTA:

    LDA.b #$09 : STA $3C
    
    LDA.b #$01 : TSB $50
    
    STZ $3D
    
    LDA $5E
    
    CMP.b #$04 : BEQ BRANCH_RHO
    CMP.b #$10 : BEQ BRANCH_RHO
    
    LDA.b #$0C : STA $5E
    
    LDA $7EF359 : INC A : AND.b #$FE : BEQ BRANCH_ALPHA
    
    LDX.b #$04

BRANCH_PHI:

    LDA $0C4A, X
    
    CMP.b #$30 : BEQ BRANCH_ALPHA
    CMP.b #$31 : BEQ BRANCH_ALPHA
    
    DEX : BPL BRANCH_PHI
    
    LDA $79 : CMP.b #$06 : BCC BRANCH_CHI
    
    LDA $1A : AND.b #$03 : BNE BRANCH_CHI
    
    JSL AncillaSpawn_SwordChargeSparkle

BRANCH_CHI:

    LDA $79 : CMP.b #$40 : BCS BRANCH_ALPHA
    
    INC $79 : LDA $79 : CMP.b #$30 : BNE BRANCH_ALPHA
    
    LDA.b #$37 : JSR Player_DoSfx2
    
    JSL AddChargedSpinAttackSparkle
    
    BRA BRANCH_ALPHA

BRANCH_RHO:

    JSR $9E63 ; $39E63 IN ROM

BRANCH_ALPHA:
    
    RTS
}
