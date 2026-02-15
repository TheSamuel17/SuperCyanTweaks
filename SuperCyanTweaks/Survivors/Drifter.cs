using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuperCyanTweaks
{
    public class Drifter
    {
        public static EntityStateConfiguration cubeLaunched = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/DLC3/Drifter/EntityStates.JunkCube.Launched.asset").WaitForCompletion();
        public static RoR2.Skills.DrifterSkillDef cleanupSkill = Addressables.LoadAssetAsync<RoR2.Skills.DrifterSkillDef>("RoR2/DLC3/Drifter/Cleanup.asset").WaitForCompletion();

        public Drifter()
        {
            // Adjust Junk Cube search angle
            if (Configs.drifterCubeSearchAngle.Value >= 0)
            {
                cubeLaunched.TryModifyFieldValue("searchAngle", Configs.drifterCubeSearchAngle.Value);
            }

            // Adjust Junk Cube search distance
            if (Configs.drifterCubeSearchDistance.Value >= 0)
            {
                cubeLaunched.TryModifyFieldValue("searchDistance", Configs.drifterCubeSearchDistance.Value);
            }

            // Fix Cleanup cooldown quirk
            if (Configs.drifterCleanupCooldownTweak.Value == true)
            {
                cleanupSkill.resetCooldownTimerOnUse = false;
            }
        }
    }
}
