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