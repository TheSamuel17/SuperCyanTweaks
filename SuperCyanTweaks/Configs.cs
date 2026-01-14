using BepInEx.Configuration;

namespace SuperCyanTweaks
{
    public static class Configs
    {
        // ==================== MONSTERS ==================== //
        public static ConfigEntry<bool> alphaConstructTeleportEnabled { get; private set; }
        public static ConfigEntry<float> alphaConstructTeleportMinDist { get; private set; }
        public static ConfigEntry<float> alphaConstructTeleportCooldown { get; private set; }
        public static ConfigEntry<float> beetleSpawnDuration { get; private set; }
        public static ConfigEntry<float> beetleHeadbuttDmg { get; private set; }
        public static ConfigEntry<float> bighornBisonChargeCoeff { get; private set; }
        public static ConfigEntry<int> clayApothecaryCost { get; private set; }
        public static ConfigEntry<float> clayApothecaryTarBallRange { get; private set; }
        public static ConfigEntry<int> geepCost { get; private set; }
        public static ConfigEntry<int> grandparentCost { get; private set; }
        public static ConfigEntry<float> grandparentRockActivationRange { get; private set; }
        public static ConfigEntry<float> grandparentGravOrbForce { get; private set; }
        public static ConfigEntry<bool> grandparentGravOrbDynamicSpeed { get; private set; }
        public static ConfigEntry<bool> grandparentReduceDowntime { get; private set; }
        public static ConfigEntry<bool> gupDelayAttackOnSpawn { get; private set; }
        public static ConfigEntry<bool> impOnHelminthRoost { get; private set; }
        public static ConfigEntry<float> lemurianFireballRange { get; private set; }
        public static ConfigEntry<float> mushrumSprintDist { get; private set; }
        public static ConfigEntry<float> mushrumSprintCoeff { get; private set; }
        public static ConfigEntry<bool> mushrumOnHabitatFall { get; private set; }
        public static ConfigEntry<bool> scuRespectNodegraph { get; private set; }
        public static ConfigEntry<float> scuPrimaryAttackRange { get; private set; }
        public static ConfigEntry<float> scuProbeHealthThreshold { get; private set; }
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
        public static ConfigEntry<float> happiestMaskProcChance { get; private set; }
        public static ConfigEntry<bool> sonorousWhispersRework { get; private set; }
        public static ConfigEntry<bool> warBondsInheritable { get; private set; }
        public static ConfigEntry<bool> waxQuailMultiJump { get; private set; }

        // ==================== EQUIPMENT ==================== //
        public static ConfigEntry<float> eccentricVaseCooldown { get; private set; }
        public static ConfigEntry<float> eccentricVaseMaxDist { get; private set; }
        public static ConfigEntry<float> eccentricVaseAcceleration { get; private set; }
        public static ConfigEntry<bool> eccentricVaseCloak { get; private set; }
        public static ConfigEntry<bool> eccentricVaseIntangible { get; private set; }
        public static ConfigEntry<bool> eccentricVaseNoCrater { get; private set; }
        public static ConfigEntry<float> molotovResidueSize { get; private set; }
        public static ConfigEntry<float> molotovResidueDuration { get; private set; }
        public static ConfigEntry<float> molotovResidueTickrate { get; private set; }
        public static ConfigEntry<float> molotovExplosionDamage { get; private set; }

        // ==================== DRONES ==================== //
        public static ConfigEntry<bool> equipDroneAlwaysFire { get; private set; }
        public static ConfigEntry<float> gunnerTurretRange { get; private set; }

        public static void Init(ConfigFile cfg)
        {
            // ==================== MONSTERS ==================== //

            // Alpha Construct
            alphaConstructTeleportEnabled = cfg.Bind("Enemies - Alpha Construct", "Teleport To Players", true, "Alpha Constructs that are out of range will periodically teleport near a target. Vanilla is false.");
            alphaConstructTeleportMinDist = cfg.Bind("Enemies - Alpha Construct", "Minimum Teleport Distance", 75f, "Alpha Constructs will attempt to teleport if their target is past this range. For reference, their attacking range is 75m.");
            alphaConstructTeleportCooldown = cfg.Bind("Enemies - Alpha Construct", "Teleport Cooldown", 30f, "Set the cooldown of the Alpha Construct's teleport skill.");

            // Beetle
            beetleSpawnDuration = cfg.Bind("Enemies - Beetle", "Spawn Duration", 3.5f, "Set the spawn duration to this value. Vanilla is 5. Set to a negative value for no change.");
            beetleHeadbuttDmg = cfg.Bind("Enemies - Beetle", "Headbutt Damage Coefficient", 2.5f, "Set the damage coefficient to this value. Vanilla is 2. Set to a negative value for no change.");

            // Bighorn Bison
            bighornBisonChargeCoeff = cfg.Bind("Enemies - Bighorn Bison", "Charge Coefficient", -1f, "Base speed is multiplied by this value while charging. Vanilla is 8. Set to a negative value for no change.");

            // Clay Apothecary
            clayApothecaryCost = cfg.Bind("Enemies - Clay Apothecary", "Spawn Cost", 125, "Set the director credit cost. Vanilla is 150. Set to a negative value for no change.");
            clayApothecaryTarBallRange = cfg.Bind("Enemies - Clay Apothecary", "Max Tar Ball Range", 75f, "Clay Apothecaries will attack from this far away with tar balls. Vanilla is 65. Set to a negative value for no change.");

            // Geep
            geepCost = cfg.Bind("Enemies - Geep", "Spawn Cost", 50, "Set the director credit cost. Vanilla is 35. Set to a negative value for no change.");

            // Grandparent
            grandparentCost = cfg.Bind("Enemies - Grandparent", "Spawn Cost", 1000, "Set the director credit cost. Vanilla is 1150. Set to a negative value for no change.");
            grandparentRockActivationRange = cfg.Bind("Enemies - Grandparent", "Rock Throw Activation Range", 1500f, "Activation range of Rock Throw. Vanilla is 300. Set to a negative value for no change.");
            grandparentGravOrbForce = cfg.Bind("Enemies - Grandparent", "Gravity Orb Force", -3000f, "The force of the gravity orb's pull. Set to vanilla value (-1000) for no change.");
            grandparentGravOrbDynamicSpeed = cfg.Bind("Enemies - Grandparent", "Dynamic Orb Speed", true, "Gravity Orb projectile speed scales with target distance. Vanilla is false.");
            grandparentReduceDowntime = cfg.Bind("Enemies - Grandparent", "Reduce Downtime", true, "Grandparents will spend less time in the 'rotate to target' behaviors. Vanilla is false.");

            // Gup
            gupDelayAttackOnSpawn = cfg.Bind("Enemies - Gup", "Delay Attack After Spawning", true, "Adds a small delay between spawning and throwing out their first attack.");

            // Imp
            impOnHelminthRoost = cfg.Bind("Enemies - Imp", "Spawn on Helminth Hatchery", true, "Imps appear on Helminth Hatchery.");

            // Lemurian
            lemurianFireballRange = cfg.Bind("Enemies - Lemurian", "Max Fireball Range", 45f, "Lemurians will attack from this far away with fireballs. Strafe range is still capped at 30m. Vanilla is 30. Set to a negative value for no change.");

            // Mini Mushrum
            mushrumSprintDist = cfg.Bind("Enemies - Mini Mushrum", "Sprint Distance", 60f, "Mini Mushrums start sprinting from this far away. Set to a negative value to disable.");
            mushrumSprintCoeff = cfg.Bind("Enemies - Mini Mushrum", "Sprint Coefficient", 3f, "Set the sprint speed multiplier to this value. Vanilla is 2.5. Set to a negative value for no change.");
            mushrumOnHabitatFall = cfg.Bind("Enemies - Mini Mushrum", "Spawn on Golden Dieback", true, "Mini Mushrums appear on Golden Dieback.");

            // Solus Control Unit
            scuRespectNodegraph = cfg.Bind("Enemies - Solus Control Unit", "Respect Nodegraph", true, "Solus Control Units will properly follow the nodegraph while chasing. Applies to AWU as well.");
            scuPrimaryAttackRange = cfg.Bind("Enemies - Solus Control Unit", "Max Primary Attack Range", 100f, "Solus Control Units will attack from this far away. Vanilla is 50. Set to a negative value for no change. Applies to AWU as well.");
            scuProbeHealthThreshold = cfg.Bind("Enemies - Solus Control Unit", "Probe Health Threshold", 1f, "Solus Control Units will deploy Solus Probes under this health fraction. Vanilla is 0.9. Set to a negative value for no change. Applies to AWU as well.");

            // Solus Distributor
            distributorCost = cfg.Bind("Enemies - Solus Distributor", "Spawn Cost", 40, "Set the director credit cost. Vanilla is 20. Set to a negative value for no change.");

            // Solus Probe
            probeCost = cfg.Bind("Enemies - Solus Probe", "Spawn Cost", 40, "Set the director credit cost. Vanilla is 60. Set to a negative value for no change.");
            probeOnShipGraveyard = cfg.Bind("Enemies - Solus Probe", "Spawn on Siren’s Call", true, "Solus Probes appear on Siren’s Call.");
            probeOnRepurposedCrater = cfg.Bind("Enemies - Solus Probe", "Spawn on Repurposed Crater", true, "Solus Probes appear on Repurposed Crater.");
            probeOnSolusEvent = cfg.Bind("Enemies - Solus Probe", "Add to Solus Family Event", true, "Solus Probes appear during the Solus Family Event.");
            probeDelayAttackOnSpawn = cfg.Bind("Enemies - Solus Probe", "Delay Attack After Spawning", true, "Adds a small delay between spawning and throwing out their first attack.");

            // Solus Transporter
            transporterCost = cfg.Bind("Enemies - Solus Transporter", "Spawn Cost", 125, "Set the director credit cost. Vanilla is 200. Set to a negative value for no change.");

            // Void Reaver
            voidReaverCost = cfg.Bind("Enemies - Void Reaver", "Spawn Cost", 250, "Set the director credit cost. Vanilla is 300. Set to a negative value for no change.");

            // ==================== ITEMS ==================== //

            // Eclipse Lite
            eclipseLiteBarrierBase = cfg.Bind("Items - Eclipse Lite", "Base Barrier Percentage", -1f, "Set the barrier gain per second of cooldown, in percentage. Vanilla is 1%. Set to a negative value for no change.");
            eclipseLiteBarrierStack = cfg.Bind("Items - Eclipse Lite", "Stack Barrier Percentage", .5f, "Set the barrier gain per second of cooldown, in percentage. Vanilla is 0.25%. Set to a negative value for no change.");

            // Empathy Cores
            empathyCoresDamageTweak = cfg.Bind("Items - Empathy Cores", "Empathy Cores Damage Tweak", true, "Empathy Cores damage boost affects skill damage instead of base damage.\nThis nerfs synergies that depend on base damage, notably the chainguns from Spare Drone Parts.");

            // Happiest Mask
            happiestMaskProcChance = cfg.Bind("Items - Happiest Mask", "Proc Chance", 10f, "Set the proc chance of this item, in percentage. Vanilla is 7%. Set to a negative value for no change.");

            // Sonorous Whispers
            sonorousWhispersRework = cfg.Bind("Items - Sonorous Whispers", "Sonorous Whispers Rework", true, "Items only drop from bosses proper instead of Champions, and will no longer drop from elites.\nStacking improves item rarity.\nEffect is now team-wide.");

            // War Bonds
            warBondsInheritable = cfg.Bind("Items - War Bonds", "War Bonds is Inheritable", false, "Whether this item can be inherited by turrets and such. Vanilla is true.");

            // Wax Quail
            waxQuailMultiJump = cfg.Bind("Items - Wax Quail", "Wax Quail Multi Jump", true, "Additional non-base jumps will grant a jump boost as well. Vanilla is false.");

            // ==================== EQUIPMENT ==================== //

            // Eccentric Vase
            eccentricVaseCooldown = cfg.Bind("Equipment - Eccentric Vase", "Cooldown", 30f, "Adjust this equipment's cooldown. Vanilla is 45. Set to a negative value for no change.");
            eccentricVaseMaxDist = cfg.Bind("Equipment - Eccentric Vase", "Max Range", 2000f, "Adjust the maximum length of the zipline. Vanilla is 1000. Set to a negative value for no change.");
            eccentricVaseAcceleration = cfg.Bind("Equipment - Eccentric Vase", "Acceleration", 50f, "Set the acceleration value. Vanilla is 30. Set to a negative value for no change.");
            eccentricVaseCloak = cfg.Bind("Equipment - Eccentric Vase", "Cloaked While Traveling", true, "User is cloaked while traveling. Vanilla is false.");
            eccentricVaseIntangible = cfg.Bind("Equipment - Eccentric Vase", "Intangible While Traveling", true, "User is intangible while traveling. Vanilla is false.");
            eccentricVaseNoCrater = cfg.Bind("Equipment - Eccentric Vase", "No Cratering", true, "User is briefly immune to fall damage on exit. Vanilla is false.");

            // Motolov (6-Pack)
            molotovResidueSize = cfg.Bind("Equipment - Molotov (6-Pack)", "Fire Residue Size", 1.75f, "Multiply the size of the lingering fire pools by this value. Vanilla is 1. Set to a negative value for no change.");
            molotovResidueDuration = cfg.Bind("Equipment - Molotov (6-Pack)", "Fire Residue Duration", 10f, "Adjust the duration of the lingering fire pools. Vanilla is 7. Set to a negative value for no change.");
            molotovResidueTickrate = cfg.Bind("Equipment - Molotov (6-Pack)", "Fire Residue Tickrate", 2f, "Adjust the tickrate of the lingering fire pools, in attacks per second. Vanilla is 1. Set to a negative value for no change.");
            molotovExplosionDamage = cfg.Bind("Equipment - Molotov (6-Pack)", "Explosion Damage", -1f, "Set the damage coefficient of the fire explosions to this value. Vanilla is 2.5, despite what the description says. Set to a negative value for no change.");

            // ==================== DRONES ==================== //

            // Equipment Drone
            equipDroneAlwaysFire = cfg.Bind("Drones - Equipment Drone", "Always Fire Equipment", true, "Equipment Drones will attempt to fire their Equipment as often as possible. Vanilla is false.");

            // Gunner Turret
            gunnerTurretRange = cfg.Bind("Drones - Gunner Turret", "Attack Range", 75f, "Gunner Turrets will attack from this far away. Vanilla is 60. Set to a negative value for no change.");
        }
    }
}
