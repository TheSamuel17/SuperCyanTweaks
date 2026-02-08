using BepInEx.Configuration;

namespace SuperCyanTweaks
{
    public static class Configs
    {
        // ==================== MONSTERS ==================== //
        public static ConfigEntry<bool> disableByDefault { get; private set; }

        // ==================== MONSTERS ==================== //
        public static ConfigEntry<bool> alphaConstructTeleportEnabled { get; private set; }
        public static ConfigEntry<float> alphaConstructTeleportMinDist { get; private set; }
        public static ConfigEntry<float> alphaConstructTeleportCooldown { get; private set; }
        public static ConfigEntry<float> beetleSpawnDuration { get; private set; }
        public static ConfigEntry<float> beetleHeadbuttDmg { get; private set; }
        public static ConfigEntry<float> bighornBisonChargeCoeff { get; private set; }
        public static ConfigEntry<int> clayApothecaryCost { get; private set; }
        public static ConfigEntry<float> clayApothecaryTarBallRange { get; private set; }
        public static ConfigEntry<float> clayApothecaryMortarHealthThreshold { get; private set; }
        public static ConfigEntry<float> clayApothecarySlamSelfDmg { get; private set; }
        public static ConfigEntry<bool> falseSonGolemCountTweak { get; private set; }
        public static ConfigEntry<int> geepCost { get; private set; }
        public static ConfigEntry<int> grandparentCost { get; private set; }
        public static ConfigEntry<float> grandparentRockActivationRange { get; private set; }
        public static ConfigEntry<bool> grandparentRockDynamicSpeed { get; private set; }
        public static ConfigEntry<bool> grandparentRockTargeting { get; private set; }
        public static ConfigEntry<float> grandparentGravOrbForce { get; private set; }
        public static ConfigEntry<bool> grandparentGravOrbDynamicSpeed { get; private set; }
        public static ConfigEntry<bool> grandparentReduceDowntime { get; private set; }
        public static ConfigEntry<bool> gupDelayAttackOnSpawn { get; private set; }
        public static ConfigEntry<bool> halcyoniteOnMeridianPreLoop { get; private set; }
        public static ConfigEntry<bool> halcyoniteOnFalseSonLoop { get; private set; }
        public static ConfigEntry<bool> halcyoniteProperCategory { get; private set; }
        public static ConfigEntry<bool> impOnHelminthRoost { get; private set; }
        public static ConfigEntry<float> lemurianFireballRange { get; private set; }
        public static ConfigEntry<float> mushrumSprintDist { get; private set; }
        public static ConfigEntry<float> mushrumSprintCoeff { get; private set; }
        public static ConfigEntry<bool> mushrumOnHabitatFall { get; private set; }
        public static ConfigEntry<bool> scuRespectNodegraph { get; private set; }
        public static ConfigEntry<float> scuPrimaryAttackRange { get; private set; }
        public static ConfigEntry<float> scuProbeHealthThreshold { get; private set; }
        public static ConfigEntry<bool> scuOnSolusWingLoop { get; private set; }
        public static ConfigEntry<int> distributorCost { get; private set; }
        public static ConfigEntry<int> probeCost { get; private set; }
        public static ConfigEntry<bool> probeOnShipGraveyard { get; private set; }
        public static ConfigEntry<bool> probeOnRepurposedCrater { get; private set; }
        public static ConfigEntry<bool> probeOnSolusEvent { get; private set; }
        public static ConfigEntry<bool> probeDelayAttackOnSpawn { get; private set; }
        public static ConfigEntry<int> transporterCost { get; private set; }
        public static ConfigEntry<int> voidReaverCost { get; private set; }

        // ==================== ITEMS ==================== //
        public static ConfigEntry<float> eclipseLiteBarrierBase { get; private set; }
        public static ConfigEntry<float> eclipseLiteBarrierStack { get; private set; }
        public static ConfigEntry<bool> empathyCoresDamageTweak { get; private set; }
        public static ConfigEntry<bool> allyAurelioniteTaunt { get; private set; }
        public static ConfigEntry<float> allyAurelioniteTauntRange { get; private set; }
        public static ConfigEntry<float> allyAurelioniteTauntDuration { get; private set; }
        public static ConfigEntry<float> allyAurelioniteMinLaserRange { get; private set; }
        public static ConfigEntry<float> happiestMaskProcChance { get; private set; }
        public static ConfigEntry<float> stealthKitThreshold { get; private set; }
        public static ConfigEntry<float> bestBuddyRamDamage { get; private set; }
        public static ConfigEntry<bool> bestBuddyBehaviorTweak { get; private set; }
        public static ConfigEntry<bool> bestBuddyHitDetection { get; private set; }
        public static ConfigEntry<bool> sonorousWhispersRework { get; private set; }
        public static ConfigEntry<float> wakeOfVulturesDurationBase { get; private set; }
        public static ConfigEntry<float> wakeOfVulturesDurationStack { get; private set; }
        public static ConfigEntry<bool> warBondsInheritable { get; private set; }
        public static ConfigEntry<bool> waxQuailMultiJump { get; private set; }

        // ==================== EQUIPMENT ==================== //
        public static ConfigEntry<float> eccentricVaseCooldown { get; private set; }
        public static ConfigEntry<float> eccentricVaseMaxDist { get; private set; }
        public static ConfigEntry<float> eccentricVaseAcceleration { get; private set; }
        public static ConfigEntry<bool> eccentricVaseCloak { get; private set; }
        public static ConfigEntry<bool> eccentricVaseIntangible { get; private set; }
        public static ConfigEntry<bool> eccentricVaseNoCrater { get; private set; }
        public static ConfigEntry<float> foreignFruitCooldown { get; private set; }
        public static ConfigEntry<float> molotovResidueSize { get; private set; }
        public static ConfigEntry<float> molotovResidueDuration { get; private set; }
        public static ConfigEntry<float> molotovResidueTickrate { get; private set; }
        public static ConfigEntry<float> molotovExplosionDamage { get; private set; }

        // ==================== DRONES ==================== //
        public static ConfigEntry<bool> equipDroneAlwaysFire { get; private set; }
        public static ConfigEntry<float> gunnerTurretRange { get; private set; }

        // ==================== INTERACTABLES ==================== //
        public static ConfigEntry<int> droneScrapperMaxCount { get; private set; }
        public static ConfigEntry<bool> droneScrapperOnHabitat { get; private set; }

        // ==================== STAGES ==================== //
        public static ConfigEntry<bool> moonCauldronTweak { get; private set; }
        public static ConfigEntry<bool> fanSpeedBoost { get; private set; }
        public static ConfigEntry<float> fanBuffDuration { get; private set; }
        public static ConfigEntry<float> fanBuffStrength { get; private set; }

        // ==================== MISCELLANEOUS ==================== //
        public static ConfigEntry<bool> eclipseTeamTweak { get; private set; }

        public static void Init(ConfigFile cfg)
        {
            // ==================== MOD ==================== //

            disableByDefault = cfg.Bind("!Mod", "Disable New Configs", false, "If set to true, newly generated configs will always be disabled by default.");

            // ==================== MONSTERS ==================== //

            // Alpha Construct
            alphaConstructTeleportEnabled = cfg.Bind("Enemies - Alpha Construct", "Teleport To Players", !disableByDefault.Value, "Alpha Constructs that are out of range will periodically teleport near a target. Vanilla is false.");
            alphaConstructTeleportMinDist = cfg.Bind("Enemies - Alpha Construct", "Minimum Teleport Distance", 75f, "Alpha Constructs will attempt to teleport if their target is past this range. For reference, their attacking range is 75m.");
            alphaConstructTeleportCooldown = cfg.Bind("Enemies - Alpha Construct", "Teleport Cooldown", 30f, "Set the cooldown of the Alpha Construct's teleport skill.");

            // Beetle
            beetleSpawnDuration = cfg.Bind("Enemies - Beetle", "Spawn Duration", disableByDefault.Value ? -1f : 3.5f, "Set the spawn duration to this value. Vanilla is 5. Set to a negative value for no change.");
            beetleHeadbuttDmg = cfg.Bind("Enemies - Beetle", "Headbutt Damage Coefficient", disableByDefault.Value ? -1f : 2.5f, "Set the damage coefficient to this value. Vanilla is 2. Set to a negative value for no change.");

            // Bighorn Bison
            bighornBisonChargeCoeff = cfg.Bind("Enemies - Bighorn Bison", "Charge Coefficient", -1f, "Base speed is multiplied by this value while charging. Vanilla is 8. Set to a negative value for no change.");

            // Clay Apothecary
            clayApothecaryCost = cfg.Bind("Enemies - Clay Apothecary", "Spawn Cost", disableByDefault.Value ? -1 : 125, "Set the director credit cost. Vanilla is 150. Set to a negative value for no change.");
            clayApothecaryTarBallRange = cfg.Bind("Enemies - Clay Apothecary", "Max Tar Ball Range", disableByDefault.Value ? -1f : 75f, "Clay Apothecaries will attack from this far away with tar balls. Vanilla is 65. Set to a negative value for no change.");
            clayApothecaryMortarHealthThreshold = cfg.Bind("Enemies - Clay Apothecary", "Ranged Mortar Health Threshold", disableByDefault.Value ? -1f : 1f, "Clay Apothecaries will use their slam/mortar attack at range under this health fraction. Vanilla is 0.5. Set to a negative value for no change.");
            clayApothecarySlamSelfDmg = cfg.Bind("Enemies - Clay Apothecary", "Slam Self-Damage", disableByDefault.Value ? -1f : 0f, "Clay Apothecaries will lose this percentage of their current health when using the slam/mortar attack. Vanilla is 5. Set to a negative value for no change.");

            // False Son
            falseSonGolemCountTweak = cfg.Bind("Enemies - False Son", "Golem Count Scales With Stage Count", !disableByDefault.Value, "The maximum number of enemies that can spawn during Phase 2 is directly proportional to the current stage count.\nThus, it will be reduced from 5 to 4 if fought without looping.");

            // Geep
            geepCost = cfg.Bind("Enemies - Geep", "Spawn Cost", disableByDefault.Value ? -1 : 50, "Set the director credit cost. Vanilla is 35. Set to a negative value for no change.");

            // Grandparent
            grandparentCost = cfg.Bind("Enemies - Grandparent", "Spawn Cost", disableByDefault.Value ? -1 : 1000, "Set the director credit cost. Vanilla is 1150. Set to a negative value for no change.");
            grandparentRockActivationRange = cfg.Bind("Enemies - Grandparent", "Rock Throw Activation Range", disableByDefault.Value ? -1f : 1500f, "Activation range of Rock Throw. Vanilla is 300. Set to a negative value for no change.");
            grandparentRockDynamicSpeed = cfg.Bind("Enemies - Grandparent", "Dynamic Rock Speed", !disableByDefault.Value, "Rock projectile speed scales with target distance. Vanilla is false.");
            grandparentRockTargeting = cfg.Bind("Enemies - Grandparent", "Improved Rock Targeting", !disableByDefault.Value, "Improves the target-searching logic to let the rock more reliably reach its destination. Vanilla is false.");
            grandparentGravOrbForce = cfg.Bind("Enemies - Grandparent", "Gravity Orb Force", disableByDefault.Value ? -1000f : -4000f, "The force of the gravity orb's pull. Set to vanilla value (-1000) for no change.");
            grandparentGravOrbDynamicSpeed = cfg.Bind("Enemies - Grandparent", "Dynamic Orb Speed", !disableByDefault.Value, "Gravity Orb projectile speed scales with target distance. Vanilla is false.");
            grandparentReduceDowntime = cfg.Bind("Enemies - Grandparent", "Reduce Downtime", !disableByDefault.Value, "Grandparents will spend less time in the 'rotate to target' behaviors. Vanilla is false.");

            // Gup
            gupDelayAttackOnSpawn = cfg.Bind("Enemies - Gup", "Delay Attack After Spawning", !disableByDefault.Value, "Adds a small delay between spawning and throwing out their first attack.");

            // Halcyonite
            halcyoniteOnMeridianPreLoop = cfg.Bind("Enemies - Halcyonite", "Spawn on Prime Meridian (Pre-Loop)", !disableByDefault.Value, "Halcyonites appear on Prime Meridian pre-loop, as they do in other Path of the Colossus stages.");
            halcyoniteOnFalseSonLoop = cfg.Bind("Enemies - Halcyonite", "Spawn During False Son (Loop)", !disableByDefault.Value, "Halcyonites appear during False Son Phase 2 when looping.");
            halcyoniteProperCategory = cfg.Bind("Enemies - Halcyonite", "Monster Category Fix", !disableByDefault.Value, "Properly categorizes Halcyonite as a Miniboss monster on every stage where it is considered a Champion.\nWhich is pretty much everywhere except Gilded Coast & Prime Meridian.");

            // Imp
            impOnHelminthRoost = cfg.Bind("Enemies - Imp", "Spawn on Helminth Hatchery", !disableByDefault.Value, "Imps appear on Helminth Hatchery.");

            // Lemurian
            lemurianFireballRange = cfg.Bind("Enemies - Lemurian", "Max Fireball Range", disableByDefault.Value ? -1f : 45f, "Lemurians will attack from this far away with fireballs. Strafe range is still capped at 30m. Vanilla is 30. Set to a negative value for no change.");

            // Mini Mushrum
            mushrumSprintDist = cfg.Bind("Enemies - Mini Mushrum", "Sprint Distance", disableByDefault.Value ? -1f : 60f, "Mini Mushrums start sprinting from this far away. Set to a negative value to disable.");
            mushrumSprintCoeff = cfg.Bind("Enemies - Mini Mushrum", "Sprint Coefficient", disableByDefault.Value ? -1f : 3f, "Set the sprint speed multiplier to this value. Vanilla is 2.5. Set to a negative value for no change.");
            mushrumOnHabitatFall = cfg.Bind("Enemies - Mini Mushrum", "Spawn on Golden Dieback", !disableByDefault.Value, "Mini Mushrums appear on Golden Dieback.");

            // Solus Control Unit
            scuRespectNodegraph = cfg.Bind("Enemies - Solus Control Unit", "Respect Nodegraph", !disableByDefault.Value, "Solus Control Units will properly follow the nodegraph while chasing. Applies to AWU as well.");
            scuPrimaryAttackRange = cfg.Bind("Enemies - Solus Control Unit", "Max Primary Attack Range", disableByDefault.Value ? -1f : 100f, "Solus Control Units will attack from this far away. Vanilla is 50. Set to a negative value for no change. Applies to AWU as well.");
            scuProbeHealthThreshold = cfg.Bind("Enemies - Solus Control Unit", "Probe Health Threshold", disableByDefault.Value ? -1f : 1f, "Solus Control Units will deploy Solus Probes under this health fraction. Vanilla is 0.9. Set to a negative value for no change. Applies to AWU as well.");
            scuOnSolusWingLoop = cfg.Bind("Enemies - Solus Control Unit", "Spawn During Solus Wing (Loop)", !disableByDefault.Value, "Solus Control Units appear during the Solus Wing fight when looping.");

            // Solus Distributor
            distributorCost = cfg.Bind("Enemies - Solus Distributor", "Spawn Cost", disableByDefault.Value ? -1 : 40, "Set the director credit cost. Vanilla is 20. Set to a negative value for no change.");

            // Solus Probe
            probeCost = cfg.Bind("Enemies - Solus Probe", "Spawn Cost", disableByDefault.Value ? -1 : 40, "Set the director credit cost. Vanilla is 60. Set to a negative value for no change.");
            probeOnShipGraveyard = cfg.Bind("Enemies - Solus Probe", "Spawn on Siren’s Call", !disableByDefault.Value, "Solus Probes appear on Siren’s Call.");
            probeOnRepurposedCrater = cfg.Bind("Enemies - Solus Probe", "Spawn on Repurposed Crater", !disableByDefault.Value, "Solus Probes appear on Repurposed Crater.");
            probeOnSolusEvent = cfg.Bind("Enemies - Solus Probe", "Add to Solus Family Event", !disableByDefault.Value, "Solus Probes appear during the Solus Family Event.");
            probeDelayAttackOnSpawn = cfg.Bind("Enemies - Solus Probe", "Delay Attack After Spawning", !disableByDefault.Value, "Adds a small delay between spawning and throwing out their first attack.");

            // Solus Transporter
            transporterCost = cfg.Bind("Enemies - Solus Transporter", "Spawn Cost", disableByDefault.Value ? -1 : 125, "Set the director credit cost. Vanilla is 200. Set to a negative value for no change.");

            // Void Reaver
            voidReaverCost = cfg.Bind("Enemies - Void Reaver", "Spawn Cost", disableByDefault.Value ? -1 : 250, "Set the director credit cost. Vanilla is 300. Set to a negative value for no change.");

            // ==================== ITEMS ==================== //

            // Eclipse Lite
            eclipseLiteBarrierBase = cfg.Bind("Items - Eclipse Lite", "Base Barrier Percentage", -1f, "Set the barrier gain per second of cooldown, in percentage. Vanilla is 1%. Set to a negative value for no change.");
            eclipseLiteBarrierStack = cfg.Bind("Items - Eclipse Lite", "Stack Barrier Percentage", disableByDefault.Value ? -1f : .5f, "Set the barrier gain per second of cooldown, in percentage. Vanilla is 0.25%. Set to a negative value for no change.");

            // Empathy Cores
            empathyCoresDamageTweak = cfg.Bind("Items - Empathy Cores", "Empathy Cores Damage Tweak", !disableByDefault.Value, "Empathy Cores damage boost affects skill damage instead of base damage.\nThis nerfs synergies that depend on base damage, notably the chainguns from Spare Drone Parts.");

            // Halcyon Seed
            allyAurelioniteTaunt = cfg.Bind("Items - Halcyon Seed", "Aurelionite Taunt", !disableByDefault.Value, "Aurelionite will get the attention of nearby enemies upon spawning, as well as any enemy that gets hit.");
            allyAurelioniteTauntRange = cfg.Bind("Items - Halcyon Seed", "Taunt Range", disableByDefault.Value ? -1f : 60f, "Set the radius of the taunt effect.");
            allyAurelioniteTauntDuration = cfg.Bind("Items - Halcyon Seed", "Taunt Duration", disableByDefault.Value ? -1f : 15f, "Set how long the enemy will be distracted for.\nIt'll take the highest value between this and the enemy's natural attention span.");
            allyAurelioniteMinLaserRange = cfg.Bind("Items - Halcyon Seed", "Minimum Laser Range", disableByDefault.Value ? -1f : 0f, "The minimum distance Aurelionite is allowed to use the laser. Vanilla is 10. Set to a negative value for no change.");

            // Happiest Mask
            happiestMaskProcChance = cfg.Bind("Items - Happiest Mask", "Proc Chance", disableByDefault.Value ? -1f : 10f, "Set the proc chance of this item, in percentage. Vanilla is 7%. Set to a negative value for no change.");

            // Old War Stealthkit
            stealthKitThreshold = cfg.Bind("Items - Old War Stealthkit", "Activation Threshold", disableByDefault.Value ? -1f : .5f, "This item will activate under this health fraction. Vanilla is 0.25. Set to a negative value for no change.");

            // Orphaned Core
            bestBuddyRamDamage = cfg.Bind("Items - Orphaned Core", "Ram Damage", disableByDefault.Value ? -1f : 6f, "Set the damage coefficient to this value. The value is multiplied by x2.5 against heavy targets. Vanilla is 4. Set to a negative value for no change.");
            bestBuddyBehaviorTweak = cfg.Bind("Items - Orphaned Core", "Tweak Behavior", !disableByDefault.Value, "Adjusts the AI of Best Buddy to be more competent. Vanilla is false.");
            bestBuddyHitDetection = cfg.Bind("Items - Orphaned Core", "Improve Hit Detection", !disableByDefault.Value, "Improves the re-hit rate and makes the kinetic aura hitbox roughly match the visuals. Vanilla is false.");

            // Sonorous Whispers
            sonorousWhispersRework = cfg.Bind("Items - Sonorous Whispers", "Sonorous Whispers Rework", !disableByDefault.Value, "Items only drop from bosses proper instead of Champions, and will no longer drop from elites.\nStacking improves item rarity.\nEffect is now team-wide.");

            // Wake of Vultures
            wakeOfVulturesDurationBase = cfg.Bind("Items - Wake of Vultures", "Base Duration", disableByDefault.Value ? -1f : 12f, "Set the aspect buff duration. Vanilla is 8. Set to a negative value for no change.");
            wakeOfVulturesDurationStack = cfg.Bind("Items - Wake of Vultures", "Stack Duration", disableByDefault.Value ? -1f : 6f, "Set the aspect buff duration. Vanilla is 5. Set to a negative value for no change.");

            // War Bonds
            warBondsInheritable = cfg.Bind("Items - War Bonds", "War Bonds is Inheritable", disableByDefault.Value, "Whether this item can be inherited by turrets and such. Vanilla is true.");

            // Wax Quail
            waxQuailMultiJump = cfg.Bind("Items - Wax Quail", "Wax Quail Multi Jump", !disableByDefault.Value, "Additional non-base jumps will grant a jump boost as well. Vanilla is false.");

            // ==================== EQUIPMENT ==================== //

            // Eccentric Vase
            eccentricVaseCooldown = cfg.Bind("Equipment - Eccentric Vase", "Cooldown", disableByDefault.Value ? -1f : 30f, "Adjust this equipment's cooldown. Vanilla is 45. Set to a negative value for no change.");
            eccentricVaseMaxDist = cfg.Bind("Equipment - Eccentric Vase", "Max Range", disableByDefault.Value ? -1f : 2000f, "Adjust the maximum length of the zipline. Vanilla is 1000. Set to a negative value for no change.");
            eccentricVaseAcceleration = cfg.Bind("Equipment - Eccentric Vase", "Acceleration", disableByDefault.Value ? -1f : 50f, "Set the acceleration value. Vanilla is 30. Set to a negative value for no change.");
            eccentricVaseCloak = cfg.Bind("Equipment - Eccentric Vase", "Cloaked While Traveling", !disableByDefault.Value, "User is cloaked while traveling. Vanilla is false.");
            eccentricVaseIntangible = cfg.Bind("Equipment - Eccentric Vase", "Intangible While Traveling", !disableByDefault.Value, "User is intangible while traveling. Vanilla is false.");
            eccentricVaseNoCrater = cfg.Bind("Equipment - Eccentric Vase", "No Cratering", !disableByDefault.Value, "User is briefly immune to fall damage on exit. Vanilla is false.");

            // Foreign Fruit
            foreignFruitCooldown = cfg.Bind("Equipment - Foreign Fruit", "Cooldown", disableByDefault.Value ? -1f : 30f, "Adjust this equipment's cooldown. Vanilla is 45. Set to a negative value for no change.");

            // Motolov (6-Pack)
            molotovResidueSize = cfg.Bind("Equipment - Molotov (6-Pack)", "Fire Residue Size", disableByDefault.Value ? -1f : 1.75f, "Multiply the size of the lingering fire pools by this value. Vanilla is 1. Set to a negative value for no change.");
            molotovResidueDuration = cfg.Bind("Equipment - Molotov (6-Pack)", "Fire Residue Duration", disableByDefault.Value ? -1f : 10f, "Adjust the duration of the lingering fire pools. Vanilla is 7. Set to a negative value for no change.");
            molotovResidueTickrate = cfg.Bind("Equipment - Molotov (6-Pack)", "Fire Residue Tickrate", disableByDefault.Value ? -1f : 2f, "Adjust the tickrate of the lingering fire pools, in attacks per second. Vanilla is 1. Set to a negative value for no change.");
            molotovExplosionDamage = cfg.Bind("Equipment - Molotov (6-Pack)", "Explosion Damage", -1f, "Set the damage coefficient of the fire explosions to this value. Vanilla is 2.5, despite what the description says. Set to a negative value for no change.");

            // ==================== DRONES ==================== //

            // Equipment Drone
            equipDroneAlwaysFire = cfg.Bind("Drones - Equipment Drone", "Always Fire Equipment", !disableByDefault.Value, "Equipment Drones will attempt to fire their Equipment as often as possible. Vanilla is false.");

            // Gunner Turret
            gunnerTurretRange = cfg.Bind("Drones - Gunner Turret", "Attack Range", disableByDefault.Value ? -1f : 75f, "Gunner Turrets will attack from this far away. Vanilla is 60. Set to a negative value for no change.");

            // ==================== INTERACTABLES ==================== //

            // Drone Scrapper
            droneScrapperMaxCount = cfg.Bind("Interactables - Drone Scrapper", "Max Count", disableByDefault.Value ? -1 : 1, "Set the maximum amount of times it can spawn per stage. Vanilla is -1. Set to negative for no limit.");
            droneScrapperOnHabitat = cfg.Bind("Interactables - Drone Scrapper", "Spawn on Treeborn Colony", !disableByDefault.Value, "Drone Scrappers appear on Treeborn Colony & Golden Dieback.");

            // ==================== STAGES ==================== //

            // Commencement
            moonCauldronTweak = cfg.Bind("Stages - Commencement", "Cauldron Tweak", !disableByDefault.Value, "Blacklist items tagged with 'OnStageBeginEffet', which includes things like Rusted Key/Sale Star.");

            // Rallypoint Delta
            fanSpeedBoost = cfg.Bind("Stages - Rallypoint Delta", "Enable Fan Speed Boost", !disableByDefault.Value, "Taking the fans temporarily increases movement speed for players.");
            fanBuffDuration = cfg.Bind("Stages - Rallypoint Delta", "Fan Buff Duration", disableByDefault.Value ? -1f : 10f, "Set the duration of the speed boost, in seconds.");
            fanBuffStrength = cfg.Bind("Stages - Rallypoint Delta", "Fan Buff Strength", disableByDefault.Value ? -1f : 50f, "Additively increase movement speed by this percentage while under the effects of the buff.");

            // ==================== MISC ==================== //

            // Eclipse
            eclipseTeamTweak = cfg.Bind("Miscellaneous - Eclipse", "E4/E7 Team Tweak", !disableByDefault.Value, "Apply the vanilla Eclipse 4 & 7 modifiers to other non-ally teams, such as Void. Vanilla is false.");
        }
    }
}
