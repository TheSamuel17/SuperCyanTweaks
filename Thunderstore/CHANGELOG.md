## 1.0.91

- DIE NULLREFERENCEEXCEPTION DIE

## 1.0.9

- [Straw'Pair'y](https://thunderstore.io/package/acanthic/SeekingTheVoid/)
    - Nullref hunting. Shoutouts Snowy Snowtime.
    - Tagged the item as FoodRelated so it may be used for the Hearty Stew recipe.
- Drone Scrapper: shortened its animation.

## 1.0.8

- Fixed some enemies' melee attacks immediately triggering if interrupted or killed during the wind-up. This change affects Blind Vermin, Gup (including descendants) and Halcyonite.
    - As a side effect, this means they might refuse to go off at very *very* high attack speed values. Big whoop.
- Fixed Void Jailer becoming idle upon breaking line of sight with its target.
- Greater Wisp: increased attack range. (50m -> 75m)
- Stone Titan / Aurelionite
    - Removed minimum distance requirement to use the laser. (20m -> 0m)
    - Max laser range increased. (80m -> 100m)
    - Fist endlag decreased. (3s -> 2s)
    - Rock summon duration decreased. (9s -> 7s)
- Halcyonite
    - Credit cost decreased. (200 -> 125)
    - Removed that weird little pause they do upon starting their laser attack.
    - Whirlwind tickrate now scales with attack speed.
    - Whirlwind now has a small proc coefficient. (0 -> 0.2)
    - Whirlwind now steadily tracks its target instead of calculating the target's position once at the beginning.
- Made Conduit Canyon less fucking evil. My god.
    - Adjusted cost multiplier of weak Collective Elites. (x2 -> x4)
        - Wait, x2?????
        - Also included compatibility with [EclipseRevamped](https://thunderstore.io/package/Nuxlar/EclipseRevamped/)'s E3 modifier; the cost multiplier will be appropriately reduced by 20%.
    - The Elite promoting algorithm will now make sure it has enough credits to afford the upgrade, instead of... ignoring that and going into debt. What do you mean, *going into debt?*
    - Enemies promoted to weak Collective now have their gold reward increased accordingly.
    - Base monster credits for Collective Combat Shrines reduced. (120 leader / 70 support -> 100/0)
        - For reference, regular Combat Shrines hold 100.
        - After spawning the "leader", all leftover monster credits are automatically transferred to the supporting monsters. Thus, the supporting monsters will almost always spawn even if the base value is set to 0.
    - Collective Combat Shrine monster credits are no longer halved upon spawning the supporting monsters.
- Stage 4 Shrine of Order: added compatibility for [Sunset Tropics/Midnight Tropics](https://thunderstore.io/package/wormsworms/Sunset_Tropics/).
- [Straw'Pair'y](https://thunderstore.io/package/acanthic/SeekingTheVoid/): worked some black magic to make Gup a little Smarter.

## 1.0.7

- Updated Chinese translation, courtesy of JunJun5406 (Growth Nectar was missing).
- *Actually* finished the French translation.
- A guaranteed Shrine of Order spawns in a fixed location on every Stage 4.
- Shrines on Pretender's Precipice are now snowy like they should be.
- Access Node + Mountain Shrine rewards are now multiplicative when both are activated, since the bosses themselves are.
- Solus Prospector
    - Can no longer hit you multiple times per attack.
    - Configurable attack range. (Unchanged by default.)
- Alloy Hunter: laser skill cooldown now begins when the skill ends, which prevents perma-lasering on E7.
- Alloy Worship Unit
    - Reduced shield duration while performing the super attack. (6s -> 5s)
    - Super attack cooldown begins 5s after execution, matching the duration of the shield.
- Commencement
    - Initial monster credits reduced. (900 -> 450)
    - Increased charge radius for all Pillars. (20m -> 25m)
    - Decreased charge duration for Pillar of Mass. (60s -> 45s)
    - Pillar monster credits slightly increased (700 -> 720) to better account for Lunar Exploder cost change. (2 Perfected + 5 Regular -> 3 Perfected)
- Lunar Exploder credit cost decreased. (80 -> 40)
- Lunar Wisp credit cost decreased outside of Commencement. (550 -> 450)
- Perfected Elites
    - Cripple duration is now affected by proc coefficient.
    - Base health multiplier increased. (x2 -> x2.4, effectively x2.5 -> x3)

## 1.0.6

- Implemented Chinese translation, courtesy of JunJun5406. I noticed the pull request too late, so if you're reading this, Growth Nectar needs a pass.
- Finished the French translation.
- Frost Relic can now crit.
- Clay Apothecary mortar blast now sorts by angle only instead of "distance and angle", which often practically randomized its target. It should now more consistently aim at its intended target.

## 1.0.5

- Removed any and all jank from the Rallypoint Delta fan buff logic. In completely unrelated news, I now know what a `JumpVolume` is.
- Drifter
    - Junk Cube search angle increased. (8 degrees -> 15)
    - Junk Cube search distance increased. (40m -> 50m)
    - Cleanup no longer resets cooldown on use.
- Growth Nectar: max buff count per stack increased. (4 -> 5)
- Eclipse Lite: now calculates barrier gain using max health + shields instead of just max health.
- Genesis Loop
    - Proc coefficient increased. (1.0 -> 3.0)
    - Skips the line of sight check if used by a Player team member.
- Made some IL hooks more robust.

## 1.0.4

- Added "Disable New Configs" config. If enabled, any newly-generated config will be disabled. Turn that on if you don't care about The Director's Vision, or don't want to deal with new updates introducing unwanted changes.
- Grandparent
    - Rock velocity now also dynamically scales with distance.
    - Made the target-searching logic for the rock throw attack functional. As a result, Grandparents will be able to more reliably chuck boulders at airborne targets, reducing the likelihood of falling back to throwing it absolutely nowhere near its current target.
- When looping, Solus Control Units will be able to spawn during the Solus Wing fight.

## 1.0.3

- Drone Scrapper
    - Max count of 1 per stage.
    - Can now spawn on Treeborn Colony & Golden Dieback.
- Foreign Fruit cooldown decreased. (45s -> 30s)
- Eclipse 4 & 7 modifiers now apply to every non-player team; this notably includes the Void team.
- Default config: Grandparent gravity orb pull force increased (-3000 -> -4000) so targets fall out of it less at higher velocities.
- Taking the fans on Rallypoint Delta grants a temporary speed boost.
- Cauldrons on Commencement will never contain items tagged with OnStageBeginEffect.
- Further refined Best Buddy's behavior. Still a little clingy, but we love them for that.
- Sonorous Whispers rework
    - Allowed an exception case for downed Solus Heart; it'll now drop an item when defeated, just like the other final bosses.
    - Solus Wing will eject the items far enough to not end up completely buried.
- Halcyon Seed
    - Ally Aurelionite now grabs the attention of every enemy within 60m upon spawning, as well as any subsequently struck target. Taunt effect lasts for 15s.
    - Removed ally Aurelionite's minimum distance requirement to use the laser. (10m -> 0m)
- Halcyonite
    - Spawns on Prime Meridian pre-loop.
    - Is now categorized as a Miniboss on every vanilla stage, instead of being categorized as a Champion most of the time. For some reason.
    - When looping, they will also spawn during False Son Phase 2 alongside Stone Golems.
- False Son Phase 2: enemy count scales with stage count, effectively amounting to 4 (+5 per loop).
- Clay Apothecary
    - Now uses the slam/mortar attack at range regardless of health.
    - Slam/mortar attack no longer inflicts self-damage.

## 1.0.2

- Old War Stealthkit activation threshold increased. (25% -> 50%)
- Wake of Vultures buff duration increased from 8s (+5s per stack) to 12s (+6s per stack).
- Orphaned Core
    - Ram damage coefficient increased. (400%/1000% -> 600%/1500%)
    - Rehit delay decreased. (1.5s -> 1s)
    - Increased hitbox size to roughly match the kinetic aura generated around Best Buddy.
    - AI is slightly more competent.

## 1.0.1

- Empathy Cores damage boost only affects skill damage instead of base damage.
- War Bonds is no longer inheritable.
- Eccentric Vase
    - Cooldown decreased. (45s -> 30s)
    - Distance increased. (1000m -> 2000m)
    - Improved acceleration while traveling.
    - Grants invisibility & intangibility while traveling.
    - Grants brief (0.5s) fall damage immunity on exit.
- Molotov (6-Pack)
    - Fire residue duration increased. (7s -> 10s)
    - Fire residue size multiplied by x1.75.
    - Fire residue tickrate increased to 2/s. Believe it or not, it's actually just 1/s right now.
    - Added a config to adjust the impact explosion damage, though it is unchanged by default.

## 1.0.0

- Release.