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

    // Metadata
    [BepInPlugin("Samuel17.SuperCyanTweaks", "SuperCyanTweaks", "1.0.2")]

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
            new AlphaConstruct();
            new Beetle();
            new BighornBison();
            new ClayApothecary();
            new Geep();
            new Grandparent();
            new Gup();
            new Imp();
            new Lemurian();
            new MiniMushrum();
            new SolusControlUnit();
            new SolusDistributor();
            new SolusProbe();
            new SolusTransporter();
            new VoidReaver();

            // Changes - Items
            new EclipseLite();
            new EmpathyCores();
            new HappiestMask();
            new OldWarStealthkit();
            new OrphanedCore();
            new SonorousWhispers();
            new WakeOfVultures();
            new WarBonds();
            new WaxQuail();

            // Changes - Equipment

            new EccentricVase();
            new Molotov6Pack();

            // Changes - Drones
            new EquipmentDrone();
            new GunnerTurret();
        }
    }
}
