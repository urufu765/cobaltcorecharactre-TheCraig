# Illeana the SNEK

A corrosion engineer who works with corrosion to help keep the ship at tip top condition.

## Changelog

### PUBLIC

#### 1.2: Illeana Story

* **ext.2*: Craig as playable?
* **ext.1*: Craig ship?
* **dev.6*: Fine-tune dialogue
* **dev.5**: Craig Ophidiophobia replacement sprite (as Cracs), and gave background a little bit more fill
* **dev.4**: SFX
* **dev.3**: Fix sprites, bg sprites
* **dev.2**: Backgrounds worked on (0.75/2 done) (still need to properly animate the glow.. also remove the pen from Craig's mouth in bg)
* **dev.1**: Let us break out the story. Todo: stories, a lot of new sprites that aren't Illeana, backgrounds, maybe even a playable ship?

#### 1.1: Illeana Duo Machine!

* **(1.1.5)**: Added mod options (along with Ophidiophobia mode :D)
* **(1.1.4)**: Fixed a dialogue being said by the wrong person, and added dialogue for Weth stuff
* **(1.1.3)**: Optimized for release (not built: Changed IncludedMOdProjectPaths to ModFiles, updated ModBuildConfig)
  * **1.1.3a**: csproj fix so assets are properly copied
* **(1.1.2)**: Made the harmless warnings shut up and moved dialogue validators after mod existence checks, internal changes
* **(1.1.1)**: Autotomy B cardart tint was wrong
* **(1.1.0)**: Switch over to DialogueMachine + New dialogue! + Duos + Tarnish!
  * **dev.a0**: Switch to DialogueMachine
  * **dev.a1**: Old dialogue cleanup + dialogue fixes
  * **dev.b0**: Added PerfectShield card for artifact
  * **dev.b1**: Duo descriptions and API
  * **dev.b2**: Reusable Scrap, Thrust Thursters, Airlock, Extra Slip, Perfected Protection implemented
  * **dev.b3**: Super Injection, Lubricated Heatpump, Unprotected Storage, Hull Harvester implemented and tarnish tooltip changed to match upcoming rework. Byproduct Processor no longer needs energy
  * **dev.c0**: Caustic Armor buff with the upcoming Tarnish rework, and implemented Competition
  * **dev.c1**: Duo sprites!
  * **dev.c2**: Finished all duo sprites and fixed Airlock description
  * **dev.c3**: Mini art and panic(+blinkrapid, shocked) eyes art fix
  * **dev.d0**: New tarnish implementation
  * **dev.e0**: Card rework 3 (optimised for NEW Tarnish) & Tarnish cost implemented
    * Autotomy: Corrode clear + Evade -> Corrode clear + Autododge & Blockable Hurt
    * Build-A-Cure: Cost 1 -> 0, A gives 1 non-exhaust Cure instead of 2 exhaust Cure
    * Find-A-Solution: Overhauled to give Tarnish more and give Solution instead of Cure.
    * Part Swap: Ace and PS is given in different conditions and corrode cost adjusted
    * Acidic Package: Removed.
    * NEW Acid Backflow: Replaces Acidic Package by being reusable
    * Immunity Shot: 1 Corrode -> 1 Tarnish
    * Makeshift Hull: Removed.
    * NEW Improvised Timing: Cost = max hull
    * NEW Solution: like Cure, but Takes Tarnish, gives Heal
  * **dev.e1**: Tarnish only converts corrode when being added. New corrode with existing tarnish does nothing.
  * **dev.e2**: Dracula Duo Complete!
  * **ext.f0**: Fixed duo not being excluded if duo mod not present and Reusable Scrap being annoying
  * **ext.f1**: Card edits
    * Untested Substance: Now requires 1 tarnish to play, reworked B to only give exhaust
    * Build-A-Cure: Removed the "Exhaustible" part, and B gives two Cure B's
    * Find-A-Solution: Removed the "Exhaustible" part
    * Solution: B loses retain, base and A now exhaust
    * Cure: A loses recycle, base and B now exhaust
  * **ext.f2**: Forgot to remove the code that removes corrode when hitting tarnished
  * **ext.f3**: Set X begun
  * **ext.f4**: Set X Continu and FINISH! (disabled in next version as it will be ported over to Nickel)
  * **ext.f5**: ImmunityShot no longer gives tarnish, Gone-Jiffy buffed and dialogue machine fixed! + more dialogue and dialogue fixes
  * **ext.f6**: Fixed Exposure counting Tempshield for some reason + Lightened Load only gives 1 dodge per tarnish + fixed missing period
  * **RELEASE**: More dialogue! (a bit more)
  * *ext.b0*: Full body sprites + finish the screams + Craig sprites
  * *ext.c0*: Memory

#### 1.0: First Public Release!!!

* **(1.0.6)**: Fixed issue where some dialogues didn't select Illeana, EXE now implemented to starter deck and also exhausts.
* **(1.0.5)**: Balance patch: Immunity shot SETS perfect shield, instead of adding.
* **(1.0.4)**: Balance patch: Gone-In-A-Jiffy balance change, EFS only grants up to 2 evade per turn, Scrap Patchkit balance
* **(1.0.3)**: Fixed FailureB not giving player corrode
* **(1.0.2)**: Fixed Forged Certificate requiring one more than described
* **(1.0.1)**: Fixed Restoration giving the wrong amount of heal
* **(1.0.0)**: Build-A-Cure A no longer offers Failure A

### ALPHA

#### a.3: Don't Talk With Your Mouth Full

* **(0.3.23)**: Applied constructive criticism to final ALPHA phase
* **(0.3.22)**: New portraits to finish the sprites! + Dialogue sprite adjustments
* **(0.3.21)**: Portrait fix! Replaced all the current ones and changed all the de-implemented dialogue
* **(0.3.20)**: Custom sprites for the new Slitherman variants + tooltip clarification
* **(0.3.19)**: New variants of the Slitherman + Snektunez A and B overhaul (A does double, B does not end turn)
* **(0.3.18)**: Many more dialogue 5: Many many many more shouts, finished vanilla shouts
* **(0.3.17)**: Many more dialogue 4: Many many more shouts & events + Even more delayed Personal Stereo
* **(0.3.16)**: Many more dialogue 3: Many more shouts + dialogue restructuring + External Fuel Source description clarification + Delayed add card queue for Personal Stereo
* **(0.3.15)**: Changed Gone-In-A-Jiffy and clarified Amputation and made shine more visible, removed unnecessary combat dialogue
* **(0.3.14)**: Many more dialogue 2: Finished artifact and more combat
* **(0.3.13)**: Many more dialogue: Shopkeeper & Dracula & More & Fixed artifact dialogue not playing properly
* **(0.3.12)**: Fixed a combat dialogue not dialoguing correctly
* **(0.3.11)**: Fixed description not working.
* **(0.3.10)**: Implemented fix for modded dialogues, just apply them much later + artiact and card dialogue
* **(0.3.9)**: Added Warp Prototype
* **(0.3.8)**: TheFailure and TheAccident now exhausts on play. BuildACure now gives temporary cures instead of reusable ones. Removed The from Failure, Accident, and Cure, and Failure now just gives you 1 corrode on draw.
* **(0.3.7)**: More dialogue (EnemyPack edition)
* **(0.3.6)**: Added a cat EXE card for Illeana and a secret card that has a rare chance to spawn
* **(0.3.5)**: Fixed StoryDialogue not being converted to new system of animations && Removed unused variable in PartSwap
* **(0.3.4)**: Simplified animation registering + Memory dialogue planned out (but not implemented)
* **(0.3.3)**: Take/deal damage dialogue + Weaponised Patchkit balance + Deck Colour Change
* **(0.3.2)**: Added alt art for Experimental Lubricant, and made it switch on use
* **(0.3.1)**: Renamed Illeana's Personal Stereo to Stolen Slitherman
* **(0.3.0)**: Added more dialogues (corrode replies) + Changelog + More character sprites

#### a.2: Card Rework 2

* **(0.2.6)**: Card Art Updates B: The Alternatives + Partswap giving wrong temp shield fix
* **(0.2.5)**: Card Art Updates A: The Tintening
* **(0.2.4)**: The Accident/The Failure readability fix
* **(0.2.3)**: Bad descriptor fixed + little mistakes
* **(0.2.2)**: Fixed name for Restoration
* **(0.2.1)**: Forgot to fix False Vaccine's upgrade B cost
* **(0.2.0)**: Card balance changes
  * Overhauls
    * Deadly Amputation: Now hurts to give Ace rather than corrodes to evade
    * Part Swap: Now costs corrode to get PShield and ace rather than hurting to remove corrode
    * Weaponised Patchkit: Now does weaken/brittle shots but gives a corrode instead of armoring and shooting corrode
  * Balance Changes
    * Acidic Package: (Now exhausts as intended)
    * Exposure: Upgrade A cost -1
    * Untested Substance: No longer gives Ace
    * Autotomy: Swapped A and B
    * GoneJiffy: B now exhausts
    * Amputation: Swapped A and B
    * Great Healing: Name changed to Great Restoration, corrode give lowered and placed as first action
    * Distracted: Rare -> Uncommon
    * Makeshift Hull: Uncommon -> Rare, Base and A now apply the same effects and exhausts, and B now hurts to give max hull
    * Laced Yo Food: Reduced corrode give from 2 to 1 for Base and A

#### a.1: First Alpha Release

* **(0.1.7)**: Gave better descriptions
* **(0.1.6)**: Added artifact Toxic Sports, but left it unimplemented
* **(0.1.5)**: Forgot to capitalise name
* **(0.1.4)**: Gave Illeana's name Colour and fixed mini sprite cheek corner
* **(0.1.3)**: Added alt starter + Lightened Load artifact
* **(0.1.2)**: Changed the project name from Craig to Illeana (Finally!)
* **(0.1.1)**: Fixed Causticarmor tooltip and artifact pulse
* **(0.1.0)**: First alpha release!

### Pre-release

* **(0.0.28)**: Byproduct Processor * Caustic Armor buff
* **(0.0.27)**: More tooltip fixes + Tempoboost fix
* **(0.0.26)**: New artifat + Fixed artifact glossary + Changed Experimental Lubricant
* **(0.0.25)**: Dialogue test
* **(0.0.24)**: Sprite reworks
* **(0.0.23)**: Adjusted artifact art, simplified artifact registration
* **(0.0.22)**: Changed Scrapkit upgrade B to retain
* **(0.0.21)**: Fixed TheAccident giving tarnish to the wrong target
* **(0.0.20)**: Fixed TheAccident's description
* **(0.0.19)**: Cards Overhaul + removed private sets on artifacts
* **(0.0.18b)**: Forgot to remove something...
* **(0.0.18)**: Fixed SnekTunez state not being saved
* **(0.0.17)**: Text fixes
* **(0.0.16)**: Added simple artifacts
* **(0.0.15)**: Tarnish implemented correctly
* **(0.0.14)**: Tarnish implemented
* **(0.0.13)**: Added the rest of the cards
* **(0.0.12)**: New sprites!
* **(0.0.11)**: More Cards added
* **(0.0.10)**: Implemented Corrode cost
* **(0.0.9)**: FindACure/BuildACure now adds the right number of cards
* **(0.0.8)**: FindACure/BuildACure now add cards as intended
* **(0.0.7)**: Forgot upgrade paths?
* **(0.0.6)**: Changed names of the project
* **(0.0.5)**: Fixed mismatched sprite names
* **(0.0.4)**: Animation frames needed to be renamed
* **(0.0.3)**: Weird compile errors ironed out
* **(0.0.2)**: Nipped the leftovers from Demomod
* **(0.0.1)**: BAM! (Began Assured Mutilation)!
