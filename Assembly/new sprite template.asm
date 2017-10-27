;----------------------------------------------------------------------------------------------------------------------------------
;Sprites Variables, X is the sprite ID - All informations come from the Zelda_3_Ram.log file
;----------------------------------------------------------------------------------------------------------------------------------
;$0B58, X ; Timers for stunned enemies. Counts down from 0xFF.

;$0B6B, X ; Multiples Bits Data [ttttacbp]
;t - 'Tile Interaction Hit Box', a, c, b - 'Dies like a boss', p - Sprite ignores falling into a pit when frozen?

;$0B89, X ; Object priority stuff for sprites?

;$0BA0, X ; Seems to indicate that it ignores all projectile interactions if set.

;$0BB0, X ; For sprites that interact with speical objects, the special object will identify its type to the sprite via this location.

;$0BE0, X ; [iwbspppp]
;i - If set, disable tile interactions for the sprite, such as falling into holes, moving floors, and conveyor belts
;w - set if in water whether that's deep water or shallow water
;b - If set, the sprite can be blocked by a shield
;s - If set, play the 'enemy taking damage' sound effect. Otherwise, play the basic 'sprite getting hit' sound effect
;p - Prize pack to grant

;$0C9A, X ; Room or Area number that the sprite has been loaded to. (If in a dungeon, only contains the lower byte)

;$0CAA, X ; Multiples Bits Data [abcdefgh]
;a - If set... creates some condition where it may or may not die
;b - Same as bit 'a' in some contexts (Zora in particular)
;c - While this is set and unset in a lot of places for various sprites, its
;    status doesn't appear to ever be queried. Based on the pattern of its
;	 usage, however, the best deduction I can make is that this was a flag
;	 intended to signal that a sprite is an interactive object that Link can
;	 push against, pull on, or otherwise exerts a physical presence.
;	 In general, it might have indicated some kind of A button (action
;	 button) affinity for the sprite, but I think this is merely informative
;	 rather than something relevant to gameplay.
;d - If hit from front, deflect Ice Rod, Somarian missile,
;	 boomerang, hookshot, and sword beam, and arrows stick in
;	 it harmlessly.  If bit 1 is also set, frontal arrows will
;	 instead disappear harmlessly.  No monsters have bit 4 set
;	 in the ROM data, but it was functional and interesting
;	 enough to include.
;e - If set, makes the sprite collide with less tiles than usual
;f - If set, makes sprite impervious to sword and hammer type attacks
;g - ???? Seems to make sprite impervious to arrows, but may have other additional meanings.
;h - disabled???

;$0CBA, X ; Sprite drop when he die:
;0x00: nothing happens. / 0x01: leaves a normal key. / 0x03: single green rupee. / anything else: Big Key

;$0CD2, X ; Bump damage the sprite can inflict on the player.

;$0CE2, X ; When the sprite is hit, this is written to with the amount of damage to subtract from the sprite's HP.

;$0CF2, X ; Damage type determiner

;$0D00, X ; The lower byte of a sprite's Y - coordinate.

;$0D10, X ; The lower byte of a sprite's X - coordinate.

;$0D20, X ; The high byte of a sprite's Y - coordinate.

;$0D30, X ; The high byte of a sprite's X - coordinate.

;$0D40, X ; Y velocity

;$0D50, X ; X velocity

;$0D60, X ; Y "second derivative" to give a path a more rounded shape when needed.

;$0D70, X ; X "second derivative" to give a path a more rounded shape when needed.

;$0D80, X ; Controls whether the sprite has been spawned yet. 0 - no. Not 0 - yes. Also used as an AI pointer

;$0D90, X ; In some creatures, used as an index for determining $0DC0 *Unused by sprite Test can be used for anything*

;$0DA0, X ; usage varies considerably for each sprite type **USED by the sprite Test to intialize the sprite**

;$0DB0, X ; Various usages *Unused by sprite Test can be used for anything*

;$0DC0, X ; Designate which graphic to use.

;$0DD0, X ; Sprite State:
;0x00 - Sprite is dead, totally inactive
;0x01 - Sprite falling into a pit with generic animation.
;0x02 - Sprite transforms into a puff of smoke, often producing an item
;0x03 - Sprite falling into deep water (optionally making a fish jump up?)
;0x04 - Death Mode for Bosses (lots of explosions).
;0x05 - Sprite falling into a pit that has a special animation (e.g. Soldier)
;0x06 - Death Mode for normal creatures.
;0x08 - Sprite is being spawned at load time. An initialization routine will
;       be run for one frame, and then move on to the active state (0x09) the very next frame.
;0x09 - Sprite is in the normal, active mode.
;0x0A - Sprite is being carried by the player.
;0x0B - Sprite is frozen and / or stunned.

;$0DE0, X ; Sprite Directions *Unused by sprite Test can be used for anything*

;$0DF0, X ; Main delay timer [decreased every frames from sprites routine until it reach 0]

;$0E00, X ; Main Delay Timer 1 [decreased every frames from sprites routine until it reach 0]

;$0E10, X ; Main Delay Timer 2 [decreased every frames from sprites routine until it reach 0]

;$0E20, X ; ID of the sprite that control which sprite type the sprite is

;$0E30, X ; Subtype designation 1?

;$0E40, X ; Bits 0-4: If zero, the sprite is invisible. Otherwise, visible.

;$0E50, X ; Health of the sprite

;$0E60, X ; [niospppu]
;n - If set, don't draw extra death animation sprites over the sprite as it is expiring.
;i - if set, sprite is impervious to all attacks (also collisions?)
;o - If set, adjust coordinates of sprites spawned off of this one, such as water splashes. In general this would roughly approximate the
;    concept of 'width' of the sprite, and for this reason usually absorbable items like arrows, rupees, and heart refills utilize this.
;s - If set, draw a shadow for the sprite when doing OAM handling
;p - (Note: 3-bit) Palette into that actually is not used by this variable, but ends up getting copied to the array $0F50 (bitwise and with 0x0F).
;u - unused?

;$0E70, X ; Bit set When a sprite is moving and has hit a wall: ----udlr

;$0E80, X ; Subtype Designation 2?

;$0E90, X ; When a Pikit grabs something from you it gets stored here. *Unused by sprite Test can be used for anything*

;$0EA0, X ; When sprite is taking damage. palette cycling index 0x80 -  Signal that the recoil process has finished and will terminate

;$0EB0, X ; For sprites that have a head set the direction of the head *Unused by sprite Test can be used for anything*

;$0EC0, X ; Animation Clock? *Unused by sprite Test can be used for anything*

;$0ED0, X ; ??? *Unused by sprite Test can be used for anything*

;$0EE0, X ; Auxiliary Delay Timer 3 [decreased every frames from sprites routine until it reach 0]

;$0EF0, X ; Death Timer [abbbbbbb]
;a - start death timer?
;b - death timer?

;$0F00, X ; Pause button for sprites apparently. If nonzero they don't do anything.

;$0F10, X ; Auxiliary Delay Timer 4 [decreased every frames from sprites routine until it reach 0]

;$0F20, X ; Floor the sprite stand on

;$0F30, X ; Recoiling Y Velocity when sprite being hit

;$0F40, X ; Recoiling X Velocity when sprite being hit

;$0F50, X ; OAM Related - [vhoopppN]
;v - vflip
;h - hflip
;o - priority
;p - palette
;N - name table

;$0F60, X ; [isphhhhh]
;i - Ignore collision settings and always check tile interaction on the same layer that the sprite is on.
;s - 'Statis'. If set, indicates that the sprite should not be considered as "alive" in routines that try
; 	 to check that property. Functionally, the sprites might not actually be considered to be in statis though.
;	 Example: Bubbles (aka Fire Faeries) are not considered alive for the purposes of puzzles, because it's
;	 not expected that you always have the resources to kill them. Thus, they always have this bit set.
;p - 'Persist' If set, keeps the sprite from being deactivated from being too far offscreen from the camera.
;	 The sprite will continue to move and interact with the game map and other sprites that are also active.
;h - 5-bit value selecting the sprite's hit box dimensions and perhaps other related parameters.

;$0F70, X ; Height value (how far the enemy is from its shadow)

;$0F80, X ; Height Velocity for jump/fall if sprite_move_z is used else can be used for anything

;$0F90, X ; Subpixel portion of altitude.

;$0FC7, X ; Affects something to do with prizes...?

;==================================================================================================================================
;Sprite_Main
;----------------------------------------------------------------------------------------------------------------------------------
;The core of the sprite that is executing all the subfunctions OnCreate, OnInit, OnUpdate, OnDeath, OnTimer1-3, OnDamage, Draw
;==================================================================================================================================
Sprite_Test_Main:
{
  ;Call Create once
	LDA $0DA0, X : BNE .already_created
		INC : STA $0DA0, X ; set 0DA0 to 1 to prevent coming back here
		JSR Sprite_Test_OnCreate ; Call OnCreate
	.already_created

	JSR Sprite_Test_Draw ; Call Draw every frames
	JSL Sprite_CheckIfActive ; Prevent the sprite from moving when we are in menu/transitioning

	LDA $0DA0, X : CMP #$01 : BNE .already_init
		INC : STA $0DA0, X ; set 0DA0 to 2 to prevent coming back here
		JSR Sprite_Test_OnInit ; Call OnInit
    .already_init
    .not_defeated_yet

    ;Call Update every frames
	JSR Sprite_Test_OnUpdate

	;Timer 1
	LDA $0E00, X : BNE .timer1_not_over
		JSR Sprite_Test_OnTimer1
	.timer1_not_over

	;Timer 2
	LDA $0E10, X : BNE .timer2_not_over
		JSR Sprite_Test_OnTimer2
	.timer2_not_over

	;Timer 3
	LDA $0EE0, X : BNE .timer3_not_over
		JSR Sprite_Test_OnTimer3
	.timer3_not_over

	;Timer 4
	LDA $0DF0, X : BNE .timer4_not_over
		JSR Sprite_Test_OnTimer4
	.timer4_not_over

  	;Timer 5 (special timer?)
	LDA $0F10, X : BNE .timer5_not_over
		JSR Sprite_Test_OnTimer5
	.timer5_not_over


	;If the sprite has received damage
	LDA $0CE2, X : BEQ .not_damaged
		JSR Sprite_Test_OnDamageFromPlayer
	.not_damaged

	;Check if the sprite is still active
	LDA $0DD0, X : BEQ .not_active
		LDA $0E50, X : DEC : BMI .not_dead_yet ;health is lower than 0 so the sprite is dying
			JSR Sprite_Test_OnDeath
		.not_dead_yet
	.not_active

	RTL

}
;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnCreate
;Executed once when the sprite is created
;Executed during screen transitions
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnCreate:
{

	RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnInit
;Executed once when the sprite is initialized
;Executed when the screen transition is completed
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnInit:
{


	RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnUpdate
;Executed once every frames after the intializations
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnUpdate:
{

RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnDamageFromPlayer
;Executed once when the sprite receive damage
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnDamageFromPlayer:
{

RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnTimer1
;Executed once when the timer1[$0E00, X] reach 0
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnTimer1:
{


	RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnTimer2
;Executed once when the timer2[$0E10, X] reach 0
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnTimer2:
{

	RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnTimer3
;Executed once when the timer3[$0EE0, X] reach 0
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnTimer3:
{

	RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnTimer4
;Executed once when the timer3[$0DF0, X] reach 0
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnTimer4:
{

	RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnTimer4
;Executed once when the timer3[$0F10, X] reach 0
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnTimer5:
{

	RTS
}

;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_OnDeath
;Executed once when the sprite is about to turn into a death cloud
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_OnDeath:
{

	RTS
}

;16x16 Draw Test
;----------------------------------------------------------------------------------------------------------------------------------
;Sprite_Test_Draw
;Executed every frames to draw the sprite
;----------------------------------------------------------------------------------------------------------------------------------
Sprite_Test_Draw:
{

	JSL Sprite_OAM_AllocateDeferToPlayer ; Mostly used for NPC (draw the sprite under link depending on the positions)
	LDA #$EA : STA $06 : STZ $07 ; Save tile index in $06,  load the gfx part used 00-01 saved in $07
	JSL Sprite_PrepOamCoord ; Set the oam coordinate
	JSL Sprite_DrawSingle16 ; Draw one 16x16 sprite $0DC0, X = frame index
	
	RTS
}
