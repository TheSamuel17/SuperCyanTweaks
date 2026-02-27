using RoR2;
using BepInEx;
using R2API;

namespace SuperCyanTweaks
{
    // Dependencies
    [BepInDependency(DirectorAPI.PluginGUID)]
    [BepInDependency(R2API.ContentManagement.R2APIContentManager.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInDependency(RecalculateStatsAPI.PluginGUID)]

    // Soft dependencies
    [BepInDependency("Jeffdev.FalseSonBossTweaks", BepInDependency.DependencyFlags.SoftDependency)] // Phase 2 enemy tweaks of this mod can't be conventionally turned off, so I have to forcibly acquire priority

    // Metadata
    [BepInPlugin("Samuel17.SuperCyanTweaks", "SuperCyanTweaks", "1.0.7")]

    public class Main : BaseUnityPlugin
    {
        public static ItemDef emptyItemDef = null;

        public void Awake()
        {
            // Logging!
            Log.Init(Logger);

            // Load configs
            Configs.Init(Config);

            // Changes - Enemies
            new AlloyHunter();
            new AlloyWorshipUnit();
            new AlphaConstruct();
            new Beetle();
            new BighornBison();
            new ClayApothecary();
            new FalseSon();
            new Geep();
            new Grandparent();
            new Gup();
            new Halcyonite();
            new Imp();
            new Lemurian();
            new LunarExploder();
            new LunarWisp();
            new MiniMushrum();
            new SolusControlUnit();
            new SolusDistributor();
            new SolusProbe();
            new SolusProspector();
            new SolusTransporter();
            new VoidReaver();

            // Changes - Items
            new EclipseLite();
            new EmpathyCores();
            new FrostRelic();
            new GenesisLoop();
            new GrowthNectar();
            new HalcyonSeed();
            new HappiestMask();
            new OldWarStealthkit();
            new OrphanedCore();
            new SonorousWhispers();
            new WakeOfVultures();
            new WarBonds();
            new WaxQuail();

            // Changes - Equipment
            new EccentricVase();
            new ForeignFruit();
            new Molotov6Pack();

            // Changes - Drones
            new EquipmentDrone();
            new GunnerTurret();

            // Changes - Interactables
            new AccessNode();
            new DroneScrapper();
            new ShrineOfOrder();

            // Changes - Stages
            new Commencement();
            new PretendersPrecipice();
            new RallypointDelta();

            // Changes - Survivors
            new Drifter();

            // Changes - Miscellaneous
            new Eclipse();
            new PerfectedElite();
        }
    }
}
